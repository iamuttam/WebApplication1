using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Runtime.Intrinsics.X86;
using System.Text;
using WebApplication1.Configuration;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Logging;

var builder = WebApplication.CreateBuilder(args);


//builder.Logging.ClearProviders();
//builder.Logging.AddDebug();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("lOG/log.txt",rollingInterval:RollingInterval.Minute).CreateLogger();
//Use Serilog
//builder.Host.UseSerilog();

// Use Serilog along with Built in loggers
builder.Logging.AddSerilog();
// Add services to the container. 

//Add Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//builder.Services.AddDbContext<EmployeeDBContax>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppDBConnectionString"))
//);


builder.Services.AddDbContext<CompanyDBContax>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppDBConnectionString")));


builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddScoped<ILogs, LogToServerMemory>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped(typeof(ICompanyRepository<>), typeof(CompanyRepository<>));

//Added JWT Token
builder.Services.AddEndpointsApiExplorer();   // Needed for minimal APIs

// Registers ISwaggerProvider
//builder.Services.AddSwaggerGen();             

// Registers JWT Authentication with Swagger
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

//    // Add JWT Bearer definition
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' followed by your JWT token."
//    });

//    // Add requirement
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});


// Add CORS policy
builder.Services.AddCors(option =>
{
    //option.AddDefaultPolicy(policy =>
    //{
    //    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //});
    option.AddPolicy("AllowAll", policy =>
    {
         //Allow All Origin 
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    option.AddPolicy("AllowOnlyLocalhost", policy =>
    {
        //Allow only local host 
        policy.WithOrigins("http://localhost:8080").WithHeaders("Accept","sdf","").WithMethods("GET","POST");
    });
    option.AddPolicy("AllowOnlygoogle", policy =>
    {
        //Allow All google origin 
        policy.WithOrigins("http://google.com","http://gmail.com","http://googledrive.com").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    option.AddPolicy("AllowOnlymicrosoft", policy =>
    {
        //Allow All Origin 
        policy.WithOrigins("http://microsoft.com","http://otlook.com","http://onedrive.com").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
} );

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretlocalhost"));
var JWTSecretgoogle = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretgoogle"));
var JWTSecretMicrosoft = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretMicrosoft"));
var GoogleIssuer = builder.Configuration.GetValue<string>("GoogleIssuer");
var MicrosoftIssuer = builder.Configuration.GetValue<string>("MicorsoftIssuer");
var LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");
var GoogleAudience = builder.Configuration.GetValue<string>("GoogleAudience");
var MicrosoftAudience = builder.Configuration.GetValue<string>("MicrosoftAudience");
var LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");



//JWT Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer("LoginForLocalUser",option =>
{
    option.SaveToken = true;
   // option.RequireHttpsMetadata = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = LocalIssuer,
        ValidateAudience = true,       
        ValidAudience = LocalAudience,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 } // match your token generation

    };
}).AddJwtBearer("LoginForGoogleUser", option =>
{
    option.SaveToken = true;
    // option.RequireHttpsMetadata = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(JWTSecretgoogle),
        ValidateIssuer = true,
        ValidIssuer =GoogleIssuer,
        ValidateAudience = true,
        ValidAudience= LocalAudience,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 } // match your token generation

    };
}).AddJwtBearer("LoginForMicrosoftlUser", option =>
{
    option.SaveToken = true;
    // option.RequireHttpsMetadata = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(JWTSecretMicrosoft),
        ValidateIssuer = true,
        ValidIssuer = MicrosoftIssuer,
        ValidateAudience = true,
        ValidAudience = MicrosoftAudience,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 } // match your token generation

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



//Use Corsfor google
//app.UseCors("AllowOnlygoogle");
//Use Cors for default
//app.UseCors();

//Use Cors for default

app.MapControllers();


app.UseEndpoints(endpoint =>
{
    endpoint.MapGet("api/testingendpoint",
        context => context.Response.WriteAsync("Test Response"))
        .RequireCors("AllowOnlyLocalhost");

    //endpoint.MapControllers().RequireCors("AllowAll");

    endpoint.MapGet("api/testendpoint2",
        context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));

});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
