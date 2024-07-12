using Discord;
using Discord.WebSocket;
using Humanizer;

namespace ErmiiSoft.ErmiiBot.Commands;

public sealed class PizzaCommand() : BotCommand("pizza", "Witness Italy's best thing (probably)")
{
    public override async Task ExecuteAsync(SocketSlashCommand command)
    {
        var imageFiles = Directory.EnumerateFiles("Assets/Images/Pizza");
        string imagePath = imageFiles.ElementAt(Random.Shared.Next(imageFiles.Count()));
        await command.RespondWithFileAsync(imagePath, text: "Te piace accussì?");
    }
}
