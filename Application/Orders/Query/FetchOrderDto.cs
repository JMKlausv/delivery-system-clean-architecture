using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Query
{
    public class FetchOrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public int ViechleId { get; set; }
        public string ViechleLicenceNumber { get; set; }

        public IEnumerable<FetchOrderProductItemDto> Products { get; set; }

        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderAddressDto OrderAddress { get; set; }
    }
}
