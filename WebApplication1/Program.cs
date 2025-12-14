using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Runtime.Intrinsics.X86;
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

builder.Services.AddDbContext<EmployeeDBContax>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppDBConnectionString"))
);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILogs, LogToServerMemory>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
