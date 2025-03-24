using Company.Api.Contracts.Responses;
using Company.Api.Utils.Extensions;
using Shouldly;

namespace Company.Api.Tests.Integration.Endpoints;

public class PingEndpointTest(TestableCompanyApiFactory<Program> factory) : IClassFixture<TestableCompanyApiFactory<Program>>
{
    private const string Url = "v1/ping";

    private readonly HttpClient _client = factory.CreateClient();
 
    [Fact]
    public async Task Ping_Endpoint_ReturnsPong()
    {
        // Act
        var response = await _client.GetAsync(Url);

        // Assert
        response.ShouldNotBeNull().EnsureSuccessStatusCode();

        var responseObj = (await response.Content.ReadAsStringAsync())
            .Deserialize<PingResponse>();

        responseObj.ShouldNotBeNull().Message.ShouldBe("Pong");
    }


}
