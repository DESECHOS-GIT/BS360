namespace BS360.CustomControls;

public partial class TerminalStateBox : UserControl
{
    public static readonly DependencyProperty ColorBorderProperty =
                DependencyProperty.Register("BorderColor", typeof(Brush), typeof(TerminalStateBox), new PropertyMetadata(Brushes.Beige));

    public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register("Text", typeof(string), typeof(TerminalStateBox), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty SourceProperty =
                DependencyProperty.Register("SourceIcon", typeof(string), typeof(TerminalStateBox), new PropertyMetadata(string.Empty));

    public Brush BorderColor
    {
        get { return (Brush)GetValue(ColorBorderProperty); }
        set { SetValue(ColorBorderProperty, value); }
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public string SourceIcon
    {
        get { return (string)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    public TerminalStateBox()
    {
        InitializeComponent();
        DataContext = this;
        BorderColor = new SolidColorBrush(Colors.White);
        SourceIcon = "/Resources/OkM.png";
    }


}
