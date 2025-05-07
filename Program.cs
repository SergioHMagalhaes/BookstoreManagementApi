using BookstoreManagementApi.Data;
using BookstoreManagementApi.Services.Author;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthorInterface, AuthorServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerFeature?.Error != null)
        {
            logger.LogError(exceptionHandlerFeature.Error, "Erro não tratado");

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Erro interno no servidor",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            });
        }
    });
});

app.Run();
