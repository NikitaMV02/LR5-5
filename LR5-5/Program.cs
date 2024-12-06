using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;

namespace ApiClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "jvb7mxg557mzrn2a181vgxpg7ueteuhkqc292oox4ad1eqdib";

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient<ApiClient>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://api.wordnik.com/v4/");
            })
            .AddTransientHttpErrorPolicy(policy => policy.RetryAsync(3));

            serviceCollection.AddTransient<ApiClient>(provider =>
            {
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new ApiClient(httpClient, apiKey);
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var apiClient = serviceProvider.GetService<ApiClient>();

            if (apiClient == null)
            {
                Console.WriteLine("ApiClient is not registered correctly.");
                return;
            }

            // Використання GET
            var getResponse = await apiClient.GetAsync("words.json/randomWords");
            Console.WriteLine($"GET Response: {getResponse.Message}, HTTP Code: {getResponse.HttpStatusCode}");
            if (getResponse.Data != null)
            {
                foreach (var word in getResponse.Data)
                {
                    Console.WriteLine($"Word: {word.Word}, Canonical: {word.CanonicalForm}");
                }
            }

            // Використання POST
            var postPayload = new { hasDictionaryDef = "true", minLength = 5, maxLength = 10 };
            var postResponse = await apiClient.PostAsync("words.json/search", postPayload);
            Console.WriteLine($"POST Response: {postResponse.Message}, HTTP Code: {postResponse.HttpStatusCode}");
            if (postResponse.Data != null)
            {
                foreach (var word in postResponse.Data)
                {
                    Console.WriteLine($"Word: {word.Word}, Canonical: {word.CanonicalForm}");
                }
            }
        }
    }
}
