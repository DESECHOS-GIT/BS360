namespace BS360.CustomControls;

public partial class MyMessageBox : Window
{
    public MyMessageBox()
    {
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    public enum MessageBoxButton
    {
        YesNoCancel,
        OK,
        YesNo,
        OkCancel
    }
    public enum ButtonResult
    {
        NULL,
        YES,
        NO,
        CANCEL,
        OK
    }

    public static ButtonResult buttonResultClicked;
    public MyMessageBox(string message, string title, MessageBoxButton button)
    {
        InitializeComponent();
        TBLOCK_Title.Text = title;
        TBLOCK_Message.Text = message;
        buttonResultClicked = ButtonResult.NULL;
        if (button == MessageBoxButton.YesNoCancel)
        {
            SP_ContainsButton.Children.Remove(BTN_OK);

        }
        else if (button == MessageBoxButton.OK)
        {
            SP_ContainsButton.Children.Remove(BTN_CANCEL);
            SP_ContainsButton.Children.Remove(BTN_NO);
            SP_ContainsButton.Children.Remove(BTN_YES);

        }
        else if (button == MessageBoxButton.YesNo)
        {
            SP_ContainsButton.Children.Remove(BTN_CANCEL);
            SP_ContainsButton.Children.Remove(BTN_OK);

        }
        else if (button == MessageBoxButton.OkCancel)
        {
            SP_ContainsButton.Children.Remove(BTN_NO);
            SP_ContainsButton.Children.Remove(BTN_YES);

        }
    }

    public static void Show(string text, string title = "", MyMessageBox.MessageBoxButton button = MyMessageBox.MessageBoxButton.OK)
    {
        Application.Current.Dispatcher.Invoke((Action)delegate
        {
            new MyMessageBox(text, title, button).Show();
        });
    }
    public static void ShowDialog(string text, string title = "", MyMessageBox.MessageBoxButton button = MyMessageBox.MessageBoxButton.OK)
    {
        Application.Current.Dispatcher.Invoke((Action)delegate
        {
            new MyMessageBox(text, title, button).ShowDialog();
        });
    }

    private void pnlControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        WindowInteropHelper helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }

    private void pnlControl_MouseEnter(object sender, MouseEventArgs e)
    {
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }

    private void Button_Click_OK(object sender, RoutedEventArgs e)
    {
        buttonResultClicked = ButtonResult.OK;
        this.Close();
    }

    private void Button_Click_YES(object sender, RoutedEventArgs e)
    {
        buttonResultClicked = ButtonResult.YES;
        this.Close();
    }

    private void Button_Click_NO(object sender, RoutedEventArgs e)
    {
        buttonResultClicked = ButtonResult.NO;
        this.Close();
    }

    private void Button_Click_CANCEL(object sender, RoutedEventArgs e)
    {
        buttonResultClicked = ButtonResult.CANCEL;
        this.Close();
    }
}
