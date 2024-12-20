namespace BS360.Services;

public class ProcessResultsService
{
    private readonly FileService fileService = new FileService();
    private TcpCommunication socketClient;

    public static string registraevento = "N";
    public static string descripcionequipo = "BS-200";
    public static string consecutivoMsg = "";
    public static string strLineaOrdenBS = "";
    public static string strTipoResultado = "";
    public static string strLineaOrdenBSQCK = "";

    public ProcessResultsService(TcpCommunication socketClient) => this.socketClient = socketClient;  

    public void ProcessResults(List<string> PaqueteResultado)
    {
        ObjResultados objResul = new();
        Insertresult insertresult = new();

        string strOrdenResultado = string.Empty;
        string strFechaToma = string.Empty;
        string nroDocumento = string.Empty;
        List<Insertresult> ListaResultados = [];

        try
        {
            RegisterLog.Instance.RegisterInLog("Paquete recibido:", InterfaceConfig.logName, TerminalStates.PROCESS);

            for (int x = 0; x <= PaqueteResultado.Count - 1; x++)
            {

                if (string.IsNullOrEmpty(PaqueteResultado[x])) continue;

                RegisterLog.Instance.RegisterInLog("-->" + PaqueteResultado[x], InterfaceConfig.logName, TerminalStates.PROCESS);
                string strlinea = PaqueteResultado[x];
                string[] arrLinea = strlinea.Split('|');
                string strEncabezado = arrLinea[0];

                switch (strEncabezado)
                {
                    case "MSH":
                        consecutivoMsg = arrLinea[9];
                        strTipoResultado = arrLinea[15];
                        objResul.CodeDM = InterfaceConfig.codeDMJson;
                        break;

                    case "PID":
                        nroDocumento = arrLinea[1];
                        RegisterLog.Instance.RegisterInLog("Envio Respuesta Correcta al BS PID--> " + ObtenerMensajePositivoRes(), InterfaceConfig.logName, TerminalStates.PROCESS);
                        socketClient.SendData(ObtenerMensajePositivoRes());
                        break;

                    case "OBR":
                        strOrdenResultado = arrLinea[2];
                        RegisterLog.Instance.RegisterInLog("Resultados para Nro Tubo: " + strOrdenResultado, InterfaceConfig.equipmentName, TerminalStates.PROCESS);
                        RegisterLog.Instance.RegisterInLog("Tipo de resultados: " + strTipoResultado, InterfaceConfig.equipmentName, TerminalStates.PROCESS);

                        string mensajeControl = ObtenerMensajeControl();
                        RegisterLog.Instance.RegisterInLog("Envio Respuesta Control al BS PID--> " + mensajeControl, InterfaceConfig.equipmentName, TerminalStates.PROCESS);
                        socketClient.SendData(mensajeControl);

                        if (string.IsNullOrEmpty(strOrdenResultado))
                        {
                            RegisterLog.Instance.RegisterInLog("No hay numero de tubo", InterfaceConfig.equipmentName, TerminalStates.PROCESS);
                            continue;
                        }

                        break;

                    case "OBX":
                        try
                        {
                            Insertresult insertresulta = new Insertresult();

                            string analito = arrLinea[3];
                            string strResultado = true ? arrLinea[13] : arrLinea[5];
                            string strUnidades = arrLinea[6];
                            RegisterLog.Instance.RegisterInLog("Variable: " + analito + " Resultado: " + strResultado, InterfaceConfig.logName, TerminalStates.PROCESS);
                            insertresulta.SampleNumber = strOrdenResultado.ToString();
                            insertresulta.NumberDocument = nroDocumento;
                            insertresulta.Homologation = analito;
                            insertresulta.Result = strResultado;
                            insertresulta.UnitsMeasurement = strUnidades;
                            ListaResultados.Add(insertresulta);
                        }
                        catch (Exception ex)
                        {
                            RegisterLog.Instance.RegisterInLog("Error Cargando Linea: " + strlinea, InterfaceConfig.logName, TerminalStates.PROCESS);
                        }
                        break;

                    case "QRD":
                        try
                        {
                            strOrdenResultado = arrLinea[8];
                            RegisterLog.Instance.RegisterInLog("Nro Tubo Query: " + strOrdenResultado, InterfaceConfig.logName, TerminalStates.PROCESS);
                            QuerySample(strOrdenResultado);
                        }
                        catch (Exception ex)
                        {
                            RegisterLog.Instance.RegisterInLog("Error Cargando Linea 2: " + strlinea, InterfaceConfig.logName, TerminalStates.PROCESS);
                        }
                        break;

                    case "QRF":
                        // Acción opcional para "QRF"
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog("Se generó un error procesando la trama, verifique logs", InterfaceConfig.logName, TerminalStates.PROCESS);
            RegisterLog.Instance.RegisterInLog($"Error: [{ex}]", InterfaceConfig.logName, TerminalStates.PROCESS);
        }

        if (ListaResultados.Count > 0)
        {
            if (LiveLisService.ServidorActivo())
            {
                objResul.InsertResult = ListaResultados;

                ResultadosInsertarTextoPlano resultados = LiveLisService.EnviarResultados(objResul);
                ObjResultados objResultado = new();
                if (resultados == null)
                {
                    fileService.CreateFlatFile(objResultado, InterfaceConfig.errorFilesPath, "Resultados");
                }
                if (resultados.ArrayAnalitosInsertadosOk.Count > 0)
                {
                    objResultado.CodeDM = resultados.CodeDM;
                    objResultado.InsertResult = resultados.ArrayAnalitosInsertadosOk;
                    fileService.CreateFlatFile(objResultado, InterfaceConfig.okFilesPath, "Resultados");
                }

                if (resultados.ArrayAnalitosInsertadosConError.Count > 0)
                {
                    objResultado.CodeDM = resultados.CodeDM;
                    objResultado.InsertResult = resultados.ArrayAnalitosInsertadosConError;

                    fileService.CreateFlatFile(objResultado, InterfaceConfig.errorFilesPath, "Resultados");
                }
            }
            else fileService.CreateFlatFile(objResul, InterfaceConfig.retransmitionPath, "Resultados");            
        }
    }

    private void QuerySample(string strOrdenResultado)
    {
        List<Samplenumber> strOrdenResultadArray = new();
        List<DatosPaciente> strOrdenResultadArray2 = new();
        Samplenumber strOrdenResultad = new();
        strOrdenResultad.SampleNumber = strOrdenResultado;
        strOrdenResultadArray.Add(strOrdenResultad);
        objSolicitudHostQuery objSolicitudHostQuery = new() { CodeDM = InterfaceConfig.codeDMJson, SampleNumbers = strOrdenResultadArray };

        DatosPaciente[] respuestaProgramacion = LiveLisService.EnviarHostQuery(objSolicitudHostQuery);

        if (respuestaProgramacion != null)
        {
            string strFechaMg = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmmss");

            RegisterLog.Instance.RegisterInLog("Envio Respuesta correcta al BS QRD--> " + ObtenerMensajePositivo(), InterfaceConfig.logName, TerminalStates.PROCESS, false);
            socketClient.SendData(ObtenerMensajePositivo());


            strLineaOrdenBSQCK = "";
            strLineaOrdenBSQCK = $"{BsAscii.VT}MSH|^~\\&|Mindray|{descripcionequipo}|||{strFechaMg}||ACK^Q03|{consecutivoMsg}|P|2.3.1||||0||BsAscii|||" + BsAscii.CR;
            strLineaOrdenBSQCK += $"MSA|AA|{consecutivoMsg}|Message accepted|||0|" + BsAscii.CR;
            strLineaOrdenBSQCK += "ERR|0|" + BsAscii.CR + BsAscii.FS + BsAscii.CR;

            RegisterLog.Instance.RegisterInLog("Envio Respuesta correcta al BS QRD--> " + strLineaOrdenBSQCK, InterfaceConfig.logName, TerminalStates.PROCESS, false);
            socketClient.SendData(strLineaOrdenBSQCK);
        }
        else
        {
            RegisterLog.Instance.RegisterInLog("Envio Respuesta Incorrecta al BS QRD--> " + ObtenerMensajeNoData(), InterfaceConfig.logName, TerminalStates.PROCESS, false);
            socketClient.SendData(ObtenerMensajeNoData());
            return;
        }

        strOrdenResultadArray2.AddRange(respuestaProgramacion);

        BuscarOrdenes(strOrdenResultadArray2, strOrdenResultado);

        if (respuestaProgramacion != null)
        {
            RegisterLog.Instance.RegisterInLog("Envio Paquete al BS--> " + strLineaOrdenBS, InterfaceConfig.logName, TerminalStates.PROCESS);
            socketClient.SendData(strLineaOrdenBS);
        }
    }   

    private void BuscarOrdenes(List<DatosPaciente> pacientes, string muestratubo)
    {
        int SeqOrden = 29;

        string strFechaBs = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmmss");
        try
        {
            string strLineaOrdenBS1 = "";
            string sex = pacientes[0].sex == "Hombre" ? "H" : pacientes[0].sex == "Mujer" ? "M" : "O";


            strLineaOrdenBS = $"MSH|^~\\&|||Mindray|{descripcionequipo}|{strFechaBs}||DSR^Q03|{consecutivoMsg}|P|2.3.1||||||BsAscii|||" + BsAscii.CR;
            strLineaOrdenBS += "MSA|AA|" + consecutivoMsg + "|Message accepted|||0|" + BsAscii.CR;
            strLineaOrdenBS += "ERR|0|" + BsAscii.CR;
            strLineaOrdenBS += "QAK|SR|OK|" + BsAscii.CR;
            strLineaOrdenBS += "QRD|" + strFechaBs + "|R|D|68|||RD|" + muestratubo + "|OTH|||T|" + BsAscii.CR;
            strLineaOrdenBS += $"QRF|{descripcionequipo}|{strFechaBs}|{strFechaBs}|||RCT|COR|ALL||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|1||" + consecutivoMsg + "|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|2|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|3||" + pacientes[0].name.Trim().Replace("|", " ").Trim() + "|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|4||" + pacientes[0].birthDate.ToString("yyyyMMddHHmmss") + "|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|5||" + sex + "|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|6|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|7|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|8|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|9|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|10|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|11|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|12|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|13|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|14|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|15|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|16|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|17|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|18|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|19|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|20|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|21||" + muestratubo + "|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|22|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|23|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|24|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|25|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|26||serum|||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|27|||||" + BsAscii.CR;
            strLineaOrdenBS += "DSP|28|||||" + BsAscii.CR;


            string strLineaExamenesBs = string.Empty;

            foreach (var respuestaProgramacion in pacientes[0].analytes)
            {
                strLineaOrdenBS1 += "DSP|" + Convert.ToString(SeqOrden++).Trim() + "||" + respuestaProgramacion.hostQueryCode + "^^^|||" + BsAscii.CR;
            }

            strLineaOrdenBS += strLineaOrdenBS1;
            strLineaOrdenBS += "DSC||" + BsAscii.CR;
            strLineaOrdenBS = BsAscii.VT + strLineaOrdenBS + BsAscii.FS + BsAscii.CR;

        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog("Error buscando Ordenes : " + ex.Message, InterfaceConfig.logName, TerminalStates.ERROR);
        }
    }

    public string ObtenerMensajePositivo()
    {
        string strFecha = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmmss");

        string strMSH = $"MSH|^~\\&|||Mindray|{descripcionequipo}|{strFecha}||QCK^Q02|{consecutivoMsg}|P|2.3.1||||||BsAscii|||";
        string strMSA = $"MSA|AA|{consecutivoMsg}|Message accepted|||0|";
        string strERR = "ERR|0|";
        string strQAK = "QAK|SR|OK|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + strERR + BsAscii.CR + strQAK + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }

    public string ObtenerMensajeNoData()
    {
        DateTime fecha = DateTime.Now;

        string strFecha = DateTime.Now.ToString("yyyyMMddHHmmss");

        string strMSH = $"MSH|^~\\&|||Mindray|{descripcionequipo}|{strFecha}||QCK^Q02|{consecutivoMsg}|P|2.3.1|||||||BsAscii|0|";
        string strMSA = $"MSA|AA|{consecutivoMsg}|Message accepted|||0|";
        string strERR = "ERR|0|";
        string strQAK = "QAK|SR|NF|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + strERR + BsAscii.CR + strQAK + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }

    public string ObtenerMensajePositivoRes()
    {
        DateTime fecha = DateTime.Now;

        string strFecha = DateTime.Now.ToString("yyyyMMddHHmmss");
        string strMSH = $"MSH|^~\\&|||BioCLIA6500|{descripcionequipo}|{strFecha}||ACK^R01|{consecutivoMsg}|P|2.3.1||||{strTipoResultado}||BsAscii|||";
        string strMSA = "MSA|AR|1|Message accepted|||0|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }
    public static string ObtenerMensajePositivoResQC()
    {
        DateTime fecha = DateTime.Now;

        string strFecha = DateTime.Now.ToString("yyyyMMddHHmmss");
        string strMSH = $"MSH|^~\\&|||BioCLIA6500|{descripcionequipo}|{strFecha}||ACK^R01|{consecutivoMsg}|P|2.3.1||||{strTipoResultado}||BsAscii|||";
        string strMSA = "MSA|AA|1|Message accepted|||0|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }

    public string ObtenerMensajeControl()
    {
        DateTime fecha = DateTime.Now;

        string strFecha = DateTime.Now.ToString("yyyyMMddHHmmss");
        string strMSH = $"MSH|^~\\&|||BioCLIA6500|{descripcionequipo}|{strFecha}||ACK^R01|{consecutivoMsg}|P|2.3.1||||{strTipoResultado}||BsAscii|||";
        string strMSA = "MSA|AA|1|Message accepted|||0|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }

    public string ObtenerMensajeNo()
    {
        DateTime fecha = DateTime.Now;

        string strFecha = DateTime.Now.ToString("yyyyMMddHHmmss");

        string strMSH = $"MSH|^~\\&|ANNARLAB||BioCLIA6500|{descripcionequipo}|{strFecha}||QCK^Q02|{consecutivoMsg}|P|2.3.1||||||BsAscii|||";
        string strMSA = $"MSA|AA|{consecutivoMsg}|Message accepted|||0|";
        string strERR = "ERR|0|";
        string strQAK = "QAK|SR|NF|";
        string strMensaje = BsAscii.VT + strMSH + BsAscii.CR + strMSA + BsAscii.CR + strERR + BsAscii.CR + strQAK + BsAscii.CR + BsAscii.FS + BsAscii.CR;
        return strMensaje;
    }
}
