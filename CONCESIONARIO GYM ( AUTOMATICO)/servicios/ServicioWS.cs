using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class ServicioWhatsAppMeta
    {
        // Reemplaza estos dos valores con los de tu panel de Meta
        private readonly string _token = "TU_TOKEN_TEMPORAL_AQUI";
        private readonly string _phoneNumberId = "TU_IDENTIFICADOR_DE_NUMERO_DE_TELEFONO_AQUI";

        public async Task<bool> EnviarMensajeBienvenidaAsync(string numeroDestinatario, string nombreCliente)
        {
            // Formatear número (debe incluir código de país sin el signo +, ej: 5939XXXXXXXX)
            string numeroLimpio = LimpiarNumero(numeroDestinatario);
            string url = $"https://graph.facebook.com/v19.0/{_phoneNumberId}/messages";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            // Cuerpo del mensaje usando la plantilla genérica 'hello_world' de prueba de Meta
            var payload = new
            {
                messaging_product = "whatsapp",
                to = numeroLimpio,
                type = "template",
                template = new
                {
                    name = "hello_world",
                    language = new
                    {
                        code = "en_US"
                    }
                }
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("¡Notificación de WhatsApp enviada con éxito!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error al enviar WhatsApp: {response.StatusCode}");
                    Console.WriteLine($"Detalle: {responseBody}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al conectar con Meta API: {ex.Message}");
                return false;
            }
        }

        private string LimpiarNumero(string numero)
        {
            // Remueve espacios, guiones y el signo +
            return numero.Replace("+", "").Replace(" ", "").Replace("-", "").Trim();
        }
    }
}
