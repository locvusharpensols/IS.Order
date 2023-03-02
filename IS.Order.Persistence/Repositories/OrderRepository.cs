using IS.Order.Application.Contracts.Persistence;

namespace IS.Order.Persistence.Repositories;

public class OrderRepository : BaseRepository<Domain.Entities.Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsUniqueOrderNumberAsync(string orderNumber, CancellationToken cancellationToken)
    {
        var match = DbContext.Orders != null && DbContext.Orders.Any(o => o.OrderNumber!.Equals(orderNumber));

        return Task.FromResult(match);
    }
}