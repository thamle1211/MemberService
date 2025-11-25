# ============================
# 1. Build Stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution & restore
COPY MemberService.sln .
COPY src/MemberService.Api/MemberService.Api.csproj src/MemberService.Api/
COPY src/MemberService.Application/MemberService.Application.csproj src/MemberService.Application/
COPY src/MemberService.Domain/MemberService.Domain.csproj src/MemberService.Domain/
COPY src/MemberService.Infrastructure/MemberService.Infrastructure.csproj src/MemberService.Infrastructure/

RUN dotnet restore

# copy source and build
COPY . .
RUN dotnet publish src/MemberService.Api/MemberService.Api.csproj -c Release -o /app/publish

# ============================
# 2. Runtime Stage
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MemberService.Api.dll"]