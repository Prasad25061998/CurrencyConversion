using AutoMapper;
using CurrencyConversion.Application.CurrencyHelpers;
using CurrencyConversion.Application.Queries.GetConvertedCurrency;
using CurrencyConversion.Application.Queries.GetRatesQuery;
using CurrencyConversion.Application.Services;
using CurrencyConversion.Domain.Contracts;
using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure;
using CurrencyConversion.Infrastructure.Contracts;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<ICurrencyDbContext, CurrencyDbContext>();
builder.Services.AddScoped<ICurrencyHelper, CurrencyHelper>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetRatesQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetConvertedCurrencyHandler).Assembly);
});
builder.Services.AddHttpClient();
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("ConnectionString"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.FromSeconds(15),
              UseRecommendedIsolationLevel = true,
              DisableGlobalLocks = true 
          }));
builder.Services.AddTransient<ICurrencyBackgroundHourlyJob, CurrencyBackgroundHourlyJob>();


builder.Services.AddHangfireServer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard();

//RecurringJob.AddOrUpdate<ICurrencyBackgroundHourlyJob>(
//    nameof(ICurrencyBackgroundHourlyJob),
//    job => job.Run(),
//    Cron.Hourly);Microsoft.Data.SqlClient.SqlException: 'A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)'


using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate<ICurrencyBackgroundHourlyJob>(
        nameof(ICurrencyBackgroundHourlyJob),
        job => job.Run(),
        Cron.Hourly);
}

app.Run();
