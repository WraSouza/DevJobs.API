using DevJobs.API.Persistance;
using DevJobs.API.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");
builder.Services.AddDbContext<DevJobContext>(options =>
options.UseInMemoryDatabase("DevJobs"));

builder.Services.AddScoped<IJobVacancyRepositories,JobVacancyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1",new OpenApiInfo{
        Title = "DevJobs.API",
        Version = "v1",
        Contact = new OpenApiContact{
            Name = "Wladimir",
            Email = "wladimirsouza@outlook.com.br",
            Url = new Uri("https://www.linkedin.com/in/wladimir-ribeiro-5b70531a/")
        }
    });
    var xmlFile = "DevJobs.API.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);

    c.IncludeXmlComments(xmlPath);
});
/*
builder.Host.ConfigureAppConfiguration((hostingContext,config) =>{
    Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(connectionString,
    sinkOptions: new MSSqlServerSinkOptions(){
        AutoCreateSqlTable = true,
        TableName = "Logs"
    })
    .WriteTo.Console()
    .CreateLogger();
}).UseSerilog();
*/

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
