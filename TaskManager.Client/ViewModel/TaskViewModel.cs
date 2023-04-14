using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Client.ViewModel;

public class TaskViewModel:BaseViewModel
{
    public List<UserView> TasksView { get; set; }
    
    public bool IsComplete { get; set; }
    public string Id{ get; set; }
    public string Text{ get; set; }
    private bool _isRefreshing { get; set; }
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set { _isRefreshing = value; OnPropertyChanged("IsRefreshing"); }
    }
    
    private readonly HttpService _httpService;
    public ICommand Command { get; set; }
    
    public TaskViewModel(HttpService client)
    {
        _httpService = client;
        Task.Run(async () =>
        {
            await RefreshView_OnRefreshing();
        });
        
        Command = new Command(async () => await RefreshView_OnRefreshing());

    }

   


    async Task RefreshView_OnRefreshing()
    {
        try
        {
            var http = await _httpService.GetHttp();
            
            var httpResponse = await http.GetAsync("api/Task/Read");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json =await httpResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<UserView>>(json);

                TasksView = list;
                
                OnPropertyChanged(nameof(TasksView));
                IsRefreshing = false;
            }
            
         
            
        }
        catch (Exception ex)
        {
            var mes = ex.Message;
            IsRefreshing = false;
        }
    }
    
}