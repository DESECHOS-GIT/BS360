namespace BS360;

public partial class App : Application
{
    protected void ApplicationStart(object sender, StartupEventArgs e)
    {
        var mainView = new MainView();
        mainView.Show();
    }
}
