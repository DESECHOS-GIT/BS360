namespace BS360.Views;

public partial class TerminalResultsView : Page
{
    public TerminalResultsView()
    {
        InitializeComponent();
        DataContext = new TerminalResultsViewModel(MainTerminal, TerminalScrollViewer);       
    }  

}
