namespace BS360.Utilities;

internal class RegisterLog
{
    private static readonly Lazy<RegisterLog> instance = new Lazy<RegisterLog>(() => new RegisterLog());
    public static RegisterLog Instance => instance.Value;

    private bool logIniciado = false;
    private string logName = "Log_";
    private bool catchEjecutado = false;
    private static readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

    private RegisterLog() { }

    public void InitializeLog(string p_equipo)
    {
        string RutaLog = InterfaceConfig.logPath;
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        try
        {
            logName = Path.Combine(RutaLog, $"Log_{p_equipo}_v{version}_{DateTime.Now:ddMMyyyy}");
            using (StreamWriter w = File.AppendText($"{logName}.txt"))
            {
                w.WriteLine($"\r\nLog {p_equipo} v{version} : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("------------------------------------------------------------------------------------------------");
            }
            if (p_equipo != "Desconocido") logIniciado = true;
        }
        catch (Exception)
        {

        }
    }

    public void RegisterInLog(string logMessage, string p_device, TerminalStates state, bool proyectOnInterface = true)
    {
        bool activateLog = InterfaceConfig.activateLog;
        string logPath = InterfaceConfig.logPath;
        var version = Assembly.GetExecutingAssembly().GetName().Version;

        try
        {
            if (activateLog)
            {
                logName = Path.Combine(logPath, $"Log_{p_device}_v{version}_{DateTime.Now:ddMMyyyy}");
                if (!logIniciado) InitializeLog(p_device);
                using (StreamWriter w = File.AppendText($"{logName}.txt"))
                {
                    w.WriteLine($"{DateTime.Now}  :  {logMessage}");
                }
            }
        }
        catch (Exception) { }

        if (proyectOnInterface) dispatcher.Invoke(() => MessageService.Instance.AddMessage(logMessage, state));
    }
}
public static class ILog
{
    public static string LogPath { get; set; } = InterfaceConfig.logPath;
    public static bool activateLog { get; set; }
}
