using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FCB.Models;

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
        public People Owner { get; set; }=null!;
        [NotMapped]
        public IFormFile? FormFile { get; set; }

    }
}