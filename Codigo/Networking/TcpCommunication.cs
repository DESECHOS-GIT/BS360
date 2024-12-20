namespace BS360.Networking;

public class TcpCommunication
{
    private Stream messagesSendReceive; // To send and receive data from the server
    private string serverIP; // Server IP address
    private int serverPort; // Listening port

    private TcpClient tcpClient;
    private Thread serverMessageThread; // Thread to listen for server messages
    private volatile bool isRunning = true; // Flag to control the thread execution

    public event Action ConnectionTerminated;
    public event Action<string> DataReceived;
    // Dirección IP del servidor al que nos conectaremos

    public string IP
    {
        get { return serverIP; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La dirección IP no puede estar vacía.");
            }
            serverIP = value;
        }
    }

    // Port por el que realizar la conexión al servidor
    public int Port
    {
        get { return serverPort; }
        set
        {
            if (value <= 0 || value > 65535)
            {
                throw new ArgumentOutOfRangeException("El puerto debe estar entre 1 y 65535.");
            }
            serverPort = value;
        }
    }

    // Procedimiento para realizar la conexión con el servidor
    public void ConnectSocket()
    {
        if (tcpClient != null && tcpClient.Connected)
        {
            throw new InvalidOperationException("Ya existe una conexión activa.");
        }

        tcpClient = new TcpClient();

        try
        {
            // Conectar con el servidor
            tcpClient.Connect(IP, Port);
            messagesSendReceive = tcpClient.GetStream();

            // Crear hilo para establecer escucha de posibles mensajes
            // enviados por el servidor al cliente
            serverMessageThread = new Thread(readSocket)
            {
                IsBackground = true
            };

            isRunning = true; // Establecer la bandera de ejecución del hilo
            serverMessageThread.Start();
        }
        catch (SocketException ex)
        {
            throw new InvalidOperationException("No se pudo conectar al servidor.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Ocurrió un error al intentar conectar.", ex);
        }
    }

    // Procedimiento para cerrar la conexión con el servidor
    public void DisconnectSocket()
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            throw new InvalidOperationException("No hay una conexión activa para cerrar.");
        }

        try
        {
            // Establecer la bandera para detener la ejecución del hilo
            isRunning = false;

            // Desconectar del servidor
            tcpClient.Close();



        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Ocurrió un error al intentar desconectar.", ex);
        }
        finally
        {
            ConnectionTerminated?.Invoke(); // Generar evento de conexión terminada
        }
    }

    // Enviar mensaje al servidor
    public void SendData(string Datos)
    {
        if (string.IsNullOrWhiteSpace(Datos))
        {
            throw new ArgumentException("Los datos a enviar no pueden estar vacíos.");
        }

        byte[] BufferDeEscritura = Encoding.ASCII.GetBytes(Datos);

        if (messagesSendReceive != null)
        {
            try
            {
                messagesSendReceive.Write(BufferDeEscritura, 0, BufferDeEscritura.Length);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Error al enviar datos al servidor.", ex);
            }
        }
        else
        {
            throw new InvalidOperationException("No se puede enviar datos, la conexión no está establecida.");
        }
    }

    private void readSocket()
    {
        byte[] BufferDeLectura = new byte[1024];

        while (true)
        {
            try
            {
                // Esperar a que llegue algún mensaje
                int bytesLeidos = messagesSendReceive.Read(BufferDeLectura, 0, BufferDeLectura.Length);

                if (bytesLeidos > 0)
                {
                    // Generar evento DatosRecibidos cuando se reciban datos desde el servidor
                    DataReceived?.Invoke(Encoding.ASCII.GetString(BufferDeLectura, 0, bytesLeidos));
                    //DatosRecibidos = Encoding.UTF8.GetString(BufferDeLectura, 0, bytesLeidos);
                }
            }
            catch (IOException)
            {
                break; // Salir del bucle si hay una excepción de E/S
            }
            catch (ObjectDisposedException)
            {
                break; // Salir del bucle si el objeto está desechado
            }
            catch (Exception ex)
            {
                // Otras excepciones pueden ser manejadas aquí
                Console.WriteLine($"Error desconocido: {ex.Message}");
                break;
            }
        }

        // Finalizar conexión y generar evento ConexionTerminada
        ConnectionTerminated?.Invoke();
    }
}
