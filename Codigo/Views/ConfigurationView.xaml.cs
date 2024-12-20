namespace BS360.Views;

public partial class ConfigurationView : Page
{
    public ConfigurationView()
    {
        InitializeComponent();
        DataContext = new ConfigurationViewModel();
    }  
}
