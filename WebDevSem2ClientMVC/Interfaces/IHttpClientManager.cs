namespace WebDevSem2ClientMVC.Interfaces
{
    public interface IHttpClientManager
    {
        HttpClient CreateClient();
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string uri);

    }
}
