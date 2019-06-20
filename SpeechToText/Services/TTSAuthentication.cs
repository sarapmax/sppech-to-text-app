using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpeechToText.Services
{
    public class TTSAuthentication
    {
        //Token url
        public static readonly string fetchTokenUri =
            "https://japaneast.api.cognitive.microsoft.com/sts/v1.0/issuetoken";
        private string subscriptionKey;
        private string authToken;

        public TTSAuthentication(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
            this.authToken = FetchTokenAsync(fetchTokenUri, subscriptionKey).Result;
        }

        public string GetAccessToken()
        {
            return this.authToken;
        }

        /// <summary>
        /// Fetch auth token from the Azure
        /// </summary>
        /// <param name="fetchUri">Token url endpoint</param>
        /// <param name="subscriptionKey">Azure speech subscription key</param>
        /// <returns></returns>
        private async Task<string> FetchTokenAsync(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                var uriBuilder = new UriBuilder(fetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
