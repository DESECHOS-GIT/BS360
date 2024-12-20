namespace BS360.Views;

public partial class MainView : Window
{  
    public MainView()
    {
        InitializeComponent();
        InterfaceConfig.InitializeConfig();

        DataContext = new MainViewModel(DashboardPanelTerminal);
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    private void pnlControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        WindowInteropHelper helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }

    private void pnlControl_MouseEnter(object sender, MouseEventArgs e)
    {
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (this.Width <= 800)
        {
            TextBtnCaption.Visibility = Visibility.Hidden;
            if (GridBtnCaption.ColumnDefinitions.Count == 2)
                GridBtnCaption.ColumnDefinitions.RemoveAt(1);
        }
        else
        {
            TextBtnCaption.Visibility = Visibility.Visible;
            if (GridBtnCaption.ColumnDefinitions.Count == 1)
                GridBtnCaption.ColumnDefinitions.Add(new ColumnDefinition());

        }
    }

    private void btnMaximize_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Normal)
            this.WindowState = WindowState.Maximized;
        else this.WindowState = WindowState.Normal;
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }   
}
