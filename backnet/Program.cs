using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

  var JwtSettings = builder.Configuration.GetSection("JwtSettings");

  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = JwtSettings["Issuer"],
    ValidateAudience = true,
    ValidAudience = JwtSettings["Audience"],
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"])),
    ClockSkew = TimeSpan.Zero
  };

  options.Events = new JwtBearerEvents
  {
    OnMessageReceived = context =>
    {
      var token = context.HttpContext.Request.Cookies["access_token"];
      
      if (!string.IsNullOrEmpty(token))
      {
        context.Token = token;
      }
      
      return Task.CompletedTask;
    }
  };
});
builder.Services.AddAuthorization();
builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddValidatorsFromAssemblyContaining<T>();
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();