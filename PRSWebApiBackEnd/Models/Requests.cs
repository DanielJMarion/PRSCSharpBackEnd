using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Status { get; set; }
        
        [StringLength(20)]
        public string DeliveryMode { get; set; }

        [StringLength(80)]
        public string? RejectionReason { get; set; }
        public int UserId { get; set; }
    }
}
