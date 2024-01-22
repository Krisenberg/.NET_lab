using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace List_10.ViewModels
{
    public class OrderViewModel
    {
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "First name must be at most 20 characters long")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Last name must be at most 20 characters long")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[\w-\.]+@([\w -]+\.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        [MinLength(3, ErrorMessage = "Street must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Street must be at most 20 characters long")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Address line 1 is required.")]
        [MinLength(1, ErrorMessage = "Address line 1 must be at least 1 characters long")]
        [MaxLength(10, ErrorMessage = "Address line 1 must be at most 10 characters long")]
        public string AddressLine1 { get; set; }
        [MinLength(1, ErrorMessage = "Address line 2 must be at least 1 characters long")]
        [MaxLength(10, ErrorMessage = "Address line 2 must be at most 10 characters long")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [MinLength(3, ErrorMessage = "City must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "City must be at most 20 characters long")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        [MinLength(3, ErrorMessage = "Country must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Country must be at most 20 characters long")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Zip is required.")]
        [MinLength(3, ErrorMessage = "Zip code must be at least 3 characters long")]
        [MaxLength(10, ErrorMessage = "Zip code must be at most 10 characters long")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Payment method is required.")]
        public string PaymentMethod { get; set; }

    }
}
