using System.Windows.Input;
using Newtonsoft.Json;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Client;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        BindingContext = this;
    }

}