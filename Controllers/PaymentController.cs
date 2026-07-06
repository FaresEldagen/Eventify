using Eventify.Data;
using Eventify.Models.Entities;
using Eventify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Security.Claims;

public class PaymentController : Controller
{
    private readonly IEventService _eventService;
    private readonly IApplicationUserService _applicationUser;
    private readonly AppDbContext _db;
    private readonly IPaymentService _paymentService;

    public PaymentController(IEventService eventService, IApplicationUserService applicationUser, AppDbContext db, IPaymentService paymentService)
    {
        _eventService = eventService;
        _applicationUser = applicationUser;

        _db = db;
        _paymentService = paymentService;
    }

    // /Payment/PreparePaymentData/5
    [Authorize(Roles ="Organizer")]
    public IActionResult PaymentWithStripe(int eventId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var evt = _eventService.GetByIdWithIncludes(eventId);
        if (evt == null) return NotFound();

        if(evt.OrganizerId == Convert.ToInt32(userId) && User.IsInRole("Organizer"))
        {
            var bookingHours = (evt.EndDateTime - evt.StartDateTime).TotalHours;
            decimal amountInCents = (evt.Venue.PricePerHour * (decimal)bookingHours) * 100;

            // Create Stripe Checkout session
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = amountInCents,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Booking: {evt.Venue.Name}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Payment", new { eventId = evt.EventId }, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Payment", new { eventId = evt.EventId }, Request.Scheme)
            };

            var service = new SessionService();
            var session = service.Create(options);
            
            TempData["SessionId"] = session.Id;
            // Redirect directly to Stripe Checkout
            return Redirect(session.Url);
        }
        return RedirectToAction("Index", "Home");

    }

    // GET: /Payment/Success?eventId=5
    public IActionResult Success(int eventId)
    {
        TempData["IsPaid"] = true;
        var service = new SessionService();
        var session = service.Get(TempData["SessionId"]!.ToString());

        string paymentIntentId = session.PaymentIntentId;
        TempData["PaymentReference"] = paymentIntentId;
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                var evt = _eventService.GetByIdWithIncludes(eventId);
                if (evt == null) return NotFound();
                // Mark the event as Paid
                evt.Status = Eventify.Models.Enums.EventStatusEnum.Paid;
                 var changeInEvents=_eventService.Update(evt);
                if(changeInEvents != 1)
                {
                    throw new Exception();
                }

                // Add money to owner (85% of total)
                var bookingHours = (evt.EndDateTime - evt.StartDateTime).TotalHours;
                decimal totalAmount = evt.Venue!.PricePerHour * (decimal)bookingHours;
                decimal ownerAmount = totalAmount * 0.85m;

                var changeInUsers=_applicationUser.AddMonyToOwnerById(evt.Venue.OwnerId, ownerAmount);
                if (changeInUsers != 1)
                {
                    throw new Exception();
                }
                var newPayment = new Payment
                {
                    Amount = ownerAmount,
                    EventName = evt.EventTitle,
                    PaymentDate = DateTime.UtcNow,
                    EventId = eventId,
                    status = Eventify.Models.Enums.PaymentStatusEnum.Completed,
                    Reference = paymentIntentId
                };

                var rowEffected = _paymentService.Insert(newPayment);

                if(rowEffected != 1)
                {
                    throw new Exception();
                }

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                return RedirectToAction("Cancel", new { eventId = eventId });
            }
        }

        //Set success flag for the popup
        TempData["ShowSuccessPopup"] = true;
        //redirect back to EventDetails
        return RedirectToAction("Details", "Events", new { id = eventId });
    }

    // GET: /Payment/Cancel?eventId=5
    public IActionResult Cancel(int eventId)
    {
        TempData["ShowCancelPopup"] = true;
        if (TempData["IsPaid"] != null)
        {
            try
            {
                var options = new RefundCreateOptions
                {
                    PaymentIntent =TempData["PaymentReference"]!.ToString(),
                    Reason = RefundReasons.RequestedByCustomer
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                TempData["RefundSuccess"] = true;
            }
            catch (Exception ex)
            {
                TempData["RefundError"] = ex.Message;
                return RedirectToAction("Details", "Events", new { id = eventId });
            }
        }
        return RedirectToAction("Details", "Events", new { id = eventId });
    }

    public IActionResult Withdraw(int ownerId)
    {
        using(var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                var rowsEffected = _applicationUser.WithdrawMonyFromOwnerById(ownerId);

                if (rowsEffected != 1)
                    throw new Exception("there is error Occurred while Withdrawing Mony");
                transaction.Commit();
            }
            catch(Exception ex)
            {
                TempData["WithdrawFailed"] = true; 
                transaction.Rollback();
                return RedirectToAction("Index", "Profile");
            }
        }
        TempData["WithdrawSuccess"] = true;
        return RedirectToAction("Index", "Profile");
    }
}

