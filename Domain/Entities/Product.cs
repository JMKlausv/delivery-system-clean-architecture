
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product 
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        
        public float price { get; set; }
       
        public int quantity { get; set; }
        public string? imageUrl { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
