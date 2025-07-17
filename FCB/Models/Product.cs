using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; } = 0.0m;
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        [ValidateNever]
        public People? Owner { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}