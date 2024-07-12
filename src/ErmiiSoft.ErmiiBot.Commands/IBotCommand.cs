using Discord;
using Discord.WebSocket;

namespace ErmiiSoft.ErmiiBot.Commands;

public interface IBotCommand
{
    SlashCommandBuilder SlashCommandBuilder { get; }
    Task ExecuteAsync(SocketSlashCommand command);
}
