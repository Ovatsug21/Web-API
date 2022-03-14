using GestorTarefas.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adicionando os serviços ao container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestor de Tarefas", Version = "1.0" });

    var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
}
);

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer( 
    builder.Configuration.GetConnectionString("ServerConnection")
    ));


var app = builder.Build();

// Pipeline de Solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
