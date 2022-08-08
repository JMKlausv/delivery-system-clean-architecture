using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class OrderAddressDto :IMapFrom<OrderAddress>
    {
        public string Region { get; set; }
       
        public string City { get; set; }
      
        public string? SpecificAddress { get; set; }
       
        public string? CustomerEmail { get; set; }
       
        public string? Phone { get; set; }
    }
}