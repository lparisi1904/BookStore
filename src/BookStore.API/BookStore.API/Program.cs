using BookStore.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;


var builder = WebApplication.CreateBuilder(args);


//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("ConnStr");
//builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<BookStoreDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// Register database
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// Register Library Service to use it with Dependency Injection in Controllers
//builder.Services.AddTransient<ILibraryService, LibraryService>();


//builder.Services.AddAutoMapper();



//// Register Library Service to use it with Dependency Injection in Controllers
//builder.Services.AddScoped<IService<Author, AuthorDetailsDto>, AuthorDataManager>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "BookStore API",
        Version = "v1"
    });
});

// reference to CORS
builder.Services.AddCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
