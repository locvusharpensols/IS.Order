using IS.Order.Application.Contracts.Persistence;
using IS.Order.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace IS.Order.Persistence.UnitTest;

public class ApplicationDbContextTests
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection)
            .Options;
        _applicationDbContext = new ApplicationDbContext(dbContextOptions);
        _applicationDbContext.Database.EnsureCreated();
    }

    [Fact]
    public async void CanInsertOrder()
    {
        var order = new Domain.Entities.Order()
        {
            Id = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"),
            OrderNumber = "12345",
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 1000000,
            FundId = 1121
        };

        IOrderRepository repo = new OrderRepository(_applicationDbContext);
        var rs = await repo.AddAsync(order);
        
        rs.ShouldBe(order);
    }
}