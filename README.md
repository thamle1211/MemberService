MemberService

MemberService is a Mock Project that provides RESTful API for managing Member information, built using Clean Architecture with the following layers: API, Application, Domain, and Infrastructure.
Because it's a Mock Project, there's no connection to real db and jwt login didn't go to db to verify also, it's only mock 

🔹 Features

CRUD Members: Create, Read, Update, Delete
Search Members with filters, paging, and sorting
Validation using FluentValidation (email, birthdate, firstname, lastname etc.)
JWT Authentication & Authorization
Unit Tests with xUnit + Moq

🔹 Project Structure
src/
 ├── MemberService.Api          # Web API
 ├── MemberService.Application  # Application layer: Commands, Queries, DTOs, Validators
 ├── MemberService.Domain       # Entities, Domain interfaces
 └── MemberService.Infrastructure # EF Core, Repositories
tests/
 └── MemberService.Tests        # Unit tests

🔹 Getting Started
Clone the repository
git clone <repo-url>
cd MemberService

Build the project
dotnet build

Run the API
cd src/MemberService.Api
dotnet run

The API will run by default on:
http://localhost:5014

🔹 Docker
Build the image:
docker build -t memberservice .

Run the container:
docker run -p 8080:8080 memberservice

🔹 Testing
dotnet test

✔ Tests validators, handlers

🔹 Technologies
.NET 9 Web API
EF Core 9
MediatR
FluentValidation
JWT Authentication
Docker => just a simple sample file for example
xUnit + Moq