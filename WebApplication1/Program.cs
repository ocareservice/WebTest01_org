using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var solutionDirectory = TryGetSolutionDirectory();

//var sharedFolder = Path.Combine(solutionDirectory.FullName, "..", "Shared"); 

builder.Configuration.AddJsonFile(Path.Combine(solutionDirectory.FullName, "appsettingsTest.json")); 

//test 021
//change 02
//change 032 add some change for reviews
var app = builder.Build();

string connectionString = GetConnectionStringForTenant(app.Configuration);

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

string GetConnectionStringForTenant(IConfiguration configuration)
{
    
    Console.WriteLine(configuration.GetValue<string>("GlobalSetting"));
    Console.WriteLine(configuration.GetValue<string>("PlatformSetting"));
    Console.WriteLine(configuration.GetValue<string>("UserSetting"));

    return "";
}

DirectoryInfo TryGetSolutionDirectory()
{
    var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

    while (directory != null && !directory.GetFiles("*.sln").Any())
    {
        directory = directory.Parent;
    }

    return directory;
}