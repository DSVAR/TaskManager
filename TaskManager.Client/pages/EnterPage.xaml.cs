using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Client.ViewModel;
using TaskManager.Shared.Models.ModelForLogic;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.pages;

public partial class EnterPage : ContentPage
{

    public EnterPage(EnterViewModel model)
    {
        InitializeComponent();
        this.BindingContext=model;
    }


    // public async void Auth(object sender, EventArgs eventArgs)
    // {
    //     try
    //     {
    //         await _auth.Enter(new EnterView() {Password = Password.Text,Login = Login.Text});
    //         await Shell.Current.GoToAsync("//Main");
    //     }
    //     catch (Exception ex)
    //     {
    //         var mes = ex.Message;
    //     }
    // }
    //
    // public async Task AsyncMethod(string path)
    // {
    //     var text = await File.ReadAllTextAsync(path);
    //     var file = JsonConvert.DeserializeObject<JsonFile>(text );
    //     var handler = new JwtSecurityTokenHandler();
    //     var jwt = handler.ReadToken(file.AccessToken);
    //     if (jwt.ValidTo < DateTime.Now)
    //     {
    //        var isEntered=await _auth.Enter(new EnterView(){Login = file.Login,Password = file.Password});
    //        if (isEntered)
    //        {
    //            await Shell.Current.GoToAsync("//Main");
    //        }
    //     }
    //     
    // }
}