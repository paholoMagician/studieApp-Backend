
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c => {
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

//Server = localhost\SQLEXPRESS; Database = master; Trusted_Connection = True;
var connectionString = "server=localhost\\SQLEXPRESS; database=STUDIEAPP-001; integrated security=true; Encrypt=False;";
builder.Services.AddDbContext<vinculacionUgContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
IServiceCollection serviceCollection = builder.Services.AddDbContext<vinculacionUgContext>(opt => opt.UseInMemoryDatabase(databaseName: "vinculacionUg"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder.WithOrigins("https://localhost:7106",
                                           "http://localhost:5106",
                                           "http://localhost:4200/", "*")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
