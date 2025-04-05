# Company API


## Purpose

Recruitment task project. Utilises [FastEndpoints](https://fast-endpoints.com)  library. 
Solution architecture based on Nick Chapsas' well-structured FastEndpoints project. 
Database used is [SqlLite](https://www.sqlite.org) accessed via [Dapper ORM](https://github.com/DapperLib/Dapper)

Requirements as specified in [Integration tests ReadMe](./Company.Api.Tests.Integration/ReadMe.md)
are fulfilled by implementing several apis:

- `GET /v1/companies` get list of all companies
- `GET /v1/companies/{{company-id}}` get company by id
- `GET /v1/companies/isin/{{isin}}` get company by ISIN
- `POST /v1/companies` create a company
- `PUT /v1/companies/{{company-id}}` update a company
- `DELETE /v1/companies/{{company-id}}` delete a company
- `POST /v1/login` issue JWT auth token for accessing all the above apis
- `GET /v1/ping` api health check

Detailed documentation of each api can be accessed by running the solution and accessing
Swagger UI (see [How to test Api using Swagger UI], below).


## How to run

Project can be run in Visual Studio, Rider or from the console by executing the following from the solution directory:
```
dotnet run --project Company.Api/Company.Api.csproj
```


## How to test Api using Swagger UI

Run the api (see above). Open [localhost:5001/swagger](https://localhost:5001/swagger) url in the browser. 
You can see the api documentation and can execute test calls from the Swagger UI. 


## How to test Api using .http file

`Company.Api` folder contains a [Company.Api.http](Company.Api/Company.Api.http) file. You can use it
to test the apis using Visual Studio or VS Code with the 
[REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension installed.
Simply open the file with one of the above IDEs and you can execute the requests, as described in the file.
 

## Authorizing the calls

Only the `v1/ping` (health check) and `v1/login` endpoints don't require authorization. All the `companies` endpoints
require a valid Bearer Jwt token in the `Authorization` header. 

To issue tokens, call POST `v1/login` endpoint. For the purpose of the exercise, only two hardcoded
users can log in: `user` or `admin`. The password for both is `Qqqq11111!`. The token is valid for 1 day, and requires
loging in again after expiration, for simplicity.

The .http file will automatically use issued token, after first call to `login` endpoint (see the comments there)

To autorize calls in Swagger UI, first execute `login` api, and then copy/paste the content of the `token` response field
to the `Available authorizations` window (click the `Authorize` button in the upper-right corner of the page)

## How to run Integration tests

Either use Visual Studio test runner, or from the console by executing the following from the solution directory:
```
 cd Company.Api.Tests.Integration
 dotnet test
 ```
 Tests project creates & initializes its (local) database, so no additional setup is required.

 Project is set up to check code coverage using [Coverlet](https://github.com/coverlet-coverage/coverlet) nuget package. 
 I use it together with VS extension [Fine Code Coverage](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage2022)
 (free) that integrates Coverlet reports into IDE.
