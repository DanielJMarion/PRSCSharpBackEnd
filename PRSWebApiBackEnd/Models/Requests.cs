using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSWebApiBackEnd.Models
{

    public class Requests //Base Class Requests 
    {
        [Key]
        public int Id { get; set; } //Primary Key

        [StringLength(80)]
        public string Description { get; set; }

        [StringLength(80)]
        public string Justification { get; set; }

        [Column(TypeName = "Decimal(11,2)")]
        public decimal Total { get; set; }

        [StringLength(10)]
        public string Status { get; set; } = "NEW";

        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";

        [StringLength(80)]
        public string? RejectionReason { get; set; }
        public int UserId { get; set; }
        
        [JsonIgnore] 
        public User? User { get; set; }

        [JsonIgnore]
        public List<RequestsLine>? RequestsLines { get; set; }
    }
}
