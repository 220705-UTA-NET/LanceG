using DissonantSerenity.Data;
using DissonantSerenity.Model;

var builder = WebApplication.CreateBuilder(args);

// Connection Strings and environment variables
// Argument, Hardcoded, File-read, User Secrets, Environment Variabels are all
// options to retrieve a secret (like a connection string).

// the order of Environmental Arguments/ Values is Environment variable first (if there is one), then User Secret, then appsettings.json


string connectionString = await File.ReadAllTextAsync("C:/Revature/ConnectionStrings/DSconnectionString.txt");
//string connectionString = builder.Configuration.GetConnectionString("connectionString");

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository>(sp => new SQLRepository(connectionString, sp.GetRequiredService<ILogger<SQLRepository>>()));
builder.Services.AddSingleton<ITempData>(sp => new TempData(sp.GetRequiredService<ILogger<TempData>>()));
/*using ILoggerFactory loggerFactory =
            LoggerFactory.Create(builder =>
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;

                    //options.IncludeScopes = false;
                    options.SingleLine = true;
                    //options.TimestampFormat = "hh:mm:ss ";
                }));

ILogger<SQLRepository> customLogger = loggerFactory.CreateLogger<SQLRepository>();
builder.Services.AddSingleton<IRepository>(sp => new SQLRepository(connectionString, customLogger));*/

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

//temp
//World.Main();

app.Run();

