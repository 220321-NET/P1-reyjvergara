using DL;
using BL;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Swagger Generator builds Swagger Document objects from routes, controllers and models
// meaning this project will use our existing models with defined controllers and routes

// Registering our dependencies in Services container to be dependency injected
/// There are 3 different lifecycles/how often they get recreated
// Scoped, Transient, Singleton
// Singleton means the entire application's lifetime it shares the one instance
// Scoped is that for every https request. new instance is made
// Transient is for everytime it calls a class, it makes a new instance
// can't register plain old value type as dependency
builder.Services.AddScoped<DBRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("FADB")));
builder.Services.AddScoped<IFABL, FABL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Object model and middleware that shows Swagger JSON endpoints from APIs built on ASP.NET
    app.UseSwaggerUI();
    // SwaggerUI interprets swagger json to describe api functionality. Has tests that can be implemented
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
