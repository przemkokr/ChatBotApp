using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure;

public class ConversationRepository : IConversationRepository
{
    private readonly ChatBotDbContext _dbContext;

    public ConversationRepository(ChatBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Conversation?> GetByIdAsync(Guid? conversationId)
    {
        if (conversationId == null)
        {
            return null;
        }

        return await _dbContext.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public async Task<bool> SaveAsync(Conversation conversation)
    {
        if (_dbContext.Conversations.Any(c => c.Id == conversation.Id))
        {
            _dbContext.Conversations.Update(conversation);
        }
        else
        {
            await _dbContext.Conversations.AddAsync(conversation);
        }

        return await _dbContext.SaveChangesAsync() > 0;
    }
}