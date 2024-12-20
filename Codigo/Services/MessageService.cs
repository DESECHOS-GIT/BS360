namespace BS360.Services;

public class MessageService
{
    private static readonly Lazy<MessageService> instance = new Lazy<MessageService>(() => new MessageService());
    public static MessageService Instance => instance.Value;
    public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();
    private MessageService() { }

    public void AddMessage(string msg, TerminalStates estado)
    {
        string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        Messages.Add(new Message { Text = $"{fechaActual}: {msg}", State = estado });
    }
}

public class Message
{
    public string Text { get; set; }
    public TerminalStates State { get; set; }
}
