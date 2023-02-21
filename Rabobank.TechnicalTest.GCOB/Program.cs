using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton(EntityDataStore<Customer>.Instance);
builder.Services.AddSingleton(EntityDataStore<Address>.Instance);
builder.Services.AddSingleton(EntityDataStore<Country>.Instance);

builder.Services.AddSingleton(new CustomerDataStoreInitializer());

// Some dummy data for the satisfaction of running the API and seeing some results in Swagger (Not that a green tick doesn't make me happy).
builder.Services.AddSingleton(new AddressDataStoreInitializer());
builder.Services.AddSingleton(new CountryDataStoreInitializer());

builder.Services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddScoped<IAddressRepository, InMemoryAddressRepository>();
builder.Services.AddScoped<ICountryRepository, InMemoryCountryRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

