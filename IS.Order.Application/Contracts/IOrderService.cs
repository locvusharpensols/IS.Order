using IS.Order.Application.Features.Orders;

namespace IS.Order.Application.Contracts;

public interface IOrderService
{
    public Task<Guid> CreateOrderAsync(OrderPlacementRequestDto orderPlacementRequestDto,
        CancellationToken cancellationToken);
}