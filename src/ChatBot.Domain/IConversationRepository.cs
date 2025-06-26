namespace ChatBot.Domain;

public interface IConversationRepository
{
    Task<Conversation?> GetByIdAsync(Guid? conversationId);
    
    Task<bool> SaveAsync(Conversation conversation);}