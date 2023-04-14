using TaskManager.Client.Services;

namespace TaskManager.Client;

public partial class App : Application
{
    
    public App(AppShell shell)
    {
        InitializeComponent();

         MainPage = shell;
    }
}