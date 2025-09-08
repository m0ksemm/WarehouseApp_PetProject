using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using Services.CategoriesServices;
using WarehouseWebAPI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureServices(builder.Configuration);



////Repositories
//builder.Services.AddScoped<ICategoryRepository, CategoriesRepository>();


////Services
//builder.Services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
//builder.Services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
//builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
//builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//    b => b.MigrationsAssembly("Entities")));


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/openapi/v1.json","api");
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
