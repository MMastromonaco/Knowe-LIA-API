using Microsoft.EntityFrameworkCore;
using KnoweLia.Data;
using KnoweLia.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LiaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
//Skapa ett litet .NET REST API  med "CRUD" funktioner för dessa 3 objekt:
//USER
//ROLE
//GROUP

//API behöver 3 controllers, 1 per objekt.
//För att prata med datalager används EF CORE.
//Databas är SQL baserad, MariaDB/ SQL Express etc

//Man får ha valfria propertys på dessa objekt men relationerna mellan dem är viktiga.

//USER kan ha 1 ROLE och tillhöra flera GROUPS
//En ROLE eller GROUP kan ha flera USERS

//Funktioner i controllerna som skall finnas är CRUD:
//CREATE - Skapa objekt
//READ - Hämta upp ett objekt på ID/nyckel
//READ - Hämta upp flera/lista på objekt med sökbegrep(filter på namn grupp/roll),
//ex alla USER som ligger i GROUP "abc"
//UPDATE - Skall kunna updatera data på objektet
//DELETE - Ta bort objekt, tänk på effekter som kan bli om det ligger relation
//mot objektet som skall tas bort...."validering" kan behövas.