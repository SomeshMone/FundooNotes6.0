using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System.Text;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// add loggers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerBusiness, CustomerBusiness>();
builder.Services.AddScoped<ICustomerRepository, CustomerRespository>();

builder.Services.AddScoped<INotesBusiness,NotesBusiness>();
builder.Services.AddScoped<INotesRepository,NotesRepository>();

builder.Services.AddScoped<ILabelBusiness,LabelBusiness>();
builder.Services.AddScoped<ILabelRepository,LabelRepository>();

builder.Services.AddScoped<ICollaboratorBusiness,CollaboratorBusiness>();
builder.Services.AddScoped<ICollaboratorRepository,CollaboratorRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionString:FundooDBp"]));
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"]; });



builder.Services.AddSwaggerGen(
    option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"]; });
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
builder.Services.AddMassTransitHostedService();

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
app.UseSession(); //for session purpose

app.MapControllers();

app.Run();
