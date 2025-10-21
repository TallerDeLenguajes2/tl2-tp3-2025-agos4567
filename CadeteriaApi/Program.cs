using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = WebApplication.CreateBuilder(args);

//  Agrega soporte para controladores (como CadeteriaController)
builder.Services.AddControllers();

//  Swagger: para probar tus endpoints fácilmente
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger visible solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//  Mapea tus controladores (por ejemplo, CadeteriaController)
app.MapControllers();

app.Run();
