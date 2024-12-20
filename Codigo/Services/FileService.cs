namespace BS360.Services;

public class FileService
{
    public void ReadFlatFiles()
    {
        string archivo = Directory.GetFiles(InterfaceConfig.retransmitionPath, "*.txt").FirstOrDefault();
        List<ResultadosInsertarTextoPlano> resultadosServicio = new List<ResultadosInsertarTextoPlano>();

        if (LiveLisService.ServidorActivo() && archivo != null)
        {
            try
            {
                RegisterLog.Instance.RegisterInLog($"Restasmitiendo archivo [{archivo}]", InterfaceConfig.equipmentName, TerminalStates.WARNING);

                string contenido = File.ReadAllText(archivo);
                List<string> resultados = contenido.Split(BsAscii.ETX).ToList();

                foreach (var resultadoJson in resultados)
                {
                    try
                    {
                        if (resultadoJson == BsAscii.BR || resultadoJson == string.Empty) continue;
                        JObject objResul = JObject.Parse(resultadoJson);

                        resultadosServicio.Add(LiveLisService.EnviarResultados(objResul.ToObject<ObjResultados>()));
                    }
                    catch (JsonReaderException jsonEx)
                    {
                        RegisterLog.Instance.RegisterInLog($"Error al procesar JSON en archivo [{archivo}]. Detalles: {jsonEx.Message}", InterfaceConfig.equipmentName, TerminalStates.ERROR);
                    }
                    catch (Exception ex)
                    {
                        RegisterLog.Instance.RegisterInLog($"Error inesperado al procesar archivo [{archivo}]. Detalles: {ex.Message}", InterfaceConfig.equipmentName, TerminalStates.ERROR);
                    }
                }

                foreach (var resultado in resultadosServicio)
                {
                    ObjResultados objResultado = new ObjResultados();
                    if (resultados == null)
                    {
                        continue;
                    }

                    if (resultado.ArrayAnalitosInsertadosOk.Count > 0)
                    {
                        objResultado.CodeDM = resultado.CodeDM;
                        objResultado.InsertResult = resultado.ArrayAnalitosInsertadosOk;
                        CreateFlatFile(objResultado, InterfaceConfig.okFilesPath, "Resultados");
                    }

                    if (resultado.ArrayAnalitosInsertadosConError.Count > 0)
                    {
                        objResultado.CodeDM = resultado.CodeDM;
                        objResultado.InsertResult = resultado.ArrayAnalitosInsertadosConError;
                        CreateFlatFile(objResultado, InterfaceConfig.errorFilesPath, "Resultados");
                    }

                    DeleteRetransmitionFile(archivo);
                    continue;
                }
            }
            catch (Exception ex)
            {
                RegisterLog.Instance.RegisterInLog($"Error leyendo archivos de retransmisión [{ex}]", InterfaceConfig.equipmentName, TerminalStates.ERROR);
            }
        }
        else return;
        
    }

    public void DeleteRetransmitionFile(string archivo)
    {
        try
        {
            if (File.Exists(archivo))
            {
                File.Delete(archivo);
                RegisterLog.Instance.RegisterInLog($"Archivo [{archivo}] eliminado", InterfaceConfig.equipmentName, TerminalStates.PROCESS);
            }
            else RegisterLog.Instance.RegisterInLog($"El archivo [{archivo}] no existe", InterfaceConfig.equipmentName, TerminalStates.PROCESS);            
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"Error al eliminar el archivo [{archivo}]. Detalles: {ex.Message}", InterfaceConfig.equipmentName, TerminalStates.ERROR);
        }
    }

    public void CreateFlatFile(ObjResultados resultado, string rutaArchivos, string nombreArchivo)
    {
        string path;
        string fecha = DateTime.Now.ToString("yyyy-MM-dd");
        string sampleNumber = resultado.InsertResult.First().SampleNumber;

        try
        {
            if (Directory.Exists(rutaArchivos))
            {
                path = Path.Combine(rutaArchivos, $"{nombreArchivo}-{sampleNumber}-{fecha}.txt");
            }
            else
            {
                Directory.CreateDirectory(rutaArchivos);
                path = Path.Combine(rutaArchivos, $"{nombreArchivo}-{sampleNumber}-{fecha}.txt");
            }
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                RegisterLog.Instance.RegisterInLog($"Archivo creado[{path}]", InterfaceConfig.equipmentName, TerminalStates.PROCESS);
                sw.WriteLine(JsonConvert.SerializeObject(resultado, Formatting.Indented) + BsAscii.ETX);
                sw.Close();
            }
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"Error procesando archivo, Mensaje[{ex.Message}]", InterfaceConfig.equipmentName, TerminalStates.ERROR);
        }
    }
}
