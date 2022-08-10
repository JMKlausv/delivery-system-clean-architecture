using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<int>
    {
        public int Id { get; set; } 
        public int ViechleId { get; init; }

        public IEnumerable<OrderProductItemDto> Products { get; init; }

        public float TotalPrice { get; init; }
        public DateTime OrderDate { get; init; }
        public DateTime DeliveryDate { get; init; }
        public OrderAddressDto OrderAddress { get; init; }
    }
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IDeliverySystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _context.Orders.AsNoTracking()
                .Include(o => o.OrderAddress)
                .Include(o => o.Viechle)
                .Include(o => o.Products).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.Id == request.Id);
            if (existingOrder == null)
            {
                throw new NotFoundException("Order", new { Id = request.Id });
            }

            using (IDbContextTransaction transaction = _context.database.BeginTransaction())
            {

                try
                {
                    var orderDto = new OrderDto
                    {
                        ViechleId = request.ViechleId,
                        TotalPrice = request.TotalPrice,
                        DeliveryDate = request.DeliveryDate,
                        OrderDate = request.OrderDate

                    };
                    var newOrder = _mapper.Map<OrderDto, Order>(orderDto);
                    var newOrderAddress = _mapper.Map<OrderAddressDto, OrderAddress>(request.OrderAddress);
                    var newOrderProductItems = _mapper.Map<IEnumerable<OrderProductItemDto>, IEnumerable<OrderProductItem>>(request.Products).ToList();
                    var existingAddress = existingOrder.OrderAddress;
                    var existingProductItems = existingOrder.Products.ToList();
                    //1.......updating order table

                    newOrder.Id = request.Id;
                   
                    _context.Orders.Update(newOrder);
                    await _context.SaveChangesAsync(cancellationToken);
                    var orderId = newOrder.Id;
                    //2.........updating order items

                    for (int j = 0; j < existingProductItems.Count; j++)

                    {

                        var item = existingProductItems[j];

                        item.OrderId = orderId;
                        _context.OrderProductItems.Remove(item);

                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    for (int i = 0; i < newOrderProductItems.Count; i++)
                    {
                        var item = newOrderProductItems[i];
                        item.OrderId = orderId;
                        await _context.OrderProductItems.AddAsync(item);

                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    //3..........updating order address table

                    if (existingAddress == null)
                    {
                        await _context.OrderAddresses.AddAsync(newOrderAddress);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        newOrderAddress.Id = existingOrder.OrderAddress.Id;
                        newOrderAddress.OrderId = orderId;
                        _context.OrderAddresses.Update(newOrderAddress);
                        await _context.SaveChangesAsync(cancellationToken);
                    }


                    transaction.Commit();
                    return orderId;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FailedOperationException("update", "order", ex.Message);
                }

            }

        }




    }

}