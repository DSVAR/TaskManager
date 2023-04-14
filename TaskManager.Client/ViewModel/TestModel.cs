using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Client.ViewModel;

public class TestModel:INotifyPropertyChanged
{
    public string Login { get; set; }
    public string Password { get; set; }


    public TestModel()
    {
        
    }
    
    
    
    
    
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}