# Purpose

Api contract-level Integration tests (sometimes called "Subcutaneous Tests" to distinguish 
from *external* integrations tests, but the name isn't widely used) should normally cover 
the requirements of the API as requested and ideally, also edge cases. 
I prefer them in most cases if only feasible - for the reasons given at the bottom of
this readme.

A couple of requirements would benefit from minor clarifications (see the ***todo: clarify*** tags, 
below), but for the purpose of this exercise reasonable assumptions were made and stated.

The tests run in-memory and do not require deployed instance of the API. 
They should be run as a part of the CI/CD pipeline.


## Business Requirements

### Must have

Using this WebApi an end user should be able to:
- Create a Company record specifying the Name, Stock Ticker, Exchange, Isin, and optionally a website url. (***done***)
  - You are not allowed to create two Companies with the same Isin. (***done***)
  - The first two characters of an ISIN must be letters / non numeric.(***done***)
- Retrieve an existing Company by Id(***done***)
- Retrieve a Company by ISIN(***done***)
- Retrieve a collection of all Companies (***done***)(***todo: clarify; assuming non-paginated for the purpose of the exercise***)
- Update an existing Company (***done***)(***todo: clarify; assuming by id***)

- 
Example records:

| Name                | Exchange             | Ticker | Isin         | website                    |
| ------------------- | -------------------- | ------ | ------------ | -------------------------- |
| Apple Inc.          | NASDAQ               | AAPL   | US0378331005 | http://www.apple.com       |
| British Airways Plc | Pink Sheets          | BAIRY  | US1104193065 |                            |
| Heineken NV         | Euronext Amsterdam   | HEIA   | NL0000009165 |                            |
| Panasonic Corp      | Tokyo Stock Exchange | 6752   | JP3866800000 | http://www.panasonic.co.jp |
| Porsche Automobil   | Deutsche B�rse       | PAH3   | DE000PAH0038 | https://www.porsche.com/   |


Code should be testable and have some level of unit test coverage. 
(***done***)

It should run end to end and read and write to a database. 
(***done***)

Please also design the database you would need and provide all SQL scripts 
and source used to create the application
(***done***)

If any additional steps are required to deploy or get the application running 
these should be documented very clearly.
(***done***)


### Bonus points:

Provide a very simple client to call the api and present the results in a 
browser using any client-side web technology you like.
***(Provided `.http` file and Swagger UI for testing, not sure if that counts)***


### Even more points:

Add authentication code to secure the api
(***done***)


## Running the tests

See the  [Company.Api/ReadMe.md](../README.md) for instructions on how to run bothe the api, and the tests.


## Benefits of api contract-level Integration over Unit testing 

(This is just a quick summary, not necessary for the purpose of the exercise)

The benefit of integration tests over unit is that they not only provide code coverage, 
but also test the API as a whole, including the interactions between the components, 
and the backend. Since they are closer to the actual use of the API, they are more 
likely to catch issues that unit tests might miss, they are more informative as to "how
the api should work", and provide a "black-box" type check. By the same virtue, 
they are much less "brittle", thus providing a more reliable test suite with less 
maintenance costs, they should be very rarely changed (if ever - mostly on
breaking version changes). 

As such, they promote refactoring of the system under test rather than hindering it.

Also, every test case provides a lot of code coverage, in a "vertical slice" of 
the application (for example, a simple "create company" integration test cuts accross
all layers, from request handlers to database, and with all validations, services,
cross-cutting concerns as error handling, etc.). They have extremely good ratio of 
test code/actual code, with minimal setup and mocking.

Their disadvantages are performance is much lower than unit, and 
since multiple test cases are working on a shared database, they need to take this into
account and plan accordingly, to prevent interfering with each other.

All in all, they have much more advantages than disadvantages in my opinion and that is 
why I prefer and promote them.

