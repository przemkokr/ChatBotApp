using System.Text;
using ChatBot.Domain;

namespace ChatBot.Infrastructure;

public class ChatService: IChatService
{
    /*
     * This where the real implemention should go
     * Should be async probably ;)
     */
    
    private static readonly Random Random = new();

    public string GenerateResponse()
    {
        const string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
        
        var length = Random.Next(50, 1001);
        var responseBuilder = new StringBuilder();
        var currentLength = 0;

        while (currentLength < length)
        {
            var paragraphLength = Random.Next(20, 200);
            var paragraph = loremIpsum[..Math.Min(paragraphLength, loremIpsum.Length)];

            responseBuilder.AppendLine(paragraph);
            responseBuilder.AppendLine();
            currentLength += paragraph.Length;
        }

        return responseBuilder.ToString().Trim();
    }
}