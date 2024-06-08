using Quartz;
using System.Text;
using System.Text.Json;

namespace WebApplication1
{
    public class CurrencyJob : IJob
    {
        private readonly ILogger<CurrencyJob> _logger;
        public CurrencyJob(ILogger<CurrencyJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Random rnd1 = new Random();
            var minUSD = 0.0133;
            var maxUSD = 0.0108;
            double rubUSD = Math.Round(rnd1.NextDouble() * (maxUSD-minUSD) + minUSD, 4);
            var minEUR = 0.0111;
            var maxEUR = 0.0095;
            double rubEUR = Math.Round(rnd1.NextDouble() * (maxEUR - minEUR) + minEUR, 4);
            var currencyPair = new Dictionary<string, double>()
            {
                { "RUB/USD" , rubUSD },
                { "RUB/EUR" , rubEUR }
            };
            string json = JsonSerializer.Serialize(currencyPair);

            await SendToQue(json);

            _logger.LogInformation($"{json}");
        }

        private async Task SendToQue(string json)
        {
            try
            {
                var message = new MessageBroker { Message = json };
                var messageJsn = JsonSerializer.Serialize(message);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7284/api/Broker");
                var content = new StringContent(messageJsn, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                _logger.LogError("Ошибка при отправке запроса в очередь", ex);
            }

        }
    }
}
