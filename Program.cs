using WebMinimalApi.Services;
using DPFP;

var builder = WebApplication.CreateBuilder(args);

// Configurar inyección de dependencias para VerifyUser
builder.Services.AddSingleton<VerifyUser>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/fingerprint", () =>
{
    Console.WriteLine("que onda pues");
    return "Finger print";
});

app.MapPost("/submit", async (MyRequestModel request, VerifyUser verifyUser) =>
{
    // Convierte los datos recibidos en un Template de huella digital (simulado aquí)
    var template = new DPFP.Template();
    // En un escenario real, deberías asignar el contenido de `request.FingerprintTemplate` al `template`

    var result = verifyUser.Verify(template);

    // Puedes acceder a los datos del cuerpo de la solicitud a través del objeto 'request'
    return Results.Ok(new { Message = "Datos recibidos correctamente", Data = result });
});

app.Run();

public class MyRequestModel
{
    public string Name { get; set; }
    public int Age { get; set; }
    public byte[] FingerprintTemplate { get; set; } // Asumiendo que los datos de la plantilla vienen aquí
}
