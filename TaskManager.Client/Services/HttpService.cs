using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TaskManager.Shared.Models.ModelForLogic;

namespace TaskManager.Client.Services;

public class HttpService
{
    private HttpClient _client;
    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<HttpClient> GetHttp()
    {
        string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "auth.json");
        var text = await File.ReadAllTextAsync(path);
        var file = JsonConvert.DeserializeObject<JsonFile>(text );
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", file.AccessToken);
        return _client;
    }
}