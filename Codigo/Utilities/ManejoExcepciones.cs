namespace BS360.Utilities;

public class ManejoExcepciones
{
    private static readonly string nombreLog = InterfaceConfig.logName + "_JSON";

    public static void ManejoErrores(RestResponse response)
    {
        // Puedes agregar más lógica aquí según tus necesidades
        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                RegisterLog.Instance.RegisterInLog("Recurso no encontrado. Verifica la URL de la solicitud.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.BadRequest:
                RegisterLog.Instance.RegisterInLog("Solicitud incorrecta. Verifica los datos enviados en la solicitud.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.Unauthorized:
                RegisterLog.Instance.RegisterInLog("No autorizado. Verifica las credenciales o permisos de acceso.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.UnsupportedMediaType:
                RegisterLog.Instance.RegisterInLog("El tipo de media no es el adecuado. Verifica el Content-Type de los headers.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.Forbidden:
                RegisterLog.Instance.RegisterInLog("Acceso prohibido. No tienes los permisos necesarios para realizar la solicitud.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.InternalServerError:
                RegisterLog.Instance.RegisterInLog("Error interno del servidor. Inténtalo de nuevo más tarde.", nombreLog, TerminalStates.ERROR);
                break;
            case HttpStatusCode.ServiceUnavailable:
                RegisterLog.Instance.RegisterInLog("Servicio no disponible. Inténtalo de nuevo más tarde.", nombreLog, TerminalStates.ERROR);
                break;
            default:
                RegisterLog.Instance.RegisterInLog($"Código de estado no manejado: {response.StatusCode}", nombreLog, TerminalStates.ERROR);
                break;
        }

        if (response.ErrorException != null)
        {
            RegisterLog.Instance.RegisterInLog($"Error en la solicitud: {response.ErrorException.Message}", nombreLog, TerminalStates.ERROR);
        }

        if (!string.IsNullOrWhiteSpace(response.Content))
        {
            try
            {
                var errorDetails = JsonConvert.DeserializeAnonymousType(response.Content, new { Message = "" });
                if (!string.IsNullOrWhiteSpace(errorDetails?.Message))
                {
                    RegisterLog.Instance.RegisterInLog($"Detalles del error: {errorDetails.Message}", nombreLog, TerminalStates.ERROR);
                }
            }
            catch (JsonException)
            {
                // No se pudo analizar el contenido como JSON
            }
        }

        RegisterLog.Instance.RegisterInLog("--------------------------------------------------------------------------", nombreLog, TerminalStates.ERROR);
    }
}
