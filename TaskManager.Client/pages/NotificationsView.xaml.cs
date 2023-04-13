using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Client.pages;

public partial class NotificationsView : ContentPage
{
    public List<NotificationViewModel> ListNewTask;
    public NotificationsView()
    {
        InitializeComponent();
    }
}

