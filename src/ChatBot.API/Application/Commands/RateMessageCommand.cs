using ChatBot.Domain;
using FluentResults;
using MediatR;

namespace ChatBot.API.Application.Commands;

public record RateMessageCommand(Guid ConversationId, Guid MessageId, int Rating)
    : IRequest<Result<bool>>;

public class RateMessageCommandHandler : IRequestHandler<RateMessageCommand, Result<bool>>
{
    private readonly IConversationRepository _repository;

    public RateMessageCommandHandler(IConversationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(RateMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _repository.GetByIdAsync(request.ConversationId);

        if (conversation == null)
        {
            return Result.Fail("Conversation not found");
        }
        
        var message = conversation.Messages.FirstOrDefault(m => m.Id == request.MessageId);
        
        if (message is not null)
        {
            message.Rating = request.Rating;
        }
        
        var result = await _repository.SaveAsync(conversation);

        return Result.Ok(result);
    }
}