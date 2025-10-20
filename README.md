# ManageProduct API

A .NET 8 RESTful API for managing products, built with clean architecture, EF Core, and best practices.

## Features
- CRUD operations for products
- Unique 6-digit product IDs (distributed-safe)
- Stock increment/decrement endpoints
- Layered architecture (Controller, Service, Repository, Data)
- EF Core code-first (InMemory and SQL Server support)
- Global error handling and logging
- OpenAPI/Swagger documentation
- Unit tests (xUnit)

## Getting Started

### Prerequisites
- .NET 8 SDK

### Running with InMemory Database (default)
```sh
dotnet run --project ManageProduct/ManageProduct.csproj
```

### Running with SQL Server
1. Update `appsettings.json` with your connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ManageProductDb;Trusted_Connection=True;"
  },
  "DbProvider": "SqlServer"
}
```
2. Add migration and update database:
```sh
cd ManageProduct
 dotnet ef migrations add InitialCreate
 dotnet ef database update
```
3. Run the API:
```sh
dotnet run --project ManageProduct/ManageProduct.csproj
```

### API Endpoints
- `POST /api/products` - Create product
- `GET /api/products` - List products
- `GET /api/products/{id}` - Get product by ID
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product
- `PUT /api/products/decrement-stock/{id}/{quantity}` - Decrement stock
- `PUT /api/products/add-to-stock/{id}/{quantity}` - Add to stock

### Testing
```sh
dotnet test
```

## Project Structure
- `Entities/` - Data models
- `Data/` - EF Core DbContext
- `Repositories/` - Data access
- `Services/` - Business logic
- `Controllers/` - API endpoints
- `Helpers/` - Middleware, utilities
- `ManageProduct.Tests/` - Unit tests

## License
MIT
