using BookStore.Domain.Interfaces;
using BookStore.Domain.Services;
using BookStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using AutoMapper;
using BookStore.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
//Add services to the container.
//------------------------------

// Mappatura del connectionString al database...
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// => (AddScoped) vengono creati una volta per richiesta client (connessione).
// da utilizzare con EF core..
// Utilizzo Scoped perché gli oggetti vengono creati una sola volta per richiesta,
// in questo modo evito di utilizzare più memoria, una volta che farà sempre riferimento alla stessa locazione di memoria.
//

// Registro i servizi per utilizzarli con Dependency Injection nei Controllori
//
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookService, BookService>();


//builder.Services.AddAutoMapper();
//// Register Library Service to use it with Dependency Injection in Controllers
//builder.Services.AddScoped<IService<Author, AuthorDetailsDto>, AuthorDataManager>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var booksclient = "bookclient";

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: booksclient,
    policy =>
    {
        policy
     .AllowAnyHeader()
     .AllowAnyMethod()
     .AllowAnyOrigin();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// reference to CORS
builder.Services.AddCors();


app.UseHttpsRedirection();

app.UseCors(booksclient);

app.UseAuthorization();

app.MapControllers();

app.Run();
