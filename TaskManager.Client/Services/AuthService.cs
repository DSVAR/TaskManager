using System.Text;
using Newtonsoft.Json;
using TaskManager.Shared.Models.ModelForLogic;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.Services;

public class AuthService
{

    private readonly HttpClient _client;
    public AuthService(HttpClient client)
    {
        _client = client;
    }
    public async Task<bool> Enter(EnterView model)
    {
        try
        {
            var jsonEnter =JsonConvert.SerializeObject( new EnterView() { Login = model.Login, Password = model.Password });
            var content = new StringContent(jsonEnter,Encoding.UTF8,"application/json");
            var response = await _client.PostAsync("/api/Auth/Enter",content);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                var json = JsonConvert.SerializeObject( new JsonFile() 
                { AccessToken = token ,Login = model.Login,
                    Password = model.Password});

                
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "auth.json");

                using FileStream outputStream =File.OpenWrite(targetFile);
                using StreamWriter streamWriter = new StreamWriter(outputStream);

                await streamWriter.WriteAsync(json);
                return true;
            }

            return false;

        }
        catch (Exception ex)
        {
            var mess = ex.Message;
            return false;
        }
        
    }


}