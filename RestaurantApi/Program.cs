using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestaurantApi.Identity;
using BuisnessLogic;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILogic, Logic>();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

//JWT
builder.Services.AddAuthentication().AddJwtBearer("Customeronlyscheme", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
}).AddJwtBearer("Adminonlyscheme", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtAdmin:Issuer"],
        ValidAudience = builder.Configuration["JwtAdmin:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAdmin:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
//builder.Services.AddAuthorization();

//
builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("AdminOnly",
    //    policy => policy.RequireClaim("Role", "Admin"));

    options.AddPolicy("CustomerOnly",
        policy => policy.RequireClaim("Role", "Customer"));

    var onlyfirstJwtSchemePolicyBuilder = new AuthorizationPolicyBuilder("Customeronlyscheme");
    options.AddPolicy("CustomerOnly", onlyfirstJwtSchemePolicyBuilder
        .RequireClaim("Role", "Customer")
        .Build());

    var onlySecondJwtSchemePolicyBuilder = new AuthorizationPolicyBuilder("Adminonlyscheme");
    options.AddPolicy("AdminOnly", onlySecondJwtSchemePolicyBuilder
        .RequireClaim("Role", "Admin")
        .Build());

});
//

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//jwt end


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();

app.Run();

