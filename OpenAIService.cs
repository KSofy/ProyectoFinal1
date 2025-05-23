using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Proyecto_De_investigacion.Servicios
{
    public class OpenAIService
    {
        private readonly string apiKey;

        public OpenAIService()
        {
            apiKey = ConfigurationManager.AppSettings["OpenAIKey"];
        }

        public async Task<string> ConsultarAsync(string prompt)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);
                return result.choices[0].message.content;
            }
            catch (Exception ex)
            {
                return $"Error consultando IA: {ex.Message}";
            }
        }
    }
}