using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DPFP;

namespace WebMinimalApi.Services
{
    public class VerifyUser
    {
        private readonly HttpClient _httpClient;
        private DPFP.Template _template;
        private DPFP.Verification.Verification _verificator;

        public VerifyUser(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _verificator = new DPFP.Verification.Verification(); // Inicializar el Verificator
        }

        public async Task<object> VerifyAsync(DPFP.Template template)
        {
            _template = template;

            try
            {
                // Hacer la solicitud GET para obtener los usuarios
                HttpResponseMessage response = await _httpClient.GetAsync("https://tomatesoft-server-paas.azurewebsites.net/users");
                response.EnsureSuccessStatusCode();

                // Leer la respuesta
                string responseBody = await response.Content.ReadAsStringAsync();

              

                // Parsear el JSON sin conocer la estructura exacta
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = doc.RootElement;
                    foreach (JsonElement user in root.EnumerateArray())
                    {
                        if (user.TryGetProperty("name", out JsonElement fingerprintElement))
                        {
                            Console.WriteLine(fingerprintElement.ToString());
                           

                        }

                    }

                }

                    // Aquí puedes procesar los datos recibidos
                    return responseBody;
            }
            catch (HttpRequestException e)
            {
                // Manejo de excepciones
                Console.WriteLine($"Excepción al hacer la solicitud: {e.Message}");
                return new { message = "Error en la solicitud" };
            }
        }
    }
}
