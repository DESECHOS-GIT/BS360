namespace BS360.ViewModels;

public partial class TerminalResultsViewModel : ObservableObject
{

    private StackPanel stackPanel;
    private ScrollViewer scrollViewer;

    public TerminalResultsViewModel(StackPanel stackPanel, ScrollViewer scrollViewer)
    {
        this.stackPanel = stackPanel;
        this.scrollViewer = scrollViewer;

        MessageService.Instance.Messages.CollectionChanged += Messages_CollectionChanged;

        foreach (var message in MessageService.Instance.Messages)
        {
            AddMessageTerminal(message);
        }
    }

    private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Message newMessage in e.NewItems)
            {
                AddMessageTerminal(newMessage);
            }
        }
    }
    
    private void AddMessageTerminal(Message terminalState)
    {
		try
		{
            TerminalStateBox terminalStateBox = new TerminalStateBox();
            terminalStateBox.Text = terminalState.Text;
            terminalStateBox.Margin = new Thickness(0, 0, 0, 10);

            switch (terminalState.State)
            {
                case TerminalStates.ERROR:
                    terminalStateBox.BorderColor = Application.Current.Resources["ColorError"] as SolidColorBrush;
                    terminalStateBox.SourceIcon = Application.Current.Resources["IconError"].ToString();
                    break;
                case TerminalStates.PROCESS:
                    terminalStateBox.BorderColor = Application.Current.Resources["ColorInfo"] as SolidColorBrush;
                    terminalStateBox.SourceIcon = Application.Current.Resources["IconProcess"].ToString();
                    break;
                case TerminalStates.WARNING:
                    terminalStateBox.BorderColor = Application.Current.Resources["ColorWarning"] as SolidColorBrush;
                    terminalStateBox.SourceIcon = Application.Current.Resources["IconWarning"].ToString();
                    break;
                case TerminalStates.OK:
                    terminalStateBox.BorderColor = Application.Current.Resources["ColorSucess"] as SolidColorBrush;
                    terminalStateBox.SourceIcon = Application.Current.Resources["IconSucess"].ToString();
                    break;
                default:
                    terminalStateBox.BorderColor = Application.Current.Resources["ColorInfo"] as SolidColorBrush;
                    terminalStateBox.SourceIcon = Application.Current.Resources["IconProcess"].ToString();
                    break;
            }

            stackPanel.Children.Add(terminalStateBox);
            scrollViewer.ScrollToEnd();
        }
		catch (Exception ex)
		{
			throw;
		}
    }
}
