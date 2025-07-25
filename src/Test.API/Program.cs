using Test.Core.Repositories.Implementations;
using Test.Core.Repositories.Interfaces;
using Test.Core.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// �����������
builder.Services.AddScoped<IFieldRepository, FieldRepository>();

// �������
builder.Services.AddScoped<FieldService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
