using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace List_10.ViewModels
{
    public class OrderViewModel
    {
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First name must contain between 2 and 30 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last name must contain between 2 and 30 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Email must contain between 6 and 30 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must contain between 2 and 30 characters.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Address line 1 is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Address line 1 must contain between 1 and 30 characters.")]
        public string AddressLine1 { get; set; }
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Address line 2 must contain between 1 and 30 characters.")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "City must contain between 3 and 30 characters.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Country must contain between 3 and 30 characters.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Zip is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Zip must contain between 3 and 30 characters.")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Payment method must contain between 3 and 30 characters.")]
        public string PaymentMethod { get; set; }

    }
}
