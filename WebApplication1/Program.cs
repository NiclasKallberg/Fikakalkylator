using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;



//Connection strings finns i WebApplication1\appsettings.json

//Om denna s�tts p� 0: Connection string f�r lokal databas samt context.Database.EnsureCreated() anv�nds
//Om denna s�tts p� 1: Connection string f�r produktionsdatabas anv�nds
int isProduction = 1;









//I stort s� har dessa tutorials anv�nts

//Skapa API
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio

//Modell-validering
//https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0

//Entity Framework Core f�r databas
//https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio

//Hur man anropar API fr�n Blazor
//https://learn.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-8.0












var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();










//Tar bort API:ns automatiska filter s� man kan anv�nda ModelState.IsValid
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);



string connectionString = "WebApplication1ContextDevelopment";

if (isProduction == 1)
    connectionString = "WebApplication1ContextProduction";


builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString) ?? throw new InvalidOperationException("Connection string '" + connectionString + "' not found.")));












var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


















using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WebApplication1Context>();



    if (isProduction == 0)
        context.Database.EnsureCreated();
}














app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();