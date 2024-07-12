using Discord;
using Discord.WebSocket;

namespace ErmiiSoft.ErmiiBot.Commands;

public class TestCommand : IBotCommand
{
    public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
        .WithName("test")
        .WithDescription("Test command for ErmiiBot");

    public async Task ExecuteAsync(SocketSlashCommand command)
        => await command.RespondAsync(text: "This is a test!");
}
