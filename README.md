# MemberService

**MemberService** is a mock project that provides RESTful APIs for managing member information, built using **Clean Architecture** with the following layers: **API**, **Application**, **Domain**, and **Infrastructure**.  
Because this is a mock project, there is **no real database connection**, and **JWT login does not verify credentials against a database**—it is only a mock implementation.

---

## 🔹 Features

- **CRUD Members**: Create, Read, Update, Delete  
- **Search Members** with filtering, paging, and sorting  
- **Validation** using FluentValidation (email, birthdate, firstname, lastname, etc.)  
- **JWT Authentication & Authorization**  
- **Unit Tests** with xUnit + Moq  

---

## 🔹 Project Structure

```text
src/
 ├── MemberService.Api                # Web API
 ├── MemberService.Application        # Application layer: Commands, Queries, DTOs, Validators
 ├── MemberService.Domain             # Entities, Domain interfaces
 └── MemberService.Infrastructure     # EF Core, Repositories

tests/
 └── MemberService.Tests              # Unit tests

```
---

## 🔹 Getting Started

- Clone the repository  
  ```bash
  git clone <repo-url>
  cd MemberService
  ```

- Build the project
 ```bash
 dotnet build
 Run the API
 cd src/MemberService.Api
 dotnet run
```
- The API will run by default on:
```bash
 http://localhost:5014
```
---

## 🔹 Docker

- Build the Docker image
```bash
 docker build -t memberservice .
 Run the container
 docker run -p 8080:8080 memberservice
```
---

## 🔹 Testing

```bash
dotnet test
✔ Includes tests for validators and handlers.
```
---

## 🔹 Technologies Used
.NET 9 Web API

EF Core 9

MediatR

FluentValidation

JWT Authentication

Docker (simple sample file)

xUnit + Moq

