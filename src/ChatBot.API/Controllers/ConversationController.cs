using ChatBot.API.Application.Commands;
using ChatBot.API.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConversationController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostConversationAsync([FromBody] PostMessageCommand command)
    {
        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Reasons);
        }
        
        return Ok(result.Value);
    }

    [HttpPost]
    [Route("{conversationId:guid}/messages/{messageId:guid}/rate")]
    public async Task<IActionResult> RateMessage(Guid conversationId, Guid messageId, [FromBody] RateMessageRequest request)
    {
        var result = await mediator.Send(new RateMessageCommand(conversationId, messageId, request.Rating));

        if (result.IsFailed)
        {
            return BadRequest(result.Reasons);
        }

        return Ok(result.Value);
    }
}