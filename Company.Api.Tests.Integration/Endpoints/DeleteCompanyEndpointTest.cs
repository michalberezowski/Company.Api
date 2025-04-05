using System.Net;
using System.Net.Http.Headers;
using Company.Api.Repositories;
using Company.Api.Services;
using Flurl;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace Company.Api.Tests.Integration.Endpoints;

public class DeleteCompanyEndpointTest : IClassFixture<TestableCompanyApiFactory<Program>>
{
    private const string Url = "v1/companies";
    private readonly HttpClient _client;
    private readonly DbSeeder _dbSeeder;
    private readonly ICompanyRepository _repo;

    public DeleteCompanyEndpointTest(TestableCompanyApiFactory<Program> apiFactory)
    {
        _dbSeeder = apiFactory.DbSeeder;
        _repo = apiFactory.Get<ICompanyRepository>();
        _client = apiFactory.CreateClient();
        var jwtSecret = apiFactory.Get<IConfiguration>().GetValue<string>("Auth:TokenSecret")!;
        var authService = apiFactory.Get<ICompanyAuthenticationService>();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authService.JwtToken("admin", jwtSecret));
    }

    [Fact]
    public async Task Returns_NoContent_When_Success()
    {
        // confirm seeded company in the database
        var existing = await _repo.GetAsync(_dbSeeder.CompanyIdForDelete);
        existing.ShouldNotBeNull();

        if (existing is null)
        {
            throw new Exception("Company not found in the database");
        }
        // Act
        var response = await _client.DeleteAsync(Url.AppendPathSegment(_dbSeeder.CompanyIdForDelete));

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.NoContent);

        //confirm the company was deleted
        var deleted = await _repo.GetAsync(_dbSeeder.CompanyIdForDelete);
        deleted.ShouldBeNull();
    }

    [Fact]
    public async Task Returns_NotFound_When_CompanyIdNotInTheDB()
    {
        // Act
        var response = await _client.DeleteAsync(Url.AppendPathSegment(Guid.Empty.ToString()));

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}