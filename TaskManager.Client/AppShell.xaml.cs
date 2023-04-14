using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Shared.Models.ModelForLogic;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client;

public partial class AppShell : Shell
{
    string path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "auth.json");
    private readonly AuthService _auth;
    public AppShell(AuthService auth)
    {
        _auth = auth;
        InitializeComponent();

        stackLogout.IsVisible = false;
       
        
        if (File.Exists(path))
        {
            Task.Run(async ()=> await AsyncMethod(path)).Wait();

            Entry.IsVisible = true;
            Entry.IsVisible = false;
            stackLogout.IsVisible = true;
        }
    }

    public async Task AsyncMethod(string path)
    {
        var text = await File.ReadAllTextAsync(path);
        var file = JsonConvert.DeserializeObject<JsonFile>(text );
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadToken(file.AccessToken);
        if (jwt.ValidTo < DateTime.Now)
        {
            await _auth.Enter(new EnterView(){Login = file.Login,Password = file.Password});
        }
    }

    async void Logout(object sender, EventArgs eventArgs)
    {
        if (File.Exists(path))
            File.Delete(path);
        
        Entry.IsVisible = true;
        await Current.GoToAsync("//Login");
        
    }
}