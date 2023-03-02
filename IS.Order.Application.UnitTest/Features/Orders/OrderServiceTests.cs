using AutoMapper;
using IS.Order.Application.Contracts.Persistence;
using IS.Order.Application.Exceptions;
using IS.Order.Application.Features.Orders;
using IS.Order.Application.Profiles;
using IS.Order.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace IS.Order.Application.UnitTest.Features.Orders;

public class OrderServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IOrderRepository> _mockOrderRepository;

    public OrderServiceTests()
    {
        _mockOrderRepository = RepositoryMocks.GetOrderRepository();
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task PlaceOrderTest_Success()
    {
        var service = new OrderService(_mapper, _mockOrderRepository.Object);

        var orderRequest = new OrderPlacementRequestDto
        {
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 1000000,
            FundId = 1121,
            OrderNumber = "12345"
        };
        var result = await service.CreateOrderAsync(orderRequest, CancellationToken.None);

        result.ShouldBe(Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"));
    }
    
    //[Fact]
    public async Task PlaceOrderTest_Fail_InsufficientAmount()
    {
        var service = new OrderService(_mapper, _mockOrderRepository.Object);

        var orderRequest = new OrderPlacementRequestDto
        {
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 0,
            FundId = 1121,
            OrderNumber = "12345"
        };
        var ex = await Assert.ThrowsAsync<ValidationException>(() =>
             service.CreateOrderAsync(orderRequest, CancellationToken.None));

        ex.ValidationErrors[0].ShouldBe("Amount must be greater than 0.");
    }
}