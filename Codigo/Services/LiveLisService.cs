namespace BS360.Services;

public static class LiveLisService
{
    private static readonly string nombreLog = InterfaceConfig.logName + "_JSON";
    //private static Resultados formResultados;
    public static string token = string.Empty;


    public static bool ServidorActivo()
    {
        try
        {
            string host = new Uri(InterfaceConfig.endPointBase).Host;

            using (Ping ping = new Ping())
            {
                PingOptions options = new PingOptions { DontFragment = true };
                string data = "data";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 3000;

                PingReply reply = ping.Send(host, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("El servidor está activo.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"El servidor no está activo. Estado del ping: {reply.Status}");
                    RegisterLog.Instance.RegisterInLog($"El servidor no está activo. Estado del ping: {reply.Status}", nombreLog, TerminalStates.ERROR);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"El servidor no está activo. Valide el endpoint-ValideConexion: {ex.Message}");
            return false;
        }
    }



    public static ResultadosInsertarTextoPlano EnviarResultados(ObjResultados resultado)
    {
        ResultadosInsertarTextoPlano resultadosAInsertarTextoPlano = new ResultadosInsertarTextoPlano();

        if (ValidateToken())
        {
            if (string.IsNullOrEmpty(token))
            {
                RegisterLog.Instance.RegisterInLog("Se Solicita Token..", nombreLog, TerminalStates.PROCESS);
            }
            else
            {

                RegisterLog.Instance.RegisterInLog("El token expiró, se solicita uno nuevo.", nombreLog, TerminalStates.PROCESS);
            }

            ObtenerToken();
        }

        if (string.IsNullOrEmpty(token))
        {

            RegisterLog.Instance.RegisterInLog("ERROR token no se obtuvo correctamente", nombreLog, TerminalStates.ERROR);
            return null;
        }
        try
        {
            var options = new RestClientOptions(InterfaceConfig.endPointBase) { MaxTimeout = -1 };
            var client = new RestClient(options);


            RestRequest request = new RestRequest($"{InterfaceConfig.endPointResultados}", Method.Post)
                .AddHeader("Authorization", $"Bearer {token}")
                .AddHeader("Content-Type", "application/json")
                .AddHeader("Client", $"{InterfaceConfig.client}")
                .AddHeader("Cookie", "ARRAffinity=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f; ARRAffinitySameSite=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f");

            RegisterLog.Instance.RegisterInLog("----------------------------- Headers y Body enviados ------------------------", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"RestRequest({InterfaceConfig.endPointResultados}, Method.Post", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog("Content-Type, \"application/json\"", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Client, {InterfaceConfig.client}", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Body: {JsonConvert.SerializeObject(resultado, Formatting.Indented)}", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog("--------------------------------------------------------------------------", nombreLog, TerminalStates.PROCESS, false);

            request.AddBody(JsonConvert.SerializeObject(resultado));

            RestResponse response = client.Execute(request);
            RespuestaEnvioResultados respuestaEnvio = JsonConvert.DeserializeObject<RespuestaEnvioResultados>(response.Content);


            RegisterLog.Instance.RegisterInLog("------------------------- Respuesta Recibida ------------------------------", nombreLog, TerminalStates.PROCESS);

            if (response.IsSuccessful)
            {

                RegisterLog.Instance.RegisterInLog("Resultados enviados.", nombreLog, TerminalStates.PROCESS);

                if (respuestaEnvio.ok)
                {

                    RegisterLog.Instance.RegisterInLog($"Respuesta del servidor[{JsonConvert.SerializeObject(respuestaEnvio, Formatting.Indented)}]", nombreLog, TerminalStates.PROCESS);
                    RegisterLog.Instance.RegisterInLog("--------------------------------------------------------------------------", nombreLog, TerminalStates.PROCESS);
                    ObtenerAnalitosRespuesta(resultadosAInsertarTextoPlano, resultado, respuestaEnvio);
                    return resultadosAInsertarTextoPlano;
                }
                else
                {

                    RegisterLog.Instance.RegisterInLog($"Respuesta del servidor[{JsonConvert.SerializeObject(respuestaEnvio, Formatting.Indented)}]", nombreLog, TerminalStates.ERROR);
                    RegisterLog.Instance.RegisterInLog("--------------------------------------------------------------------------", nombreLog, TerminalStates.PROCESS);
                    ObtenerAnalitosRespuesta(resultadosAInsertarTextoPlano, resultado, respuestaEnvio);

                    return resultadosAInsertarTextoPlano;
                }
            }
            else
            {

                ManejoExcepciones.ManejoErrores(response);
                return null;
            }
        }
        catch (Exception ex)
        {

            RegisterLog.Instance.RegisterInLog($"ERROR en EnviarResultados: {ex.Message}", nombreLog, TerminalStates.ERROR);
            return null;
        }
        return resultadosAInsertarTextoPlano;
    }


    public static DatosPaciente[] EnviarHostQuery(objSolicitudHostQuery resultado)
    {
        if (ValidateToken())
        {
            if (string.IsNullOrEmpty(token))
            {
                RegisterLog.Instance.RegisterInLog($"Se Solicita Token..", nombreLog, TerminalStates.PROCESS);
            }
            else
            {
                RegisterLog.Instance.RegisterInLog($"El token expiro, se solicita uno nuevo.", nombreLog, TerminalStates.PROCESS);
            }

            ObtenerToken();
        }

        if (string.IsNullOrEmpty(token))
        {
            RegisterLog.Instance.RegisterInLog($"ERROR token no se obtuvo correctamente", nombreLog, TerminalStates.ERROR);
            return null;
        }


        try
        {
            var options = new RestClientOptions(InterfaceConfig.endPointBase)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            RestRequest request = new RestRequest($"{InterfaceConfig.endPointHostQuery}", Method.Post)
                        .AddHeader("Authorization", $"Bearer {token}")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Client", $"{InterfaceConfig.client}")
                        .AddHeader("Cookie", "ARRAffinity=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f; ARRAffinitySameSite=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f");

            RegisterLog.Instance.RegisterInLog($"----------------------------- Headers y Body enviados ------------------------", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"RestRequest({InterfaceConfig.endPointHostQuery}, Method.Post", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Content-Type,\"application/json\"", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Client, {InterfaceConfig.client}", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Body: {JsonConvert.SerializeObject(resultado)} ", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS, false);

            request.AddBody(JsonConvert.SerializeObject(resultado));

            RestResponse response = client.Execute(request);
            RespuestaProgramacion respuesta = JsonConvert.DeserializeObject<RespuestaProgramacion>(response.Content);
            RegisterLog.Instance.RegisterInLog($"------------------------- Respuesta Recibida ------------------------------", nombreLog, TerminalStates.PROCESS);
            if (response.IsSuccessful)
            {

                if (respuesta.ok)
                {
                    RegisterLog.Instance.RegisterInLog($"La respuesta del consumo fue positiva. Respuesta[{respuesta.message}]", nombreLog, TerminalStates.PROCESS);
                    RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS);
                    return respuesta.data;
                }
                else
                {
                    RegisterLog.Instance.RegisterInLog($"La respuesta del consumo fue negativa. Respuesta[{respuesta.message}]", nombreLog, TerminalStates.ERROR);
                    RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS);
                    return null;
                }
            }
            else
            {

                ManejoExcepciones.ManejoErrores(response);
                return null;
            }

        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"ERROR en EnviarResultados: {ex.Message}", nombreLog, TerminalStates.ERROR);
            return null;
        }

    }

    public static void ObtenerToken()
    {
        try
        {
            var options = new RestClientOptions(InterfaceConfig.endPointBase)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            RestRequest request = new RestRequest($"{InterfaceConfig.endPointToken}", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("Client", $"{InterfaceConfig.client}")
                .AddHeader("Cookie", "ARRAffinity=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f; ARRAffinitySameSite=2bd31e84ea0f19c15dd402e7a497a7675b0400bcd5ce5ea73b2f33494c77e44f");

            RegisterLog.Instance.RegisterInLog($"----------------------------- Headers y Body enviados ------------------------", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"RestRequest({InterfaceConfig.endPointToken}, Method.Post", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Content-Type,\"application/json\"", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Client,{InterfaceConfig.client}", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"Body{{UserName = {InterfaceConfig.userName}, Password {InterfaceConfig.password}}}", nombreLog, TerminalStates.PROCESS, false);
            RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS, false);

            ObjToken objToken = new();
            objToken.userName = InterfaceConfig.userName;
            objToken.userPassword = InterfaceConfig.password;
            request.AddBody(JsonConvert.SerializeObject(objToken));
            RestResponse response = client.Execute(request);

            RegisterLog.Instance.RegisterInLog($"------------------------- Respuesta Recibida ------------------------------", nombreLog, TerminalStates.PROCESS);
            if (response.IsSuccessful)
            {
                RespuestaLoginToken respuestaEnvio = JsonConvert.DeserializeObject<RespuestaLoginToken>(response.Content);
                if (respuestaEnvio.ok)
                {
                    RegisterLog.Instance.RegisterInLog($"Token obtenido correctamente.", nombreLog, TerminalStates.PROCESS);
                    RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS);

                    token = respuestaEnvio.data.ToString();
                }
                else
                {
                    RegisterLog.Instance.RegisterInLog($"ERROR obteniendo token. [{respuestaEnvio.message}]", nombreLog, TerminalStates.ERROR);
                    RegisterLog.Instance.RegisterInLog($"-------------------------------------------------------------------------- \n", nombreLog, TerminalStates.PROCESS);
                    token = string.Empty;
                }
            }
            else
            {
                //ManejoExcepciones.ManejoERRORes(response);
                token = string.Empty;
            }
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"ERROR en Obtener Token: {ex.Message}", nombreLog, TerminalStates.ERROR);
            token = string.Empty;
        }
    }

    private static bool ValidateToken()
    {
        try
        {
            if (string.IsNullOrEmpty(token)) return true;
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo;

            return expirationDate < DateTime.Now;
        }
        catch (Exception ex)
        {
            RegisterLog.Instance.RegisterInLog($"ERROR en Validar Token: {ex.Message}", nombreLog, TerminalStates.ERROR);
            return false;
        }
    }


    public static void ObtenerAnalitosRespuesta(ResultadosInsertarTextoPlano resultadosAInsertarTextoPlano, ObjResultados resultado, RespuestaEnvioResultados respuestaEnvio)
    {
        resultadosAInsertarTextoPlano.CodeDM = resultado.CodeDM;
        resultadosAInsertarTextoPlano.ArrayAnalitosInsertadosConError = resultado.InsertResult.Where(e => respuestaEnvio.data.errors.Any(s => e.SampleNumber == s.SampleNumber && e.Homologation == s.Homologation && e.Result == s.Result)).ToList();
        resultadosAInsertarTextoPlano.ArrayAnalitosInsertadosOk = resultado.InsertResult.Where(e => respuestaEnvio.data.success.Any(s => e.SampleNumber == s.sampleNumber && e.Homologation == s.homologation && e.Result == s.result)).ToList();

    }
}
