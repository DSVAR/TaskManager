using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.ViewModel;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.pages;

public partial class CreatePages : ContentPage
{

    public CreatePages(CreatePageViewModel model)
    {
        
        InitializeComponent();
        Picker.SelectedIndex = 0;
        BindingContext = model;
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