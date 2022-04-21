using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;

namespace Podcast.Transcript.Function
{
    public class TranscribeFunction
    {

        private static HttpClient client = new HttpClient();


        [FunctionName("TranscribeFunction")]
        public static async Task Run([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var baseURL = (Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost") + ":" + (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500");

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Invoking the /transcript microservice with HttpClient
            var response = await client.PostAsync($"{baseURL}/transcript", null);
            var data = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Transcription completed at:  {DateTime.Now} with data: {data}");

        }
    }
}
