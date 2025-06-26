namespace ChatBot.Domain;

public class Conversation
{
    public Guid Id { get; set; }

    public string Title { get; private set; } = null!;

    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    public Conversation(string title)
    {
        Title = title;
    }

    public void AddMessage(string messageContent, MessageType messageType)
    {
        var message = new ChatMessage(this, messageContent, messageType);
        
        this.Messages.Add(message);
    }

    private Conversation()
    { }
}