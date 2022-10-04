using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false, // perlu set yaa biar ga invalid token nyachh
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:SecretKey").Value)),
        ClockSkew = TimeSpan.Zero // clockSkew akan menentukan berapa waktu yg diperlukan untuk memverifikasi token... default nya = 5 mins.. kalau expires 5 min + default clockSkew maka perlu 10 menit agar token expired
                                  // jadi.. set clockSkew menjadi zero.. agar expires nya exact pada berapa menit yg diperlukan agar expired
    };
});


builder.Services.AddControllers();
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

//app.UseHttpsRedirection();

Console.WriteLine("cek typeof: " + typeof(JsonStringEnumConverter)); // dengan ngeprint full nama package classnya

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
