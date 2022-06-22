using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSWebApiBackEnd.Models
{
    
    [Index(nameof(PartNbr), IsUnique = true)]
    public class Products //Base Class Products 
    {
        [Key]
        public int Id { get; set; } //Primary Key

        [StringLength(30)]
        public string PartNbr { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Column (TypeName = "Decimal(11,2)")]
        public decimal Price { get; set; }

        [StringLength(30)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }
        public int VendorId { get; set; }

        [JsonIgnore] 
        public List<RequestsLine>? RequestsLines { get; set; }

        [JsonIgnore] 
        public Vendors? Vendor { get; set; }
    }
}
