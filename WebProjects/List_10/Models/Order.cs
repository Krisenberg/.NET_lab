using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace List_10.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string UserName { get; set; }

        //[ForeignKey("UserId")]
        //public IdentityUser User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        public Order() { }

        public Order(string userName, DateTime orderDate, string firstName, string lastName, string email, string street, string addressLine1, string addressLine2, string city, string country, string zip, string paymentMethod, bool isPaid)
        {
            UserName = userName;
            OrderDate = orderDate;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            Country = country;
            Zip = zip;
            PaymentMethod = paymentMethod;
            IsPaid = isPaid;
        }

        public ICollection<OrderArticle> OrderArticles { get; set; }

    }
}
