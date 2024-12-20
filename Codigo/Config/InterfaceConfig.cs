using System.Configuration;

namespace BS360.Config
{
    internal class InterfaceConfig
    {
        //Configuración Interfaz
        static internal string? interfaceName;
        static internal string? equipmentName;
        static internal int? transmitionTime;
        static internal int? cantidadMensajesVista;
        //Configuración Log
        static internal bool activateLog;
        static internal string? logPath;
        static internal string? logName;
        static internal string? printQueriesLog;
        //Configuración Servicios
        static internal string? client;
        static internal string? codeDMJson;
        static internal string? userJson;
        static internal string? userName;
        static internal string? password;
        static internal string? endPointBase;
        static internal string? endPointHostQuery;
        static internal string? endPointResultados;
        static internal string? endPointToken;
        static internal string? portInterface;
        static internal string? ipServer;
        static internal int? socketReconnectionAttemps;
        static internal string? retransmitionPath;
        static internal string? okFilesPath;
        static internal string? errorFilesPath;



        static internal void InitializeConfig()
        {
            //Configuración Interfaz
            interfaceName = ConfigurationManager.AppSettings["nombreInterfaz"];
            equipmentName = ConfigurationManager.AppSettings["nombreEquipo"];
            transmitionTime = Convert.ToInt32(ConfigurationManager.AppSettings["tiempoRetransmision"]?.ToString());
            //Configuración Log
            activateLog = ConfigurationManager.AppSettings["logActivo"].ToString().Equals("S");
            logPath = ConfigurationManager.AppSettings["rutaLog"];
            logName = ConfigurationManager.AppSettings["nombreLog"];
            printQueriesLog = ConfigurationManager.AppSettings["imprimirQueriesDBLog"];
            //Configuración Servicios
            client = ConfigurationManager.AppSettings["client"]?.ToString();
            codeDMJson = ConfigurationManager.AppSettings["codeDM"]?.ToString();
            userJson = ConfigurationManager.AppSettings["user"]?.ToString();
            userName = ConfigurationManager.AppSettings["userName"]?.ToString();
            password = ConfigurationManager.AppSettings["password"]?.ToString();
            endPointBase = ConfigurationManager.AppSettings["endPointBase"]?.ToString();
            endPointHostQuery = ConfigurationManager.AppSettings["endPointHostQuery"]?.ToString();
            endPointResultados = ConfigurationManager.AppSettings["endPointResultados"]?.ToString();
            endPointToken = ConfigurationManager.AppSettings["endPointToken"]?.ToString();
            retransmitionPath = ConfigurationManager.AppSettings["rutaRetransmision"]?.ToString();
            okFilesPath = ConfigurationManager.AppSettings["rutaArchivosOK"]?.ToString();
            errorFilesPath = ConfigurationManager.AppSettings["rutaArchivosError"]?.ToString();
            portInterface = ConfigurationManager.AppSettings["puerto"]?.ToString();
            ipServer = ConfigurationManager.AppSettings["ipServidor"]?.ToString();
            socketReconnectionAttemps = Convert.ToInt32(ConfigurationManager.AppSettings["intentosReconexionSocket"]?.ToString());
            cantidadMensajesVista = Convert.ToInt32(ConfigurationManager.AppSettings["cantidadMensajesVista"]?.ToString());
        }
    }
}
