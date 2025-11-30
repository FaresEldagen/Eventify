using Microsoft.EntityFrameworkCore;
using Eventify.Models.Entities;
using Eventify.Services;
using Eventify.Data;
using Eventify.Models.Enums;
using System.Threading.Tasks;

namespace Eventify.Managers
{
    public class VenueManager : IVenueService
    {
        AppDbContext context;
        public VenueManager(AppDbContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            var VenueToDelete = context.Venues.Include(x => x.Events).FirstOrDefault(i=>i.Id == id);
            if (VenueToDelete == null) return 0;
            
            else if (VenueToDelete.Events.Any(v=>v.Status==EventStatusEnum.Paid))
            {
                return -1;
            }
            else 
            {
                
                foreach (var EventItem in VenueToDelete.Events)
                {
                   EventItem.VenueId=null;
                   
                }
                context.Venues.Remove(VenueToDelete);
                context.SaveChanges();

            }

            return 0;
        }

        public List<Venue> Get3()
        {
            var result = context.Venues.Take(3).Include(v => v.VenuePhotos).ToList();
            return result;
        }

        public List<Venue> GetByFilter_Search(
            SortByEnum? sortBy,
            SortingTypeEnum sortingType,
            int pageNumber,
            string? title,
            VenueTypeEnum? venueType,
            int? maxprice ,
            string? city,
            bool? airConditioning,    
            bool? catering,
            bool? wifi,
            bool? parking,
            bool? barService,
            bool? restrooms,
            bool? audioVisual,
            out int totalVenues)
        {
            var query = context.Venues.AsQueryable();

            if (maxprice.HasValue)
            {
                query = query.Where(v => v.PricePerHour <= maxprice);
            }

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(v => EF.Functions.Like(v.Name,$"%{title}%") || EF.Functions.Like(v.Address,$"%{title}%"));

            if (venueType.HasValue)
                query = query.Where(v => v.VenueType == venueType.Value);

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(v => v.City==city);

            if (airConditioning.HasValue && airConditioning.Value)
                query = query.Where(v => v.AirConditioningAvailable);

            if (catering.HasValue && catering.Value)
                query = query.Where(v => v.CateringAvailable);

            if (wifi.HasValue && wifi.Value)
                query = query.Where(v => v.WifiAvailable);

            if (parking.HasValue && parking.Value)
                query = query.Where(v => v.ParkingAvailable);

            if (barService.HasValue && barService.Value)
                query = query.Where(v => v.BarServiceAvailable);

            if (restrooms.HasValue && restrooms.Value)
                query = query.Where(v => v.RestroomsAvailable);

            if (audioVisual.HasValue && audioVisual.Value)
                query = query.Where(v => v.AudioVisualEquipment);


           
            if (sortBy.HasValue)
            {
                if (sortBy==SortByEnum.PriceAscending) 
                    query= query.Where(v => v.PricePerHour<=maxprice.Value).OrderBy(e => e.PricePerHour);
                else if(sortBy == SortByEnum.PriceDescending)
                    query = query.Where(v => v.PricePerHour <= maxprice.Value).OrderByDescending(e => e.PricePerHour);


                if (sortBy == SortByEnum.CapacityAscending)
                    query = query.OrderBy(v => v.Capacity);
                else if(sortBy == SortByEnum.CapacityDescending)
                    query = query.OrderByDescending(v => v.Capacity);

            }

            
            

            // get related photos
            query = query.Include(v => v.VenuePhotos);

            totalVenues = GetTotalPages(query);

            int pageSize = 9;
            int skip = (pageNumber - 1) * pageSize;

            var result = query
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return result;
        }

        private  int GetTotalPages(IQueryable<Venue> query)
        {
            return query.Count();
        }

        public Venue GetById(int id)
        {
            var result = context.Venues.FirstOrDefault(v=>v.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public List<Venue> GetByUserId(int id)
        {
            return context.Venues.Where(v=>v.Id==id).Include(v => v.VenuePhotos).ToList();
        }

        public int Insert(Venue obj)
        {
            context.Venues.Add(obj);
            return context.SaveChanges();
        }




    public int Update(Venue venueFromApp)

        {
            var venueFromDb = context.Venues.FirstOrDefault(v => v.Id == venueFromApp.Id);

            if (venueFromDb == null)
                return 0;

            // Basic Info
            venueFromDb.Name = venueFromApp.Name;
            venueFromDb.VenueType = venueFromApp.VenueType;
            venueFromDb.Address = venueFromApp.Address;
            venueFromDb.City = venueFromApp.City;
            venueFromDb.State = venueFromApp.State;
            venueFromDb.ZIP = venueFromApp.ZIP;
            venueFromDb.Description = venueFromApp.Description;

            // Capacity & Pricing
            venueFromDb.Capacity = venueFromApp.Capacity;
            venueFromDb.PricePerHour = venueFromApp.PricePerHour;

            // Features
            venueFromDb.SpecialFeatures = venueFromApp.SpecialFeatures;
            venueFromDb.AirConditioningAvailable = venueFromApp.AirConditioningAvailable;
            venueFromDb.CateringAvailable = venueFromApp.CateringAvailable;
            venueFromDb.WifiAvailable = venueFromApp.WifiAvailable;
            venueFromDb.ParkingAvailable = venueFromApp.ParkingAvailable;
            venueFromDb.BarServiceAvailable = venueFromApp.BarServiceAvailable;
            venueFromDb.RestroomsAvailable = venueFromApp.RestroomsAvailable;
            venueFromDb.AudioVisualEquipment = venueFromApp.AudioVisualEquipment;

            // Proof
            venueFromDb.ProofOfOwnership = venueFromApp.ProofOfOwnership;

            // Relations (IDs only)
            venueFromDb.OwnerId = venueFromApp.OwnerId;

            return context.SaveChanges();
        }

    }
}

