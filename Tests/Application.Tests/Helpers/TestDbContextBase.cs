using System;
using  Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Application.Tests.Helpers;
public abstract class TestDbContextBase : IDisposable{
    protected DeliverySystemDbContext _context;
    private readonly Mock<IMediator> _mediatorMock;
    public TestDbContextBase()
    {
        var options = new DbContextOptionsBuilder<DeliverySystemDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _mediatorMock = new Mock<IMediator>();
        _context = new DeliverySystemDbContext(options , _mediatorMock.Object);
        _context.database.EnsureCreated();
    }
    public void Dispose(){
        _context.database.EnsureDeleted();
        _context.Dispose();
    }

}