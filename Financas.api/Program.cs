using System.Reflection.Metadata;
using Financas.api.Data;
using Financas.api.Handlers;
using Financas.core.Handlers;
using Financas.core.Requests;
using Financas.core.Requests.Categories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 0))));

builder.Services.AddTransient<ICategoryHandler, CategoryHandler >();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler >();

var app = builder.Build();




app.MapGet("/", (GetCategoryByIdRequest request, ICategoryHandler handler) => handler.GetCategoryByIdAsync(request));

app.Run();
