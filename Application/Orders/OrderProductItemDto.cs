using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class OrderProductItemDto : IMapFrom<OrderProductItem>
    {
          public int ProductId { get; set; }
        public int quantity { get; set; }
    }
}
