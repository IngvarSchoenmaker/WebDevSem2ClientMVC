using SendGrid.Helpers.Mail;
using System;
using System.Net.Http.Headers;
using System.Security.Policy;
using WebDevSem2ClientMVC.Interfaces;

namespace WebDevSem2ClientMVC.Services
{
    public class HttpClientManager : IHttpClientManager
    {
        public HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            InitializeHttpClient(httpClient);
            return httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            using (var httpClient = CreateClient())
            {
                return await httpClient.GetAsync(uri);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            using (var httpClient = CreateClient())
            {
                return await httpClient.PostAsync(uri, content);
            }
        }
        private void InitializeHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44384/api/game/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
