using FluentValidation;
using IS.Order.Application.Contracts.Persistence;

namespace IS.Order.Application.Features.Orders.Validator;

public class OrderPlacementRequestValidator : AbstractValidator<Domain.Entities.Order>
{
    private IOrderRepository _orderRepository;

    public OrderPlacementRequestValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        
        RuleFor(p => p.OrderNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(BeUniqueOrderNumber!).WithMessage("An order with the same number already exists.");

        RuleFor(p => p.CustomerId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Amount)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.FundId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.OrderDate).NotEmpty().WithMessage("{PropertyName} is required.").NotNull()
            .GreaterThan(DateTime.Now);
        
    }
    
    private async Task<bool> BeUniqueOrderNumber(string orderNumber, CancellationToken cancellationToken)
    {
        return !(await _orderRepository.IsUniqueOrderNumberAsync(orderNumber, cancellationToken));
    }
}