using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Client.ViewModel;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Client.pages;

public partial class Tasks : ContentPage
{
   
    
    public List<UserView> TasksView { get; set; }
    public bool IsRefreshing { get; set; }
    
    private readonly HttpService _httpService;
    public Tasks(TaskViewModel model,HttpService httpService)
    {
        try
        {
            _httpService = httpService;
            InitializeComponent();
         
            
            BindingContext = model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

  

  
  
    private async void ChangeCheckBox(object sender, EventArgs e)
    {

        var id = sender!.GetType()!.GetProperty("AutomationId")!.GetValue(sender)?.ToString();
        var val=(bool)e!.GetType()!.GetProperty("Value")!.GetValue(e)!;

        if (id != null)
        {
            var http =await _httpService.GetHttp();
            var content = new StringContent(JsonConvert.SerializeObject(new UpdateTask() { Id = id, IsComplete = val }),
                Encoding.UTF8, "application/json");
            var response =await http.PostAsync("api/Task/Update", content);
            if (!response.IsSuccessStatusCode)
            {
            
            }
        }
       
    }


    // private async void RefreshView_OnRefreshing(object sender, EventArgs e)
    // {
    //     try
    //     {
    //         var _client = await _httpService.GetHttp();
    //         
    //         var httpResponse = (_client.GetAsync("api/Task/Read").Result);
    //         var json = httpResponse.Content.ReadAsStringAsync().Result;
    //         var list = JsonConvert.DeserializeObject<List<UserTask>>(json);
    //     
    //         Collect.ItemsSource = list;
    //     
    //         RefreshViewFirst.IsRefreshing = false;
    //     }
    //     catch (Exception ex)
    //     {
    //         var mes = ex.Message;
    //     }
    //     
    // }
}