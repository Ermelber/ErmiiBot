using Discord;
using Discord.WebSocket;

namespace ErmiiSoft.ErmiiBot.Commands;

public sealed class TestCommand() : BotCommand("test", "Test command for ErmiiBot")
{
    public override async Task ExecuteAsync(SocketSlashCommand command)
        => await command.RespondAsync(text: "This is a test!");
}
