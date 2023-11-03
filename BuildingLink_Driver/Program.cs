using BuildingLink_Driver.Data;
using BuildingLink_Driver.Services;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IDriversRepository>(_ => new DriversRepository(new SQLiteConnection(connectionString)));
builder.Services.AddScoped<DriversService>();
builder.Services.AddTransient<HelperService>();

// If database and drivers tables not exist, create them and add a seed user
new DatabaseUtils(new SQLiteConnection(connectionString)).CreateTableAndDatabaseIfNotExist();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();