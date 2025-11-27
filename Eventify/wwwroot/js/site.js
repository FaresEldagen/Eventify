// ============================================
// EVENTIFY - UNIFIED JAVASCRIPT
// ============================================

// ============================================
// THEME MANAGEMENT
// ============================================

const themeToggle = document.getElementById("themeToggle")
const sunIcon = document.getElementById("sunIcon")
const moonIcon = document.getElementById("moonIcon")

function updateThemeIcons(isDark) {
    if (isDark) {
        sunIcon.classList.remove("d-none")
        moonIcon.classList.add("d-none")
    } else {
        sunIcon.classList.add("d-none")
        moonIcon.classList.remove("d-none")
    }
}

function initTheme() {
    const savedTheme = localStorage.getItem("theme")
    const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches
    const isDark = savedTheme === "dark" || (!savedTheme && prefersDark)

    if (isDark) {
        document.documentElement.setAttribute("data-bs-theme", "dark")
    }
    updateThemeIcons(isDark)
}

if (themeToggle) {
    themeToggle.addEventListener("click", () => {
        const currentTheme = document.documentElement.getAttribute("data-bs-theme")
        const newTheme = currentTheme === "dark" ? "light" : "dark"
        document.documentElement.setAttribute("data-bs-theme", newTheme)
        localStorage.setItem("theme", newTheme)
        updateThemeIcons(newTheme === "dark")
    })
}

// ============================================
// SEATING MAP
// ============================================

const seatingMap = document.getElementById("seatingMap")

if (seatingMap) {
    const rows = ["A", "B", "C", "D", "E"]
    const seatsPerRow = 10
    const seats = []

    // Generate seats with random status
    rows.forEach((row) => {
        for (let i = 1; i <= seatsPerRow; i++) {
            const random = Math.random()
            let status = "available"
            if (random < 0.2) status = "sold"
            else if (random < 0.3) status = "selected"

            seats.push({
                id: `${row}${i}`,
                row,
                number: i,
                status,
            })
        }
    })

    function renderSeatingMap() {
        seatingMap.innerHTML = ""

        rows.forEach((row) => {
            const rowDiv = document.createElement("div")
            rowDiv.className = "seat-row"

            const rowLabel = document.createElement("div")
            rowLabel.className = "row-label"
            rowLabel.textContent = row
            rowDiv.appendChild(rowLabel)

            const seatContainer = document.createElement("div")
            seatContainer.className = "seat-container"

            for (let i = 1; i <= seatsPerRow; i++) {
                const seat = seats.find((s) => s.row === row && s.number === i)
                const seatBtn = document.createElement("button")
                seatBtn.className = `seat ${seat.status}`
                seatBtn.textContent = i
                seatBtn.setAttribute("aria-label", `Seat ${seat.id} - ${seat.status}`)

                if (seat.status === "sold") {
                    seatBtn.disabled = true
                } else {
                    seatBtn.addEventListener("click", () => toggleSeat(seat.id))
                }

                seatContainer.appendChild(seatBtn)
            }

            rowDiv.appendChild(seatContainer)
            seatingMap.appendChild(rowDiv)
        })

        updateSelectedSeatsInfo()
    }

    function toggleSeat(seatId) {
        const seat = seats.find((s) => s.id === seatId)
        if (seat && seat.status !== "sold") {
            seat.status = seat.status === "selected" ? "available" : "selected"
            renderSeatingMap()
        }
    }

    function updateSelectedSeatsInfo() {
        const selected = seats.filter((s) => s.status === "selected")
        const infoDiv = document.getElementById("selectedSeatsInfo")

        if (infoDiv) {
            if (selected.length > 0) {
                const seatIds = selected.map((s) => s.id).join(", ")
                infoDiv.innerHTML = `
                    <p class="text-muted mb-3">Selected seats: ${seatIds}</p>
                    <button class="btn btn-primary">Continue to Checkout (${selected.length} seat${selected.length !== 1 ? "s" : ""})</button>
                `
            } else {
                infoDiv.innerHTML = ""
            }
        }
    }

    renderSeatingMap()
}

// ============================================
// TESTIMONIALS CAROUSEL
// ============================================

const testimonials = [
    {
        name: "Sarah Johnson",
        role: "Event Organizer",
        company: "Creative Events Co.",
        content: "Eventify transformed how we manage our events. The booking process is seamless and our clients love the ticketing system.",
        avatar: "SJ",
    },
    {
        name: "Michael Chen",
        role: "Venue Owner",
        company: "Downtown Conference Center",
        content: "Since using Eventify, our booking efficiency increased by 300%. The platform handles everything from inquiries to payments.",
        avatar: "MC",
    },
    {
        name: "Emily Rodriguez",
        role: "Corporate Planner",
        company: "Tech Solutions Inc.",
        content: "The seat selection feature and instant ticketing made our company events so much more professional and organized.",
        avatar: "ER",
    },
]

let currentTestimonial = 0

function showTestimonial(index) {
    const testimonialText = document.getElementById("testimonialText")
    const testimonialAvatar = document.getElementById("testimonialAvatar")
    const testimonialName = document.getElementById("testimonialName")
    const testimonialRole = document.getElementById("testimonialRole")

    if (testimonialText && testimonialAvatar && testimonialName && testimonialRole) {
        const testimonial = testimonials[index]
        testimonialText.textContent = `"${testimonial.content}"`
        testimonialAvatar.textContent = testimonial.avatar
        testimonialName.textContent = testimonial.name
        testimonialRole.textContent = `${testimonial.role}, ${testimonial.company}`

        // Update dots
        document.querySelectorAll(".dot-btn").forEach((dot, i) => {
            dot.classList.toggle("active", i === index)
        })

        currentTestimonial = index
    }
}

const prevTestimonialBtn = document.getElementById("prevTestimonial")
const nextTestimonialBtn = document.getElementById("nextTestimonial")

if (prevTestimonialBtn) {
    prevTestimonialBtn.addEventListener("click", () => {
        currentTestimonial = (currentTestimonial - 1 + testimonials.length) % testimonials.length
        showTestimonial(currentTestimonial)
    })
}

if (nextTestimonialBtn) {
    nextTestimonialBtn.addEventListener("click", () => {
        currentTestimonial = (currentTestimonial + 1) % testimonials.length
        showTestimonial(currentTestimonial)
    })
}

// Initialize first testimonial if elements exist
if (document.getElementById("testimonialText")) {
    showTestimonial(0)
}

// ============================================
// STAR RATING COMPONENT
// ============================================

class StarRating {
    constructor(containerId, onRating = null) {
        this.container = document.getElementById(containerId)
        this.onRating = onRating
        this.rating = 0
        if (this.container) {
            this.init()
        }
    }

    init() {
        this.container.innerHTML = ""
        for (let i = 1; i <= 5; i++) {
            const star = document.createElement("span")
            star.className = "star"
            star.innerHTML = '<i class="fas fa-star"></i>'
            star.onclick = () => this.setRating(i)
            star.onmouseover = () => this.hoverRating(i)
            star.onmouseout = () => this.unhoverRating()
            this.container.appendChild(star)
        }
    }

    setRating(value) {
        this.rating = value
        this.updateStars()
        if (this.onRating) {
            this.onRating(value)
        }
    }

    hoverRating(value) {
        const stars = this.container.querySelectorAll(".star")
        stars.forEach((star, index) => {
            star.classList.toggle("filled", index < value)
        })
    }

    unhoverRating() {
        this.updateStars()
    }

    updateStars() {
        const stars = this.container.querySelectorAll(".star")
        stars.forEach((star, index) => {
            star.classList.toggle("filled", index < this.rating)
        })
    }

    getRating() {
        return this.rating
    }
}

// Initialize star ratings if containers exist
if (document.getElementById("ratingStars")) {
    new StarRating("ratingStars")
}

if (document.getElementById("eventRatingStars")) {
    new StarRating("eventRatingStars")
}

// ============================================
// UTILITY FUNCTIONS
// ============================================

function formatCurrency(value) {
    return new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD",
    }).format(value)
}

const priceRangeInput = document.getElementById("priceRange")
const priceRangeValue = document.getElementById("priceRangeValue")

if (priceRangeInput && priceRangeValue) {
    const updatePriceRangeValue = () => {
        const value = Number(priceRangeInput.value)
        priceRangeValue.textContent = `Selected: ${formatCurrency(value)}`
    }

    priceRangeInput.addEventListener("input", updatePriceRangeValue)
    updatePriceRangeValue()
}

// ============================================
// EVENT DATE VALIDATION
// ============================================

const eventStartDate = document.getElementById("eventStartDate")
const eventEndDate = document.getElementById("eventEndDate")
const endDateError = document.getElementById("endDateError")

if (eventStartDate && eventEndDate) {
    // Update end date min when start date changes
    eventStartDate.addEventListener("change", () => {
        if (eventStartDate.value) {
            eventEndDate.min = eventStartDate.value
            // If end date is now invalid, clear it and show error
            if (eventEndDate.value && eventEndDate.value < eventStartDate.value) {
                eventEndDate.value = ""
                eventEndDate.classList.add("is-invalid")
                if (endDateError) {
                    endDateError.style.display = "block"
                }
            }
        } else {
            eventEndDate.min = ""
        }
    })

    // Validate end date when it changes
    eventEndDate.addEventListener("change", () => {
        if (eventStartDate.value && eventEndDate.value) {
            if (eventEndDate.value < eventStartDate.value) {
                eventEndDate.classList.add("is-invalid")
                if (endDateError) {
                    endDateError.style.display = "block"
                }
            } else {
                eventEndDate.classList.remove("is-invalid")
                if (endDateError) {
                    endDateError.style.display = "none"
                }
            }
        } else {
            eventEndDate.classList.remove("is-invalid")
            if (endDateError) {
                endDateError.style.display = "none"
            }
        }
    })

    // Set initial min value if start date has a value on load
    if (eventStartDate.value) {
        eventEndDate.min = eventStartDate.value
    }
}

function formatDate(date) {
    return new Intl.DateTimeFormat("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric",
    }).format(new Date(date))
}

function showToast(message, type = "info") {
    const toastId = "toast-" + Date.now()
    const toastHTML = `
        <div id="${toastId}" class="toast fade show" role="alert">
            <div class="toast-body bg-${type}">
                ${message}
            </div>
        </div>
    `

    let container = document.getElementById("toast-container")
    if (!container) {
        container = document.createElement("div")
        container.id = "toast-container"
        container.style.cssText = "position: fixed; top: 20px; right: 20px; z-index: 9999;"
        document.body.appendChild(container)
    }

    container.insertAdjacentHTML("beforeend", toastHTML)

    setTimeout(() => {
        const toast = document.getElementById(toastId)
        if (toast) toast.remove()
    }, 3000)
}

// ============================================
// EARNINGS DISPLAY
// ============================================

function resizeEarningsAmount(element) {
    const minSize = Number(element.dataset.minSize) || 18
    const maxSize = Number(element.dataset.maxSize) || 96
    element.style.fontSize = `${maxSize}px`

    const parent = element.parentElement
    if (!parent) return

    const availableWidth = parent.clientWidth - 16
    if (availableWidth <= 0) return

    const contentWidth = element.scrollWidth
    if (contentWidth === 0) return

    let newSize = maxSize * (availableWidth / contentWidth)
    if (newSize > maxSize) newSize = maxSize
    if (newSize < minSize) newSize = minSize

    element.style.fontSize = `${newSize}px`
}

function initEarningsDisplay() {
    const amounts = document.querySelectorAll(".earnings-amount")
    if (!amounts.length) {
        return
    }

    const adjustAll = () => {
        amounts.forEach((amount) => resizeEarningsAmount(amount))
    }

    adjustAll()
    window.addEventListener("resize", adjustAll)
}

// ============================================
// SMOOTH SCROLL
// ============================================

document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
    anchor.addEventListener("click", function (e) {
        const href = this.getAttribute("href")
        if (href !== "#") {
            e.preventDefault()
            const element = document.querySelector(href)
            if (element) {
                element.scrollIntoView({ behavior: "smooth" })
            }
        }
    })
})

// ============================================
// IMAGE UPLOAD FUNCTIONALITY
// ============================================

// Venue Photos Upload
const venuePhotosUpload = document.getElementById("venuePhotosUpload")
const venuePhotosInput = document.getElementById("venuePhotosInput")
const venuePhotosPreview = document.getElementById("venuePhotosPreview")
let uploadedVenuePhotos = []

if (venuePhotosUpload && venuePhotosInput && venuePhotosPreview) {
    venuePhotosUpload.addEventListener("click", () => {
        venuePhotosInput.click()
    })

    venuePhotosInput.addEventListener("change", (e) => {
        const files = Array.from(e.target.files)
        const remainingSlots = 10 - uploadedVenuePhotos.length

        if (files.length > remainingSlots) {
            showToast(`You can only upload ${remainingSlots} more photo(s). Maximum 10 photos allowed.`, "warning")
            return
        }

        files.forEach((file) => {
            if (file.type.startsWith("image/")) {
                uploadedVenuePhotos.push(file)
                displayVenuePhoto(file)
            }
        })

        // Reset input to allow selecting the same file again
        venuePhotosInput.value = ""
    })

    function displayVenuePhoto(file) {
        const reader = new FileReader()
        reader.onload = (e) => {
            const col = document.createElement("div")
            col.className = "col-md-3 col-sm-4 col-6"
            // Store file reference on the element
            col.dataset.fileName = file.name
            col.dataset.fileSize = file.size
            col.innerHTML = `
                <div class="position-relative">
                    <img src="${e.target.result}" class="img-thumbnail w-100" style="height: 150px; object-fit: cover;" alt="Venue photo">
                    <button type="button" class="btn btn-sm btn-danger position-absolute top-0 end-0 m-1 delete-photo" style="border-radius: 50%; width: 30px; height: 30px; padding: 0;">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            `
            venuePhotosPreview.appendChild(col)

            // Add delete functionality
            const deleteBtn = col.querySelector(".delete-photo")
            deleteBtn.addEventListener("click", (e) => {
                e.stopPropagation()
                // Find and remove the file from array
                const fileName = col.dataset.fileName
                const fileSize = col.dataset.fileSize
                const index = uploadedVenuePhotos.findIndex(f => f.name === fileName && f.size === fileSize)
                if (index > -1) {
                    uploadedVenuePhotos.splice(index, 1)
                }
                col.remove()
            })
        }
        reader.readAsDataURL(file)
    }
}

// Proof of Ownership Upload
const proofUpload = document.getElementById("proofUpload")
const proofInput = document.getElementById("proofInput")
const proofPreview = document.getElementById("proofPreview")
let uploadedProof = null

if (proofUpload && proofInput && proofPreview) {
    proofUpload.addEventListener("click", () => {
        proofInput.click()
    })

    proofInput.addEventListener("change", (e) => {
        const file = e.target.files[0]
        if (!file) return

        // Check file size (5MB = 5 * 1024 * 1024 bytes)
        const maxSize = 5 * 1024 * 1024
        if (file.size > maxSize) {
            showToast("File size exceeds 5 MB limit. Please choose a smaller file.", "warning")
            proofInput.value = ""
            return
        }

        uploadedProof = file
        displayProof(file)

        // Reset input
        proofInput.value = ""
    })

    function displayProof(file) {
        proofPreview.innerHTML = ""

        const isImage = file.type.startsWith("image/")
        const reader = new FileReader()

        reader.onload = (e) => {
            const previewDiv = document.createElement("div")
            previewDiv.className = "position-relative d-inline-block"

            if (isImage) {
                previewDiv.innerHTML = `
                    <img src="${e.target.result}" class="img-thumbnail" style="max-width: 300px; max-height: 300px; object-fit: cover;" alt="Proof of ownership">
                    <button type="button" class="btn btn-sm btn-danger position-absolute top-0 end-0 m-1 delete-proof" style="border-radius: 50%; width: 30px; height: 30px; padding: 0;">
                        <i class="fas fa-times"></i>
                    </button>
                `
            } else {
                previewDiv.innerHTML = `
                    <div class="border rounded p-3 bg-light d-inline-block">
                        <i class="fas fa-file-pdf fs-1 text-danger mb-2"></i>
                        <p class="mb-0 small">${file.name}</p>
                        <p class="mb-0 small text-muted">${(file.size / 1024 / 1024).toFixed(2)} MB</p>
                    </div>
                    <button type="button" class="btn btn-sm btn-danger position-absolute top-0 end-0 m-1 delete-proof" style="border-radius: 50%; width: 30px; height: 30px; padding: 0;">
                        <i class="fas fa-times"></i>
                    </button>
                `
            }

            proofPreview.appendChild(previewDiv)

            // Add delete functionality
            const deleteBtn = previewDiv.querySelector(".delete-proof")
            deleteBtn.addEventListener("click", (e) => {
                e.stopPropagation()
                uploadedProof = null
                proofPreview.innerHTML = ""
            })
        }

        reader.readAsDataURL(file)
    }
}

// National ID Upload (Edit Profile)
const nationalIdUploadArea = document.getElementById("nationalIdUploadArea")
const nationalIdInput = document.getElementById("nationalIdInput")
const nationalIdPreview = document.getElementById("nationalIdPreview")
const nationalIdMaxFiles = 2
const nationalIdFiles = []

if (nationalIdUploadArea && nationalIdInput && nationalIdPreview) {
    nationalIdUploadArea.addEventListener("click", () => nationalIdInput.click())

    nationalIdInput.addEventListener("change", (event) => {
        const files = Array.from(event.target.files || [])
        if (!files.length) return

        const remainingSlots = nationalIdMaxFiles - nationalIdFiles.length
        if (remainingSlots <= 0) {
            showToast("You have already uploaded the maximum of 2 images.", "warning")
            nationalIdInput.value = ""
            return
        }

        if (files.length > remainingSlots) {
            showToast(`You can only upload ${remainingSlots} more image${remainingSlots > 1 ? "s" : ""}.`, "warning")
        }

        files.slice(0, remainingSlots).forEach((file) => {
            if (!file.type.startsWith("image/")) {
                showToast("National ID uploads accept images only.", "warning")
                return
            }

            const reader = new FileReader()
            const id = `${Date.now()}-${Math.random()}`
            reader.onload = (e) => {
                nationalIdFiles.push({
                    id,
                    src: e.target?.result,
                    name: file.name,
                })
                renderNationalIdPreview()
            }
            reader.readAsDataURL(file)
        })

        nationalIdInput.value = ""
    })

    function renderNationalIdPreview() {
        nationalIdPreview.innerHTML = ""
        nationalIdFiles.forEach((photo) => {
            const col = document.createElement("div")
            col.className = "col-6"
            col.innerHTML = `
                <div class="position-relative">
                    <img src="${photo.src}" class="img-thumbnail w-100" style="height: 160px; object-fit: cover;" alt="${photo.name}">
                    <button type="button" class="btn btn-sm btn-danger position-absolute top-0 end-0 m-1 delete-national-id" data-id="${photo.id}" style="border-radius: 50%; width: 28px; height: 28px; padding: 0;">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            `
            nationalIdPreview.appendChild(col)
        })

        nationalIdPreview.querySelectorAll(".delete-national-id").forEach((button) => {
            button.addEventListener("click", (e) => {
                e.stopPropagation()
                const id = button.getAttribute("data-id")
                const index = nationalIdFiles.findIndex((item) => item.id === id)
                if (index > -1) {
                    nationalIdFiles.splice(index, 1)
                    renderNationalIdPreview()
                }
            })
        })
    }
}

// Profile Photo Upload (Edit Profile)
const profilePhotoInput = document.getElementById("profilePhotoInput")
const profilePhotoPreview = document.getElementById("profilePhotoPreview")
const profilePhotoPlaceholder = profilePhotoPreview?.querySelector(".profile-photo-placeholder")
const changeProfilePhotoBtn = document.getElementById("changeProfilePhoto")
const removeProfilePhotoBtn = document.getElementById("removeProfilePhoto")
let currentProfilePhoto = null

if (profilePhotoInput && profilePhotoPreview && profilePhotoPlaceholder && changeProfilePhotoBtn && removeProfilePhotoBtn) {
    const resetProfilePhoto = () => {
        currentProfilePhoto = null
        profilePhotoPlaceholder.innerHTML = '<i class="fas fa-user fa-3x text-muted"></i>'
        profilePhotoPlaceholder.style.backgroundImage = ""
        profilePhotoPlaceholder.classList.add("bg-light")
        profilePhotoPlaceholder.classList.remove("has-photo")
        removeProfilePhotoBtn.classList.add("d-none")
        profilePhotoInput.value = ""
    }

    changeProfilePhotoBtn.addEventListener("click", () => profilePhotoInput.click())
    removeProfilePhotoBtn.addEventListener("click", resetProfilePhoto)

    profilePhotoInput.addEventListener("change", (event) => {
        const file = event.target.files?.[0]
        if (!file) return

        if (!file.type.startsWith("image/")) {
            showToast("Please upload an image file for your profile photo.", "warning")
            resetProfilePhoto()
            return
        }

        const maxSize = 2 * 1024 * 1024
        if (file.size > maxSize) {
            showToast("Profile photo must be 2MB or smaller.", "warning")
            resetProfilePhoto()
            return
        }

        const reader = new FileReader()
        reader.onload = (e) => {
            currentProfilePhoto = {
                name: file.name,
                src: e.target?.result ?? "",
            }

            profilePhotoPlaceholder.style.backgroundImage = `url(${currentProfilePhoto.src})`
            profilePhotoPlaceholder.style.backgroundSize = "cover"
            profilePhotoPlaceholder.style.backgroundPosition = "center"
            profilePhotoPlaceholder.innerHTML = ""
            profilePhotoPlaceholder.classList.remove("bg-light")
            profilePhotoPlaceholder.classList.add("has-photo")
            removeProfilePhotoBtn.classList.remove("d-none")
        }
        reader.readAsDataURL(file)
    })
}

// ============================================
// LOGIN PAGE FUNCTIONALITY
// ============================================

// Password visibility toggle
const loginPasswordToggle = document.getElementById("loginPasswordToggle")
const loginPasswordInput = document.getElementById("loginPassword")
const loginPasswordIcon = document.getElementById("loginPasswordIcon")

if (loginPasswordToggle && loginPasswordInput && loginPasswordIcon) {
    loginPasswordToggle.addEventListener("click", function () {
        const type = loginPasswordInput.getAttribute("type") === "password" ? "text" : "password"
        loginPasswordInput.setAttribute("type", type)

        if (type === "password") {
            loginPasswordIcon.classList.remove("fa-eye")
            loginPasswordIcon.classList.add("fa-eye-slash")
        } else {
            loginPasswordIcon.classList.remove("fa-eye-slash")
            loginPasswordIcon.classList.add("fa-eye")
        }
    })
}

// Login form submission
const loginForm = document.getElementById("loginForm")
if (loginForm) {
    loginForm.addEventListener("submit", function (e) {
        e.preventDefault()
        // Add your login logic here
        console.log("Login form submitted")
        // Example: You can add API call here
        // const email = document.getElementById("loginEmail").value
        // const password = document.getElementById("loginPassword").value
    })
}

// ============================================
// CHOOSE ROLE PAGE FUNCTIONALITY
// ============================================

const roleCards = document.querySelectorAll(".choose-role-card")
let selectedRole = null

if (roleCards.length > 0) {
    // Initialize selected role from the card that has 'selected' class
    const initiallySelected = document.querySelector(".choose-role-card.selected")
    if (initiallySelected) {
        selectedRole = initiallySelected.dataset.role
    }

    roleCards.forEach((card) => {
        card.addEventListener("click", function () {
            // Remove selected class from all cards
            roleCards.forEach((c) => c.classList.remove("selected"))
            // Add selected class to clicked card
            this.classList.add("selected")
            selectedRole = this.dataset.role
        })
    })
}

const chooseRoleSignupBtn = document.getElementById("chooseRoleSignupBtn")
if (chooseRoleSignupBtn) {
    chooseRoleSignupBtn.addEventListener("click", function () {
        if (!selectedRole) {
            alert("Please select a role first")
            return
        }
        // Store selected role and redirect to signup page
        localStorage.setItem("selectedRole", selectedRole)
        window.location.href = "SignUp.html"
    })
}

// ============================================
// SIGNUP PAGE FUNCTIONALITY
// ============================================

// Password visibility toggles
const signupPasswordToggle = document.getElementById("signupPasswordToggle")
const signupPasswordInput = document.getElementById("signupPassword")
const signupPasswordIcon = document.getElementById("signupPasswordIcon")

if (signupPasswordToggle && signupPasswordInput && signupPasswordIcon) {
    signupPasswordToggle.addEventListener("click", function () {
        const type = signupPasswordInput.getAttribute("type") === "password" ? "text" : "password"
        signupPasswordInput.setAttribute("type", type)

        if (type === "password") {
            signupPasswordIcon.classList.remove("fa-eye")
            signupPasswordIcon.classList.add("fa-eye-slash")
        } else {
            signupPasswordIcon.classList.remove("fa-eye-slash")
            signupPasswordIcon.classList.add("fa-eye")
        }
    })
}

const signupConfirmPasswordToggle = document.getElementById("signupConfirmPasswordToggle")
const signupConfirmPasswordInput = document.getElementById("signupConfirmPassword")
const signupConfirmPasswordIcon = document.getElementById("signupConfirmPasswordIcon")

if (signupConfirmPasswordToggle && signupConfirmPasswordInput && signupConfirmPasswordIcon) {
    signupConfirmPasswordToggle.addEventListener("click", function () {
        const type = signupConfirmPasswordInput.getAttribute("type") === "password" ? "text" : "password"
        signupConfirmPasswordInput.setAttribute("type", type)

        if (type === "password") {
            signupConfirmPasswordIcon.classList.remove("fa-eye")
            signupConfirmPasswordIcon.classList.add("fa-eye-slash")
        } else {
            signupConfirmPasswordIcon.classList.remove("fa-eye-slash")
            signupConfirmPasswordIcon.classList.add("fa-eye")
        }
    })
}

// Signup form submission
const signupForm = document.getElementById("signupForm")
if (signupForm) {
    signupForm.addEventListener("submit", function (e) {
        e.preventDefault()

        const password = signupPasswordInput.value
        const confirmPassword = signupConfirmPasswordInput.value

        if (password !== confirmPassword) {
            alert("Passwords do not match")
            return
        }

        // Add your signup logic here
        console.log("Signup form submitted")
        // Example: You can add API call here
        // const email = document.getElementById("signupEmail").value
        // const username = document.getElementById("signupUsername").value
        // const role = localStorage.getItem("selectedRole")
    })
}

// ============================================
// INITIALIZE ON DOM LOAD
// ============================================

document.addEventListener("DOMContentLoaded", () => {
    initTheme()
    initEarningsDisplay()
})