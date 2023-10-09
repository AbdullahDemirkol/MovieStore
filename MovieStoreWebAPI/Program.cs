using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.DataAccess.Extensions;
using MovieStoreWebAPI.Utilities.Logger.Abstract;
using MovieStoreWebAPI.Utilities.Logger.Concrete;
using MovieStoreWebAPI.Utilities.Middlewares;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,          // Tokeni kimler kullanabilir kontrol et
        ValidateIssuer = true,            // Tokenin dagiticisi kim bunu kontrol et
        ValidateLifetime = true,          // Tokenin yasam zamanini kontrol et
        ValidateIssuerSigningKey = true,  // Tokeni sifreleyecegi anahtari kontrol et
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "MovieStoreDb"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ILoggerService, TextFileLogger>();
builder.Services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UserCustomExceptionMiddlerware();

app.MapControllers();

app.Run();
