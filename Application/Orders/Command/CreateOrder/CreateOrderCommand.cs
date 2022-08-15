using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Orders.Command.CreateOrder
{
    public record CreateOrderCommand : IRequest<int>
    {
        public int ViechleId { get; set; }

        public IEnumerable<OrderProductItemDto> Products { get; set; }
        public float? TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderAddressDto OrderAddress { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;
        private readonly IMapper _mapper;


        public CreateOrderCommandHandler(IDeliverySystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
         
        }
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
           

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
                    var order = _mapper.Map<OrderDto, Order>(orderDto);
                    var orderAddress = _mapper.Map<OrderAddressDto, OrderAddress>(request.OrderAddress);
                    var orderProductItems = _mapper.Map<IEnumerable<OrderProductItemDto>, IEnumerable<OrderProductItem>>(request.Products);

                    //1....save order using saveorder Dto and get id 

                    await _context.Orders.AddAsync(order);

                    await _context.SaveChangesAsync(cancellationToken);
                    var orderId = order.Id;
                    //2....save productitem 

                    orderProductItems.ToList().ForEach(async orderItem =>
                      {
                          orderItem.OrderId = orderId;
                          await _context.OrderProductItems.AddAsync(orderItem);
                          // await _context.SaveChangesAsync( cancellationToken);
                      });
                    await _context.SaveChangesAsync(cancellationToken);

                    //3....save address 
                    orderAddress.OrderId = orderId;
                    await _context.OrderAddresses.AddAsync(orderAddress);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                    return orderId;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FailedOperationException("create", "order", ex.Message);
                }

            }

        }
    }
}
