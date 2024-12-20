namespace BS360.ViewModels;

public partial class ConfigurationViewModel : ObservableObject
{
    private enum PosibleTab : ushort
    {
        Connection = 0,
        Parametrization = 1,
        ParametrizationQC = 2
    } 

    [ObservableProperty]
    int activeTab; 

    #region conexion
    [ObservableProperty]
    string codeDmJsonInput = InterfaceConfig.codeDMJson;
    [ObservableProperty]
    string userJsonInput = InterfaceConfig.userJson;
    [ObservableProperty]
    string clientInput = InterfaceConfig.client;
    [ObservableProperty]
    string userNameInput = InterfaceConfig.userName;
    [ObservableProperty]
    string passwordInput = InterfaceConfig.password;
    [ObservableProperty]
    string urlBaseInput = InterfaceConfig.endPointBase;
    [ObservableProperty]
    string urlHostQueryInput = InterfaceConfig.endPointHostQuery;
    [ObservableProperty]
    string urlResultInput = InterfaceConfig.endPointResultados;
    [ObservableProperty]
    string urlTokenInput = InterfaceConfig.endPointToken;
    #endregion

    #region Parametrizacion
    [ObservableProperty]
    string interfaceNameInput = InterfaceConfig.interfaceName;
    [ObservableProperty]
    string equipmentNameInput = InterfaceConfig.equipmentName;
    [ObservableProperty]
    string logPathInput = InterfaceConfig.logPath;
    [ObservableProperty]
    string logNameInput = InterfaceConfig.logName;
    [ObservableProperty]
    int reconnAttemptsInput = InterfaceConfig.socketReconnectionAttemps ?? 1;
    [ObservableProperty]
    int retransmitionTimeInput = InterfaceConfig.transmitionTime ?? 1;
    [ObservableProperty]
    string portInterfaceInput = InterfaceConfig.portInterface;
    [ObservableProperty]
    string okFilesPathInput = InterfaceConfig.okFilesPath;
    [ObservableProperty]
    string errorFilesPathInput = InterfaceConfig.errorFilesPath;
    [ObservableProperty]
    string retransmitionPathInput = InterfaceConfig.retransmitionPath;
    [ObservableProperty]
    string cantidadMensajes = InterfaceConfig.cantidadMensajesVista.ToString();
    #endregion



    [RelayCommand]
    async Task SaveConfigurationConnection()
    {
        try
        {
            if (!GlobalVariables.VerificacionInput)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.AppSettings.Settings;

                if (ActiveTab == (int)PosibleTab.Connection)
                {
                    settings["codeDM"].Value = CodeDmJsonInput;
                    settings["user"].Value = UserJsonInput;
                    settings["client"].Value = ClientInput;
                    settings["userName"].Value = UserNameInput;
                    settings["password"].Value = PasswordInput;
                    settings["endPointBase"].Value = UrlBaseInput;
                    settings["endPointHostQuery"].Value = UrlHostQueryInput;
                    settings["endPointResultados"].Value = UrlResultInput;
                    settings["endPointToken"].Value = UrlTokenInput;
                }
                else if (ActiveTab == (int)PosibleTab.Parametrization)
                {      
                    settings["nombreInterfaz"].Value = InterfaceNameInput;
                    settings["nombreEquipo"].Value = EquipmentNameInput;
                    settings["nombreLog"].Value = LogPathInput;
                    settings["rutaLog"].Value = LogNameInput;
                    settings["intentosReconexionSocket"].Value = ReconnAttemptsInput.ToString();
                    settings["tiempoRetransmision"].Value = RetransmitionTimeInput.ToString();
                    settings["puerto"].Value = PortInterfaceInput;
                    settings["rutaArchivosOK"].Value = OkFilesPathInput;
                    settings["rutaArchivosError"].Value = ErrorFilesPathInput;
                    settings["rutaRetransmision"].Value = RetransmitionPathInput;
                    settings["cantidadMensajesVista"].Value = CantidadMensajes.ToString();
                }       
                else if (ActiveTab == (int)PosibleTab.ParametrizationQC)
                {

                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                InterfaceConfig.InitializeConfig();
                MyMessageBox.ShowDialog("Configuración guardada exitosamente.", "¡Exito!", MyMessageBox.MessageBoxButton.OK);
            }
            else MyMessageBox.ShowDialog("Verifique datos erroneos.", "¡Alerta!", MyMessageBox.MessageBoxButton.OK);
            

        }
        catch (Exception ex)
        {
            MyMessageBox.ShowDialog($"Error al tratar de guardar la configuración. {ex.Message}", "¡Alerta!", MyMessageBox.MessageBoxButton.OK);
        }
    }  

}
