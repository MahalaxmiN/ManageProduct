using ManageProduct.Data;
using ManageProduct.Repositories;
using ManageProduct.Services;
using ManageProduct.Helpers;
using Microsoft.EntityFrameworkCore;
using ManageProduct.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
        policy.WithOrigins("http://localhost", "https://localhost")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CORS");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
