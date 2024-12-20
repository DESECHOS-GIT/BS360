namespace BS360.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private int connectionCounter = 0;
    private string logName = InterfaceConfig.logName;
    private TcpCommunication socketClient = new TcpCommunication();
    private TcpMessagingService messagingService = new TcpMessagingService();

    public MainViewModel(Frame navigationFrame)
    {
        this.navigationFrame = navigationFrame;
        ResultsViewCommand.ExecuteAsync(navigationFrame);

        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName();
        var version = assemblyName.Version?.ToString();

        InterfaceHeader = "INTERFAZ " + InterfaceConfig.interfaceName + " VERSIÓN " + version;
        VersionLabel = "Versión " + version;
    }

    [ObservableProperty]
    string interfaceHeader;

    [ObservableProperty]
    string versionLabel;

    [ObservableProperty]
    bool isConnected = false;

    [ObservableProperty]
    string captionHeader;

    private readonly Frame navigationFrame;

    [RelayCommand]
    async Task ConnectTcpComunication()
    {
        try
        {
            navigationFrame.Navigate(new TerminalResultsView());

            if (string.IsNullOrEmpty(InterfaceConfig.portInterface) || string.IsNullOrEmpty(InterfaceConfig.ipServer))
            {
                RegisterLog.Instance.RegisterInLog($"Debe indicar el puerto y la IP, para realizar la conexión.", logName, TerminalStates.ERROR);
                return;
            }
            else
            {
                socketClient.Port = Convert.ToInt32(InterfaceConfig.portInterface);
                socketClient.IP = InterfaceConfig.ipServer;

                if (IsConnected)
                {
                    IsConnected = false;
                    socketClient.DisconnectSocket();
                    RegisterLog.Instance.RegisterInLog($"Desconexión realizada al PUERTO [{InterfaceConfig.portInterface}]", logName, TerminalStates.WARNING);
                    return;
                }
                else
                {
                    IsConnected = true;
                    socketClient.ConnectSocket();
                    messagingService.StartReceptionFromServer(socketClient);
                    RegisterLog.Instance.RegisterInLog($"Conexión establecida al PUERTO [{InterfaceConfig.portInterface}]", logName, TerminalStates.OK);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"Error al conectar al servidor[{InterfaceConfig.portInterface}], mensaje[{ex.Message}]", logName, TerminalStates.ERROR);
            while (!RetryConnection())
            {
                if (connectionCounter == InterfaceConfig.socketReconnectionAttemps)
                {
                    RegisterLog.Instance.RegisterInLog($"No se puede conectar al servidor, Valide dirección IP y puerto", logName, TerminalStates.ERROR);
                    IsConnected = false;
                    return;
                }
                else connectionCounter++;
            }

            Thread.Sleep(100);
            IsConnected = true;
            return;
        }
    }

    private bool RetryConnection()
    {
        try
        {
            if (connectionCounter <= InterfaceConfig.socketReconnectionAttemps)
            {
                RegisterLog.Instance.RegisterInLog($"Intento de reconexion al socket numero[{connectionCounter}]", logName, TerminalStates.WARNING);
                socketClient.ConnectSocket();
                return true;
            }
            else
            {
                RegisterLog.Instance.RegisterInLog($"Los reintentos de conexion al socket server han finalizado, cerrando aplicación...", logName, TerminalStates.ERROR);
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [RelayCommand]
    async Task ResultsView()
    {
        CaptionHeader = "Carga de Resultados";
        navigationFrame.Navigate(new TerminalResultsView());

        RegisterLog.Instance.RegisterInLog($"Esperando conexión...", logName, TerminalStates.PROCESS);
    }

    [RelayCommand]
    async Task ConfigurationView()
    {
        CaptionHeader = "Configuración";
        navigationFrame.Navigate(new ConfigurationView());
    }


    [RelayCommand]
    async Task CloseApp()
    {
        MyMessageBox.ShowDialog("¿Desea cerrar la aplicación?", "¡Advertencia!", MyMessageBox.MessageBoxButton.OkCancel);
        if (MyMessageBox.buttonResultClicked == MyMessageBox.ButtonResult.OK)
        {
            Application.Current.Shutdown();
        }
    }
}
