namespace BS360.CustomControls;


public partial class ConfigurationInput : UserControl
{
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register("LabelText", typeof(string), typeof(ConfigurationInput));

    public static readonly DependencyProperty ErrorTextProperty =
        DependencyProperty.Register("ErrorText", typeof(string), typeof(ConfigurationInput));

    public static readonly DependencyProperty LabelForegroundProperty =
        DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(ConfigurationInput), new PropertyMetadata(Brushes.Black));


    public static readonly DependencyProperty InputTextProperty =
    DependencyProperty.Register("InputText", typeof(string), typeof(ConfigurationInput), new PropertyMetadata(string.Empty, OnInputTextChanged));

    public string LabelText
    {
        get { return (string)GetValue(LabelTextProperty); }
        set { SetValue(LabelTextProperty, value); }
    }

    public string ErrorText
    {
        get { return (string)GetValue(ErrorTextProperty); }
        set { SetValue(ErrorTextProperty, value); }
    }

    public Brush LabelForeground
    {
        get { return (Brush)GetValue(LabelForegroundProperty); }
        set { SetValue(LabelForegroundProperty, value); }
    }

    public string InputText
    {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }

    private static void OnInputTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as ConfigurationInput;
        if (control != null && control.InputTextBox != null)
        {
            control.InputTextBox.Text = e.NewValue as string;
        }
    }

    private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        this.InputText = InputTextBox.Text;
    }

    public ConfigurationInput()
    {
        InitializeComponent();

        var primaryColorBrush = (SolidColorBrush)Application.Current.Resources["Color2"];
        var secondaryColorBrush = (SolidColorBrush)Application.Current.Resources["Color3"];

        LabelForeground = primaryColorBrush;
        BorderInputTextBox.BorderBrush = primaryColorBrush;
        BorderInputTextBox.BorderThickness = new Thickness(2);

        InputTextBox.LostFocus += CityTextBox_LostFocus;
        InputTextBox.GotFocus += InputTextBox_GotFocus;
        InputTextBox.TextChanged += CityTextBox_TextChanged;
    }

    private void CityTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputTextBox.Text))
        {
            LabelForeground = Brushes.Red;
            BorderInputTextBox.BorderBrush = Brushes.Red;
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
        else
        {
            LabelForeground = (SolidColorBrush)Application.Current.Resources["Color2"];
            BorderInputTextBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["Color2"];
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }
    }

    private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        bool entra = false;
        var control = sender as TextBox;

        if (control != null)
        {
            var parent = VisualTreeHelper.GetParent(control);
            while (parent != null && !(parent is UserControl))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is ConfigurationInput userControl)
            {
                var controlName = userControl.Name;

                if (controlName.Equals("cantidadMensajes") && string.IsNullOrWhiteSpace(userControl.InputTextBox.Text))
                {
                    entra = true;
                    LabelForeground = Brushes.Red;
                    BorderInputTextBox.BorderBrush = Brushes.Red;
                    BorderInputTextBox.BorderThickness = new Thickness(2);
                    userControl.ErrorTextBlock.Text = "El campo no puede estar vacio";
                    ErrorTextBlock.Visibility = Visibility.Visible;

                }
                else if (controlName.Equals("cantidadMensajes") && !string.IsNullOrWhiteSpace(userControl.InputTextBox.Text))
                {
                    int valor = 500;

                    if (!ContainsLetters(userControl.InputTextBox.Text.ToString()))
                    {
                        entra = true;
                        LabelForeground = Brushes.Red;
                        BorderInputTextBox.BorderBrush = Brushes.Red;
                        BorderInputTextBox.BorderThickness = new Thickness(2);
                        userControl.ErrorTextBlock.Text = "El valor debe ser numerico";
                        ErrorTextBlock.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (Convert.ToInt32(userControl.InputTextBox.Text) > valor)
                        {
                            entra = true;
                            LabelForeground = Brushes.Red;
                            BorderInputTextBox.BorderBrush = Brushes.Red;
                            BorderInputTextBox.BorderThickness = new Thickness(2);
                            userControl.ErrorTextBlock.Text = "Supera la cantidad maxima(500)";
                            ErrorTextBlock.Visibility = Visibility.Visible;
                        }
                    }

                }
                else if (string.IsNullOrWhiteSpace(InputTextBox.Text))
                {

                    entra = true;
                    LabelForeground = Brushes.Red;
                    BorderInputTextBox.BorderBrush = Brushes.Red;
                    BorderInputTextBox.BorderThickness = new Thickness(2);
                    ErrorTextBlock.Visibility = Visibility.Visible;
                }

                if (!entra)
                {
                    GlobalVariables.VerificacionInput = false;
                    LabelForeground = (SolidColorBrush)Application.Current.Resources["Pressed"];

                    var primaryColorBrush = (SolidColorBrush)Application.Current.Resources["Color2"];
                    var secondaryColorBrush = (SolidColorBrush)Application.Current.Resources["Color3"];

                    LabelForeground = primaryColorBrush;
                    BorderInputTextBox.BorderBrush = primaryColorBrush;
                    BorderInputTextBox.BorderThickness = new Thickness(2);
                    ErrorTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    GlobalVariables.VerificacionInput = true;
                }
            }
        }

    }

    private bool ContainsLetters(string input)
    {
        string pattern = @"^[0-9]*$";
        return Regex.IsMatch(input, pattern);
    }

    private void CityTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputTextBox.Text))
        {
            LabelForeground = Brushes.Red;
            BorderInputTextBox.BorderBrush = Brushes.Red;
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
        else
        {
            LabelForeground = (SolidColorBrush)Application.Current.Resources["Pressed"];
            BorderInputTextBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["Pressed"];
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }
    }

    private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        BorderInputTextBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["Color8"];
        LabelForeground = (SolidColorBrush)Application.Current.Resources["Color8"];
        BorderInputTextBox.BorderThickness = new Thickness(2);
        if (string.IsNullOrWhiteSpace(InputTextBox.Text))
        {
            LabelForeground = Brushes.Red;
            BorderInputTextBox.BorderBrush = Brushes.Red;
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }

    private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputTextBox.Text))
        {
            LabelForeground = Brushes.Red;
            BorderInputTextBox.BorderBrush = Brushes.Red;
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
        else
        {
            LabelForeground = (SolidColorBrush)Application.Current.Resources["Color2"];
            BorderInputTextBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["Color2"];
            BorderInputTextBox.BorderThickness = new Thickness(2);
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }
    }

    private void InputTextBox_Initialized(object sender, EventArgs e)
    {
        LabelForeground = (SolidColorBrush)Application.Current.Resources["Color2"];
        BorderInputTextBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["Color2"];
        BorderInputTextBox.BorderThickness = new Thickness(2);

    }
}
