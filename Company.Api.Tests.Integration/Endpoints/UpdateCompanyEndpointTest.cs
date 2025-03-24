using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Company.Api.Repositories;
using Company.Api.Services;
using Flurl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Company.Api.Tests.Integration.Endpoints;

public class UpdateCompanyEndpointTest : IClassFixture<TestableCompanyApiFactory<Program>>
{
    private const string Url = "v1/companies";
    private readonly HttpClient _client;
    private readonly DbSeeder _dbSeeder;
    private readonly ICompanyRepository _repo;

    public UpdateCompanyEndpointTest(TestableCompanyApiFactory<Program> apiFactory)
    {
        _dbSeeder = apiFactory.DbSeeder;
        _repo = apiFactory.Services.GetRequiredService<ICompanyRepository>();
        _client = apiFactory.CreateClient();
        var jwtSecret = apiFactory.Get<IConfiguration>().GetValue<string>("Auth:TokenSecret")!;
        var authService = apiFactory.Get<ICompanyAuthenticationService>();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authService.JwtToken("admin", jwtSecret));
    }


    [Fact]
    public async Task Returns_Ok_When_Success()
    {
        // confirm seeded company in the database
        var existing = await _repo.GetAsync(_dbSeeder.CompanyIdForUpdate);
        existing.ShouldNotBeNull();

        // Act
        var response = await _client.PutAsync(Url.AppendPathSegment(_dbSeeder.CompanyIdForUpdate), 
            JsonContent.Create(DbSeeder.CompanyForUpdate));

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.OK);
        var updated = await _repo.GetAsync(_dbSeeder.CompanyIdForUpdate);
        updated.ShouldNotBeNull();
        updated.Name       .ShouldBe(DbSeeder.CompanyForUpdate.Name);
        updated.Exchange   .ShouldBe(DbSeeder.CompanyForUpdate.Exchange);
        updated.Isin       .ShouldBe(DbSeeder.CompanyForUpdate.Isin);
        updated.StockTicker.ShouldBe(DbSeeder.CompanyForUpdate.StockTicker);
        updated.Website    .ShouldBe(DbSeeder.CompanyForUpdate.Website);
    }

    [Fact]
    public async Task Returns_BadRequest_When_UpdatedIsinNotUnique()
    {
        // confirm seeded company in the database
        var existing = await _repo.GetAsync(_dbSeeder.CompanyIdForUpdate);
        existing.ShouldNotBeNull();

        // Act
        var response = await _client.PutAsync(Url.AppendPathSegment(_dbSeeder.CompanyIdForUpdate),
            JsonContent.Create(DbSeeder.CompanyForUpdate with {Isin = DbSeeder.CompanyForUpdateDup.Isin}));

        // Assert
        response
            .ShouldNotBeNull()
            .StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}