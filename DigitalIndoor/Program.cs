using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Mapping;
using DigitalIndoor.Middlewares;
using DigitalIndoor.Models.DB;
using DigitalIndoor.Models.Options;
using DigitalIndoor.Services;
using DigitalIndoor.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorResponse = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .SelectMany(x => x.Value.Errors.Select(e => new ValidationErrorDto(x.Key, e.ErrorMessage)))
                .ToList();

            return new BadRequestObjectResult(errorResponse);
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description =  "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                {
                                        {
                                            new OpenApiSecurityScheme
                                            {
                                                Reference = new OpenApiReference
                                                {
                                                    Type = ReferenceType.SecurityScheme,
                                                    Id = "Bearer"
                                                },
                                                Scheme = "oauth2",
                                                Name = "Bearer",
                                                In = ParameterLocation.Header,

                                            },
                                            new List<string>()
                                        }
                                });
    });

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

//builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

//Implementation
builder.Services.AddSingleton<IPasswordHasher<BaseUser>, PasswordHasher<BaseUser>>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<TokenManagerMiddleware>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddTransient<ITokenManager, TokenManager>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IDemoService, DemoService>();

//Service for AutoMapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// JWT Bearer
var jwtSection = builder.Configuration.GetSection("jwt");
var jwtOptions = new JwtOptions();
jwtSection.Bind(jwtOptions);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.Configure<JwtOptions>(jwtSection);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ApiLogHandlerMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<TokenManagerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var directory = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
if (!Directory.Exists(directory))
    Directory.CreateDirectory(directory);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(directory),
    RequestPath = "/api/storage"
});

app.Run();



