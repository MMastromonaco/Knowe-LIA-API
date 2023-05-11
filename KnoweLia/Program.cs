using Microsoft.EntityFrameworkCore;
using KnoweLia.Data;
using KnoweLia.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KnoweLiaDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();

//UPPGIFT
//Skapa ett litet .NET REST API med "CRUD" funktioner f�r dessa 3 objekt:
//USER
//ROLE
//GROUP

//API beh�ver 3 controllers, 1 per objekt.
//F�r att prata med datalager anv�nds EF CORE.
//Databas �r SQL baserad, MariaDB/ SQL Express etc

//Man f�r ha valfria propertys p� dessa objekt men relationerna mellan dem �r viktiga.

//USER kan ha 1 ROLE och tillh�ra flera GROUPS
//En ROLE eller GROUP kan ha flera USERS

//Funktioner i controllerna som skall finnas �r CRUD:

//CREATE (POST) - Skapa objekt

//READ (GET{Id}) - H�mta upp ett objekt p� ID/nyckel

//READ (GET) - H�mta upp flera/lista p� objekt med s�kbegrep(filter p� namn grupp/roll),
//ex alla USER som ligger i GROUP "abc"

//UPDATE (PUT{Id}) - Skall kunna updatera data p� objektet

//DELETE (DELETE{Id})- Ta bort objekt, t�nk p� effekter som kan bli om det ligger relation
//mot objektet som skall tas bort...."validering" kan beh�vas.