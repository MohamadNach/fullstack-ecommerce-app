using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Repository;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ecommerce.Service.src.AuthService;
using Ecommerce.Infrastructure.src.Repository.Service;
using Ecommerce.Service.src.UserService;
using Newtonsoft.Json.Converters;
using Microsoft.OpenApi.Models;
using Ecommerce.Infrastructure.src;
using Ecommerce.Service.src.AddressService;
using Ecommerce.Service.src.CategoryService;
using Ecommerce.Service.src.ShipmentService;
using Ecommerce.Service.src.PaymentService;
using Ecommerce.Service.src.ReviewService;
using Ecommerce.Service.src.OrderService;
using Ecommerce.Service.src.OrderItemService;
using Ecommerce.Service.src.ProductService;
using Ecommerce.Service.src.ProductColorService;
using Ecommerce.Service.src.ProductSizeService;
using Ecommerce.Service.src.ProductImageService;
using Asp.Versioning;
using System.Security.Claims;
using Ecommerce.Service.src.UserAddressService;
using Ecommerce.Service.src.CartItemService;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173",
                "https://66f52fb39f4c78b654ec6788--ecommerce-nachawati.netlify.app/",
                "https://ecommerce-dev-app.azurewebsites.net")

                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new() { Title = "Ecommerce", Version = "v1" });
        options.SchemaFilter<EnumSchemaFilter>();
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });

builder.Services.AddApiVersioning(option =>
{

    option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
    option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
    option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.
    option.ApiVersionReader = new UrlSegmentApiVersionReader();
    // //------------------------------------------------//
    // option.ApiVersionReader = ApiVersionReader.Combine(
    // //     new QueryStringApiVersionReader("api-version"),
    // //     new HeaderApiVersionReader("X-Version"),
    //     new MediaTypeApiVersionReader("ver")); //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
    // //                                            //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; //The say our format of our version number “‘v’major[.minor][-status]”
    options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
});

// Add database context into app
builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information)
    // .AddInterceptors(new TimeStampInterceptor())
    .UseNpgsql(builder.Configuration.GetConnectionString("localhost"))
    .UseSnakeCaseNamingConvention());


// Dependency Injection
builder.Services.AddScoped<IAddressManagement, AddressManagement>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAuthManagement, AuthManagement>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryManagement, CategoryManagement>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemManagement, CartItemManagement>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartItemManagement, CartItemManagement>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemManagement, OrderItemManagement>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentManagement, PaymentManagement>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductColorManagement, ProductColorManagement>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductImageManagement, ProductImageManagement>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductManagement, ProductManagement>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IProductSizeManagement, ProductSizeManagement>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewManagement, ReviewManagement>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentManagement, ShipmentManagement>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<IUserAddressManagement, UserAddressManagement>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
//builder.Services.AddScoped<ExceptionHandlerMiddleware>();

// Add authentication configuration
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "Unknown JWT Key"))
        };
    });

builder.Services.AddAuthorization(
    policy =>
    {
        policy.AddPolicy("EmailWhiteList", policy => policy.RequireClaim(ClaimTypes.Email, "admin@mail.com", "moderator1@mail.com"));
    }
);
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//         {
//             options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//             options.RoutePrefix = string.Empty;
//         });
// }
// else
// {
//     // Ensure HTTPS redirection in non-development environments
//     app.UseHttpsRedirection();
// }
app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowMultipleOrigins");

// app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
