using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Shared.Models.ModelForLogic;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.pages;

public partial class EnterPage : ContentPage
{
    public EnterPage()
    {
        InitializeComponent();
    }


    public async void Auth(object sender, EventArgs eventArgs)
    {
        try
        {
            await new AuthService().Enter(new EnterView() {Password = Password.Text,Login = Login.Text});
            await Shell.Current.GoToAsync("//Main");
        }
        catch (Exception ex)
        {
            var mes = ex.Message;
        }
      

    }
}