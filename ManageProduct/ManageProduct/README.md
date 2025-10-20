# ManageProduct API

A .NET 8 RESTful API for managing products, built with clean architecture, Entity Framework Core, and industry best practices.

## Features
- CRUD operations for products
- Unique auto-incrementing product IDs (starting at 100000)
- Stock increment/decrement endpoints
- Layered architecture (Controller, Service, Repository, Data)
- EF Core code-first with SQL Server (production-ready)
- Global error handling and logging
- OpenAPI/Swagger documentation
- Unit tests (MSTest)

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server instance (local or remote)

### Configuration
Edit `appsettings.json` in the `ManageProduct` project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ManageProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
Replace `YOUR_SERVER` with your SQL Server instance name.

### Database Setup
1. Open a terminal in the `ManageProduct` project directory.
2. Run:
   ```sh
   dotnet ef database update
   ```
   This will create the database and tables.

### Running the API
```sh
dotnet run --project ManageProduct/ManageProduct.csproj
```

### API Endpoints
- `POST /api/products` - Create product (request: ProductGenerateDto, response: ProductDto)
- `GET /api/products` - List products (response: List<ProductDto>, includes StockAvailable)
- `GET /api/products/{id}` - Get product by ID (response: ProductDto)
- `PUT /api/products/{id}` - Update product (request: ProductGenerateDto, response: ProductDto)
- `DELETE /api/products/{id}` - Delete product
- `PUT /api/products/decrement-stock/{id}/{quantity}` - Decrement stock (response: ProductDto)
- `PUT /api/products/add-to-stock/{id}/{quantity}` - Add to stock (response: ProductDto)

### DTOs
- `ProductGenerateDto`: For create/update requests (no Id)
- `ProductDto`: For all responses (includes Id, StockAvailable, etc.)

### Testing
```sh
dotnet test
```

## Project Structure
- `Controllers/` - API endpoints
- `Services/` - Business logic
- `Repositories/` - Data access
- `Entities/` - Data models
- `DTOs/` - Data Transfer Objects
- `Data/` - EF Core DbContext and migrations
- `Helpers/` - Middleware, error handling
- `Interfaces/` - Abstractions for services and repositories
- `ManageProduct.Tests/` - Unit tests (MSTest)

## Notes
- All product responses include `StockAvailable`.
- The API uses SQL Server only (no runtime provider switching).
- Error handling is centralized via middleware.
- Unit tests use MSTest and InMemory database for fast, isolated testing.

## License
MIT
