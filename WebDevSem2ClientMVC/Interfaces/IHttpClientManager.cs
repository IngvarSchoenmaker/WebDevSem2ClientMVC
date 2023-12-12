namespace WebDevSem2ClientMVC.Interfaces
{
    public interface IHttpClientManager
    {
        HttpClient CreateClient();
        Task<HttpResponseMessage> GetAsync(string v);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);

    }
}
