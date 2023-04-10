using System.Windows.Input;
using Newtonsoft.Json;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Client;

public partial class MainPage : ContentPage
{
    public int count = 0;
    public List<UserView> Tasks { get; set; }
    public bool IsRefreshing { get; set; }
    
    public MainPage()
    {

     
        InitializeComponent();
        Tasks = new List<UserView>()
        {
            new UserView() { Text = "Тестовые значение1" ,IsComplete = true},
            new UserView() { Text = "Тестовые значение1" },
            new UserView() { Text = "Тестовые значение1", IsComplete = true},
            
        };
        
        // var httpClient = new HttpClient();
        // httpClient.BaseAddress = new Uri("http://localhost:5158/");
        // var httpResponse = (httpClient.GetAsync("api/Task/Read").Result);
        // var json = httpResponse.Content.ReadAsStringAsync().Result;
        // var list = JsonConvert.DeserializeObject<List<UserTask>>(json);
        //
        // Tasks = list;
        
        BindingContext = this;
    }

    public ICommand RefreshCommand => new Command(() => { });

    

  
  
    private void ChangeCheckBox(object sender, EventArgs e)
    {

        var d = sender!.GetType()!.GetProperty("AutomationId")!.GetValue(sender);
        var val=e!.GetType()!.GetProperty("Value")!.GetValue(e);
        var t = 5;
    }


    private async void RefreshView_OnRefreshing(object sender, EventArgs e)
    {
        try
        {
            var httpClientHandler = new HttpClientHandler();
            
            httpClientHandler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            
            httpClient.BaseAddress = new Uri("https://192.168.0.122:7134/");
            
            var httpResponse = (httpClient.GetAsync("api/Task/Read").Result);
            var json = httpResponse.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<UserTask>>(json);
            //Command="{Binding RefreshCommand
            var newList = new List<UserTask>(){new UserTask(){Text = ""}};
        
            Collect.ItemsSource = list;
        
            RefreshViewFirst.IsRefreshing = false;
        }
        catch (Exception ex)
        {
            var mes = ex.Message;
        }
        
    }
}