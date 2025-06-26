namespace ChatBot.API.Application.Responses;

public class PostMessageResponse
{
    public string Response { get; set; }

    public Guid ConversationId { get; set; }
    
    public Guid MessageId { get; set; }
}