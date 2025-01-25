using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer name is a requird field!")] // Data Annotations
        [MaxLength(100,ErrorMessage = "Customer name can not exceed 100 characters!")]
        public string ContactName { get; set; } = string.Empty;
        // Generally that string variable will be non-nullable. So we must give it a default value
        /* 
        [Key] // For mentioning that CId is a Primary Key
        public int CId { get; set; } */

        [Required(ErrorMessage = "City is a requird field!")]
        [MaxLength(100, ErrorMessage = "City name can not exceed 100 characters!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is a requird field!")]
        [MaxLength(500, ErrorMessage = "Address can not exceed 500 characters!")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is a requird field!")]
        [MaxLength(100, ErrorMessage = "Email can not exceed 100 characters!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Id!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phonenumber is a requird field!")]
        [MaxLength(20, ErrorMessage = "Phonenumber can not exceed 20 characters!")]
        public string Phone { get; set; } = string.Empty;

        // Cart can be non nullable pr nullable
        public virtual ICollection<Cart>? Carts { get; set; } // Navigation Property (One to Many)
                                                             // Between Customer & Cart
                                                             // Entity Relation is Achieved by this method
    }
}
