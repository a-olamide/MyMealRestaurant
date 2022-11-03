using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyMealRestaurant.Configurations;
using MyMealRestaurant.Models;
using MyMealRestaurant.Services;
using Serilog;
using System.Text;

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
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
};

var contactinfo = new OpenApiContact()
{
    Name = "Olamide Akinoso",
    Email = "a.olamide@wallzandqueenltd.com",
    Url = new Uri("https://mybankstatement.net")
};

var license = new OpenApiLicense()
{
    Name = "Free License",
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "User API with JWT Authentication",
    Description = "User API with JWT Authentication",
    Contact = contactinfo,
    License = license
};

var builder = WebApplication.CreateBuilder(args);
{
    //// Add services to the container.
    //var configuration = new ConfigurationBuilder()
    //// Read from your appsettings.json.
    //.AddJsonFile("appsettings.json")
    //.Build();

    //Log.Logger = new LoggerConfiguration()
    //    .ReadFrom.Configuration(configuration)
    //    .CreateLogger();

    builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
    _ = loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    
    {
        options.SwaggerDoc("v1", info);
        options.AddSecurityDefinition("Bearer", securityScheme);
        options.AddSecurityRequirement(securityReq);
    });
    builder.Services.AddScoped<IMealService, MealService>();
    builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

    builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // for dev
            ValidateAudience = false, // for dev
            RequireExpirationTime = false, // for dev -- needs to be updated when refresh token is added
            ValidateLifetime = true
        };
    });
    builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<AppDbContext>();
}



var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    Log.Information("Starting up");
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    //app.UseSerilogRequestLogging();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

