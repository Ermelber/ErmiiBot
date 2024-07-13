using Discord;
using Discord.WebSocket;
using ErmiiSoft.ErmiiBot.Commands;

namespace ErmiiSoft.ErmiiBot;

class CommandHandler(DiscordSocketClient client, LoggerService log, IEnumerable<BotCommand> botCommands)
{
    public async Task ConfigureAsync()
    {
        client.SlashCommandExecuted += OnSlashCommandExecuted;

        foreach (var guild in client.Guilds)
        {
            await guild.DeleteApplicationCommandsAsync();

            foreach (var command in botCommands)
            {
                var builder = command.SlashCommandBuilder;
                try
                {
                    await guild.CreateApplicationCommandAsync(builder.Build());
                    await log.WriteAsync($"Registered '{builder.Name}' to '{guild.Name}'.");
                }
                catch
                {
                    await log.WriteAsync($"Failed to register '{builder.Name}' to guildName '{guild.Name}'.");
                }
            }
        }
    }

    private async Task OnSlashCommandExecuted(SocketSlashCommand command)
    {
        try
        {
            await log.WriteAsync($"Processing slash command '{command.CommandName}' from user '{command.User.Id}' in channel '{command.Channel.Id}'.");
            var botCommand = botCommands.First(x => x.SlashCommandBuilder.Name == command.CommandName);
            await botCommand.ExecuteAsync(command);
        }
        catch (Exception ex)
        {
            await log.WriteAsync(LogSeverity.Error, $"Command '{command.CommandName}': {ex.Message}");
        }
    }
}
