using ManagerProductsAPI.Data;
using ManagerProductsAPI.Extras;
using ManagerProductsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //Cria��o do Cont�iner 

// Add services to the container.
builder.Services.AddControllers(); // Adiciona suporte a controladores

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(); // Registra o gerador de Documenta��o

builder.Services.AddDbContext<ProductsDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsDB")));

builder.Services.AddScoped<IProductService, ProductService>(); // Registra um servi�o para inje��o de depend�ncia, AddScoped funciona por requisi��o

// Adicionando o Log
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Realizar uma personaliza��o do ModelState 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(err => err.ErrorMessage)
            });

        return new BadRequestObjectResult(errors);
    };
});

var app = builder.Build();

// DAQUI PARA BAIXO � QUASE TODOS MIDDLEWARES

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Middleware para documenta��o Swagger
    app.UseSwaggerUI(); //Interface Swagger
}

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
