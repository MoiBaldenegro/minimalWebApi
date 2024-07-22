using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using WebMinimalApi.Services;
using DPFP;

var builder = WebApplication.CreateBuilder(args);

// Configurar HttpClient para ignorar errores SSL
builder.Services.AddHttpClient<VerifyUser>(client =>
{
    // Configurar el cliente HTTP aquí si es necesario
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
});

// Registrar servicios personalizados
builder.Services.AddSingleton<VerifyUser>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/fingerprint", () =>
{
    Console.WriteLine("que onda pues");
    return "Finger print";
});

app.MapPost("/submit", async (Dictionary<string, object> request, VerifyUser verifyUser) =>
{
    // Convierte los datos recibidos en un Template de huella digital (simulado aquí)
    var template = new DPFP.Template();
    // Aquí se puede convertir el `request` al formato necesario para `template`
    // Esto es solo un ejemplo; ajusta según tus necesidades

    var result = await verifyUser.VerifyAsync(template);

    // Puedes acceder a los datos del cuerpo de la solicitud a través del objeto 'request'
    return Results.Ok(new { Message = "Datos recibidos correctamente", Data = result });
});

app.Run();
