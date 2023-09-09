using System.Text;

namespace Sokutatsu;

public class Client
{
    string Uri { get; }
    public string Body { get; }
    string ContentType { get; }

    public Client(string uri, string body, string contentType)
    {
        Uri = uri;
        Body = body;
        ContentType = contentType;
    }

    public async Task<string> Post()
    {
        string responseText = "";
        try
        {
            File.AppendAllText("log.txt", $"POST, {Body}, {ContentType}\n");
            using HttpClient client = new();
            StringContent content = new(Body, Encoding.UTF8, ContentType);
            HttpResponseMessage response = await client.PostAsync(Uri, content);
            responseText = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            File.AppendAllText("log.txt", $"POST {ex.Message}, {Body}, {ContentType}\n");
        }

        return responseText;
    }

    public async Task<string> Get()
    {
        string responseText = "";
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Uri);
        responseText = await response.Content.ReadAsStringAsync();

        return responseText;
    }
}
