using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSWebApiBackEnd.Models
{

    public class RequestsLine //Base Class RequestsLine 
    {
        [Key]
        public int Id { get; set; } //Primary Key
        public int RequestId { get; set; }        
        public int ProductId { get; set; }    
        public int Quantity { get; set; }

        [JsonIgnore] 
        public Requests? Requests { get; set; }

        [JsonIgnore] 
        public Products? Product { get; set; }

    }
}

