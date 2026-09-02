using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace CONCESIONARIO_GYM___AUTOMATICO_.IA
{
    public class ServicioIA
    {
        private const string ApiKey = "calve secreta";
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

        public static async Task<RespuestaIA> PreguntarAsync(string preguntaUsuario)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

                    var requestBody = new
                    {
                        model = "gpt-4o-mini", // Especificamos exactamente el modelo gpt-4o-mini
                        messages = new[]
                        {
                        new { role = "system", content = "Eres el asistente virtual experto y motivador del gimnasio Smart Fit. Responde de forma muy concisa, profesional y directa." },
                        new { role = "user", content = preguntaUsuario }
                         },
                        max_tokens = 150
                    };

                    string jsonPayload = JsonSerializer.Serialize(requestBody);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(responseString))
                        {
                            string textoIA = doc.RootElement
                                .GetProperty("choices")[0]
                                .GetProperty("message")
                                .GetProperty("content")
                                .GetString();

                            return new RespuestaIA(textoIA.Trim());
                        }
                    }
                    return new RespuestaIA("[IA] Error de conexión con OpenAI.");
                }
            }
            catch (Exception ex)
            {
                return new RespuestaIA($"[ERROR] {ex.Message}");
            }
        }
    }
}
