namespace BS360.Services;

public class TcpMessagingService
{
    private readonly FileService fileService = new FileService();
    private ProcessResultsService processResultsService;
    private TcpCommunication socketClient;
    private List<string> ArrPaquete = new List<string>();
    private string strTramaRecibida = "";
    private string strTramaAnterior = "";

    public void StartReceptionFromServer(TcpCommunication socketClient)
    {
        this.socketClient = socketClient;
        processResultsService = new ProcessResultsService(socketClient);

        int ciclos = 10;
        while (ciclos <= 10)
        {
            this.socketClient.DataReceived += socketServidor_ReceivedData;
            ciclos++;
        }
    }

    public void socketServidor_ReceivedData(string datos)
    {
        try
        {
            RegisterLog.Instance.RegisterInLog($"Paquete parcial recibido --> [{datos}]", InterfaceConfig.equipmentName, TerminalStates.OK, false);
            strTramaRecibida += datos;

            int countVT = strTramaRecibida.Count(c => c == BsAscii.VT);
            int countFS = strTramaRecibida.Count(c => c == BsAscii.FS);

            if ((countVT == countFS) && (countFS != 0))
            {
                RegisterLog.Instance.RegisterInLog($"Paquete completo recibido --> [{strTramaRecibida.Replace('\0', ' ')}]", InterfaceConfig.equipmentName, TerminalStates.PROCESS);
                countVT = 0;
                countFS = 0;

                strTramaRecibida = strTramaRecibida.Replace(BsAscii.VT.ToString(), "");
                strTramaRecibida = strTramaRecibida.Trim('\0');
                strTramaRecibida = strTramaRecibida.Trim();
                strTramaRecibida = strTramaRecibida.Replace(BsAscii.LF.ToString(), "");

                string[] arrTramaRecibida = strTramaRecibida.Split(BsAscii.FS);
                string[] substrings = strTramaRecibida.Split(BsAscii.CR);

                if (arrTramaRecibida.Length > 1)
                {
                    strTramaAnterior = arrTramaRecibida[1].Trim('\0');
                }

                int x = 0;

                for (int i = 0; i < substrings.Length; i++)
                {
                    ArrPaquete.Add(substrings[i]);
                }

                processResultsService.ProcessResults(ArrPaquete);
                ArrPaquete.Clear();

                fileService.ReadFlatFiles();
                strTramaAnterior = "";
                strTramaRecibida = "";
            }
        }
        catch (Exception ex)
        {
            strTramaAnterior = "";
            strTramaRecibida = "";
            ArrPaquete.Clear();
            RegisterLog.Instance.RegisterInLog("Error en SocketCliente_DatosRecibidos: " + ex.Message, InterfaceConfig.equipmentName, TerminalStates.ERROR);
        }
    }
}
