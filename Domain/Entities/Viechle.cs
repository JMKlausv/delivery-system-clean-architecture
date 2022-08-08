
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Viechle
    {
       
        public int Id { get; set; } 
        public string Model { get; set; }
        public string Type { get; set; }
        public string  LicenceNumber { get; set; }
       
    }
}
