using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSyncService.Services
{
    public class NotifyService
    {
        public async Task Notify(string liveness, string message)
        {
            var url = "https://log-notify.azurewebsites.net/api/Notify?code=SXY/voVbLhQvzpfNgtA2PLJDl4wCoVG0zXClkFLSqKgkgHX/vU553Q==";
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var content = new
                {
                    Subject = "Failure Notification",
                    Type = "text/plain",
                    Value = $"The HealthCheck {liveness} is failing with message {message}"
                };

                await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            }
        }
    }
}
