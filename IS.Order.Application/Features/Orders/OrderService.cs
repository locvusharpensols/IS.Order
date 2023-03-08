using AutoMapper;
using IS.Order.Application.Contracts;
using IS.Order.Application.Contracts.Persistence;
using IS.Order.Application.Features.Orders.Validator;

namespace IS.Order.Application.Features.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public OrderService(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<Domain.Entities.Order> GetByGuid(Guid guid){
       return await _orderRepository.GetByIdAsync(guid);
    }
    
    public async Task<Guid> CreateOrderAsync(OrderPlacementRequestDto orderPlacementRequestDto, CancellationToken cancellationToken)
    {
        var @order = _mapper.Map<Domain.Entities.Order>(orderPlacementRequestDto);

        var validation = new OrderPlacementRequestValidator(_orderRepository);
        var validationResult = await validation.ValidateAsync(@order, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        @order = await _orderRepository.AddAsync(@order);
        return @order.Id;
    }

    public Task RemoveOrder(Domain.Entities.Order order)
        =>  _orderRepository.DeleteAsync(order);

    public Task<IReadOnlyList<Domain.Entities.Order>> GetAllOrders()
        => _orderRepository.ListAllAsync();
}