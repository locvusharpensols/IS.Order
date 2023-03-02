namespace IS.Order.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Domain.Entities.Order>
{
    Task<bool> IsUniqueOrderNumberAsync(string orderNumber, CancellationToken cancellationToken);
}