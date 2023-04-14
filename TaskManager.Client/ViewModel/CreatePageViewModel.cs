
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using TaskManager.Client.Services;
using TaskManager.Shared;
using TaskManager.Shared.Models;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.ViewModel;

public class CreatePageViewModel : BaseViewModel
{
    public string Text { get; set; }
    public string Name { get; set; }
    public ItemPicker SelectedItem { get; set; }
    public int NumberTable { get; set; }
    private List<ItemPicker> _listPicker = new List<ItemPicker>();

    public List<ItemPicker> ListPicker
    {
        get => _listPicker;
        set
        {
            _listPicker = value;
            OnPropertyChanged();
        }
    }

    public ICommand Create { get; set; }
    public ICommand Change { get; set; }

    private readonly HttpService _service;
    
    public CreatePageViewModel(HttpService  service)
    {
        _service = service;
        GetUser();
       // ListPicker.Add(new ItemPicker(){Id = 1,Name = "1"});
        Create = new Command(AddTable);
       
    }

    async void AddTable()
    {
        if (SelectedItem != null)
        {
            var _client =await _service.GetHttp();
            var json = JsonConvert.SerializeObject( new UserTaskView() { Text = Text, TableNumber = SelectedItem.Id });
            var content = new StringContent(json,Encoding.UTF8,"application/json");
           var answer= await _client.PostAsync("api/Task/Create", content);
           if (answer.IsSuccessStatusCode)
           {
               Text = "";
               OnPropertyChanged(nameof(Text));
           }
        }


    }

    public async void GetUser()
    {
        try
        {
            var _client =await _service.GetHttp();
            var response = await _client.GetAsync("api/user/GetUsersTableNumber");

            if (response.IsSuccessStatusCode)
            {
                ListPicker = JsonConvert.DeserializeObject<List<ItemPicker>>(await response.Content.ReadAsStringAsync()) ;
                OnPropertyChanged(nameof(ListPicker));
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}