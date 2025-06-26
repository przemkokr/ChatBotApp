using ChatBot.API.Application.Responses;
using ChatBot.Domain;
using FluentResults;
using MediatR;

namespace ChatBot.API.Application.Commands;

public record PostMessageCommand : IRequest<Result<PostMessageResponse>>
{
    public Guid? ConversationId { get; init; }
    
    public string Message { get; init; }
    
    public bool IsPrompt { get; init; }
}

public class PostMessageCommandHandler : IRequestHandler<PostMessageCommand, Result<PostMessageResponse>>
{
    
    private readonly IConversationRepository _conversationRepository;
    private readonly IChatService _chatService;

    public PostMessageCommandHandler(IConversationRepository conversationRepository, IChatService chatService)
    {
        _conversationRepository = conversationRepository;
        _chatService = chatService;
    }
    
    public async Task<Result<PostMessageResponse>> Handle(PostMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId) 
                           ?? new Conversation(request.Message[..(request.Message.Length > 10 ? 10 : request.Message.Length)]);

        var messageType = request.IsPrompt ? MessageType.Prompt : MessageType.Response;
        conversation.AddMessage(request.Message, messageType);

        var saveResult = await _conversationRepository.SaveAsync(conversation);
        if (!saveResult)
        {
            return Result.Fail("Unable to save the message.");
        }

        string generatedResponse = string.Empty;

        if (request.IsPrompt)
        {
            generatedResponse = _chatService.GenerateResponse();
        }

        return Result.Ok(new PostMessageResponse
        {
            Response = generatedResponse,
            ConversationId = conversation.Id,
            MessageId = conversation.Messages.Last().Id
        });
    }
}