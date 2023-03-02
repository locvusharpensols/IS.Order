using System.Text;
using IS.Order.API.IntegrationTest.Base;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IS.Order.API.IntegrationTest.Controllers;


public class OrderControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public OrderControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task PlaceOrder_Success()
    {
        var client = _factory.GetAnonymousClient();

        HttpContent content = new StringContent(_testPayload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/Order", content);
        
        var rs = await response.Content.ReadAsStringAsync();

        
        response.EnsureSuccessStatusCode();
        //
        // var result = JsonSerializer.Deserialize<List<OrderListVm>>(responseString);
        //
        // Assert.IsType<List<OrderListVm>>(result);
        // Assert.NotEmpty(result);
    }

    private readonly string _testPayload = "{\n  \"requestId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\n  \"orderNumber\": \"DFC1234\",\n  \"customerId\": \"ABC58891\",\n  \"amount\": 100000000,\n  \"fundId\": 1121,\n  \"orderDate\": \"2023-03-01T17:16:40Z\"\n}";
    
}