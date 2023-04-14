using System.IdentityModel.Tokens.Jwt;
using System.Windows.Input;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Shared.Models.ModelForLogic;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.ViewModel;

public class EnterViewModel :BaseViewModel
{
    
    public string Login { get; set; }
    public string Password { get; set; }
    readonly string _path = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "auth.json");
    private AuthService _auth;

    public  ICommand AuthCommand { get; set; }
    
    public EnterViewModel(AuthService auth)
    {
        _auth = auth;
        AuthCommand = new Command(Auth);
    }


    async void Auth()
    {
        try
        {
            await _auth.Enter(new EnterView(){Login = Login,Password = Password} );
            await Shell.Current.GoToAsync("//Main");
        }
        catch (Exception ex)
        {
            var mes = ex.Message;
        }
    }
    
   
}