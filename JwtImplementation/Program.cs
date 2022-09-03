using JwtImplementation.Context;
using JwtImplementation.Interfaces;
using JwtImplementation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var localConnectionString = builder.Configuration.GetConnectionString("LocalDbConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(u => u.UseSqlServer(localConnectionString));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenGenerator, TokenGeneratorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ValidateTokenWithParameters(builder.Services, builder.Configuration);

void ValidateTokenWithParameters(IServiceCollection services,ConfigurationManager configuration)
{
    var JwtSecret = configuration["JwtConfig:Secret"];
    var issuer = configuration["JwtConfig:Issuer"];
    var audience = configuration["JwtConfig:Audiance"];
    var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,

        ValidateAudience = true,
        ValidAudience = audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = symmetricKey,

        ValidateLifetime = true
    };

    builder.Services.AddAuthentication(u =>
    {
        u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(u => u.TokenValidationParameters = tokenValidationParameters);


}
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

app.Run();
