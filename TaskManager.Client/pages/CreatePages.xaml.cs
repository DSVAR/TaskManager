using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.pages;

public partial class CreatePages : ContentPage
{
    public List<ItemPicker> list = new List<ItemPicker>()
    {
        new ItemPicker() { Id = "1", Name = "1" },
        new ItemPicker() { Id = "2", Name = "2" },
        new ItemPicker() { Id = "3", Name = "3" },
    };

    public CreatePages()
    {
        InitializeComponent();
        Picker.ItemsSource = list;
        Picker.SelectedIndex = 0;
        ///thread
    }



    void AddItem(object sender, EventArgs e)
    {
        var item = (ItemPicker)Picker.SelectedItem;
        var text = Text.Text;
        var sw = 5;
        // thread 
    }

}