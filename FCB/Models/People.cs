using FCB.Models; // Adjust the namespace as necessary
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCB.Models
{
    public class People
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        private string _email = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        }

        
        private string _phoneNumber = string.Empty;
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        //public string PhoneNumber { get; set; } = string.Empty;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                var trimmed = value?.Trim() ?? string.Empty;
                _phoneNumber = trimmed;
            }
        }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
