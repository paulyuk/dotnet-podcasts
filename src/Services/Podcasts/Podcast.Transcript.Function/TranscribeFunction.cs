using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Podcast.Transcript.Function
{
    public class TranscribeFunction
    {

        var baseURL = (Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost") + ":" + (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500");
        var client = new HttpClient();

        [FunctionName("TranscribeFunction")]
        public void Run([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var content = new StringContent("{}", Encoding.UTF8, "application/json"); //to specify specific audio files

            // Invoking the /transcript microservice with HttpClient
            var response = await client.PostAsync($"{baseURL}/transcript", content);
            log.LogInformation("Transcription completed at:  {DateTime.Now}");
        }
    }
}
