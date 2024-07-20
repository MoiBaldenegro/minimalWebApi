using WebMinimalApi.Services;
using DPFP;

var builder = WebApplication.CreateBuilder(args);

// Configurar inyecci�n de dependencias para VerifyUser
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
    // Convierte los datos recibidos en un Template de huella digital (simulado aqu�)
    var template = new DPFP.Template();
    // En un escenario real, deber�as asignar el contenido de `request.FingerprintTemplate` al `template`

    var result = verifyUser.Verify(template);

    // Puedes acceder a los datos del cuerpo de la solicitud a trav�s del objeto 'request'
    return Results.Ok(new { Message = "Datos recibidos correctamente", Data = result });
});

app.Run();

public class MyRequestModel
{
    public string Name { get; set; }
    public int Age { get; set; }
    public byte[] FingerprintTemplate { get; set; } // Asumiendo que los datos de la plantilla vienen aqu�
}
