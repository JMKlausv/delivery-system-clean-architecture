using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class OrderDto : IMapFrom<Order>
    {
        public int ViechleId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
       
    }
}