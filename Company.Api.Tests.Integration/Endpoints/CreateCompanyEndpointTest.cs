using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Company.Api.Services;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace Company.Api.Tests.Integration.Endpoints;

public class CreateCompanyEndpointTest : IClassFixture<TestableCompanyApiFactory<Program>>
{
    private const string Url = "v1/companies";
    private readonly HttpClient _client;

    public CreateCompanyEndpointTest(TestableCompanyApiFactory<Program> apiFactory)
    {
        _client = apiFactory.CreateClient();
        var jwtSecret = apiFactory.Get<IConfiguration>().GetValue<string>("Auth:TokenSecret")!;
        var authService = apiFactory.Get<ICompanyAuthenticationService>();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authService.JwtToken("admin", jwtSecret));
    }

    [Fact]
    public async Task Returns_Id_When_Success()
    {
        // Act
        var response = await _client.PostAsync(Url, JsonContent.Create(DbSeeder.CompanyForCreate));

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.Created);
        Guid.TryParse(response.Headers.Location?.Segments[3], out var newId)
            .ShouldBeTrue();            // Id is a valid Guid
        newId.ShouldNotBe(Guid.Empty);  // Id has been generated
    }

    [Fact]
    public async Task Returns_BadRequest_When_DuplicatedIsin()
    {
        // Act
        var response1 =
            await _client.PostAsync(Url, JsonContent.Create( DbSeeder.CompanyForCreate with { Isin = DbSeeder.DuplicateIsinForCreate.Value }));

        var response2 =
            await _client.PostAsync(Url, JsonContent.Create( DbSeeder.CompanyForCreate with { Isin = DbSeeder.DuplicateIsinForCreate.Value }));

        // Assert
        response1 // First company should be created
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.Created);
        Guid.TryParse(response1.Headers.Location?.Segments[3], out var newId)
            .ShouldBeTrue();            // Id is a valid Guid
        newId.ShouldNotBe(Guid.Empty);  // Id has been generated

        response2 // Second company should fail
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Theory]
    [MemberData(nameof(GenerateInvalidRequest))]
    public async Task Returns_BadRequest_When_RequestValidationError(JsonContent testContent)
    {
        // Act
        var response = await _client.PostAsync(Url, testContent);

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    public static IEnumerable<object[]> GenerateInvalidRequest()
    {
        //complete validation tests should go to unit tests.
        //we should only test the integration of the validation here, few cases are enough

        // Invalid Name cases
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Name = "" })];                             // Empty name
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Name = "<script>malicious()</script>" })]; // Invalid characters - injection attacks</br>
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Name = "A very long company name " +       // Too long 
            "that exceeds the maximum allowed length for a company name in this test case" })];       

        // Invalid ISIN cases
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Isin = "" })];              // Empty ISIN
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Isin = "AB12345678901" })]; // Too long
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Isin = "AB123456789" })];   // Too short
        yield return [JsonContent.Create(DbSeeder.CompanyForCreate with { Isin = "123456789012" })];  // does not start with letters
    }
}