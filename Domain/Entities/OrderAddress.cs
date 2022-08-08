


namespace Domain.Entities
{
    public class OrderAddress
    {
        public int Id { get; set; } 
    
        public string Region { get; set; }
      
        public string City { get; set; }
      
        public string? SpecificAddress { get; set; }
      
        public string? CustomerEmail { get; set; }
      
        public string? Phone { get; set; }  
       public virtual Order Order { get; set; }  
        public int OrderId { get; set; }    

    }
}
