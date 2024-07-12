using Discord;
using Discord.WebSocket;

namespace ErmiiSoft.ErmiiBot.Commands;

public abstract class BotCommand(string name, string description)
{
    public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
        .WithName(name)
        .WithDescription(description);

    public abstract Task ExecuteAsync(SocketSlashCommand command);
}
