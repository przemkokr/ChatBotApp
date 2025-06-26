namespace ChatBot.Domain;

public class ChatMessage
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateCreated { get; private set; }

    public MessageType MessageType { get; private set; }

    public int Rating { get; set; } = 0;
    
    public Conversation Conversation { get; private set; }

    public ChatMessage(Conversation conversation, string content, MessageType messageType)
        : this()
    {
        Conversation = conversation;
        Content = content;
        MessageType = messageType;
    }

    private ChatMessage()
    {
        DateCreated = DateTime.UtcNow;
    }
}