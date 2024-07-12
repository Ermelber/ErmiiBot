using Discord;
using Discord.WebSocket;
using ErmiiSoft.ErmiiBot.Commands;

namespace ErmiiSoft.ErmiiBot;

class ClientHandler(DiscordSocketClient client, IEnumerable<IBotCommand> botCommands)
{
    private const string DISCORD_BOT_TOKEN_ENV_VAR_KEY = "DiscordBotToken";

    public async Task RunAsync()
    {
        if (!await ConfigureAsync())
            return;

        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }

    private async Task<bool> ConfigureAsync()
    {
        string? token = Environment.GetEnvironmentVariable(DISCORD_BOT_TOKEN_ENV_VAR_KEY);

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"Please provide a bot token inside the \"{DISCORD_BOT_TOKEN_ENV_VAR_KEY}\" environment variable.");
            return false;
        }

        client.Log += OnLogAsync;
        client.Ready += OnClientReadyAsync;
        client.SlashCommandExecuted += OnSlashCommandExecuted;

        await client.LoginAsync(TokenType.Bot, token);
        return true;
    }

    private async Task ConfigureCommandsAsync()
    {
        foreach (var guild in client.Guilds)
        {
            await guild.DeleteApplicationCommandsAsync();

            foreach (var command in botCommands)
            {
                var builder = command.SlashCommandBuilder;
                try
                {
                    await guild.CreateApplicationCommandAsync(builder.Build());
                    Console.WriteLine($"Registered '{builder.Name}' to '{guild.Name}'.");
                }
                catch
                {
                    Console.WriteLine($"Failed to register '{builder.Name}' to guildName '{guild.Name}'.");
                }
            }
        }
    }

    private async Task OnSlashCommandExecuted(SocketSlashCommand command)
    {
        var botCommand = botCommands.First(x => x.SlashCommandBuilder.Name == command.CommandName);
        Console.WriteLine($"Processing slash command '{command.CommandName}' from user '{command.User.Id}' in channel '{command.Channel.Id}'.");
        await botCommand.ExecuteAsync(command);
    }

    private async Task OnClientReadyAsync()
    {
        await ConfigureCommandsAsync();
    }

    private async Task OnLogAsync(LogMessage logMessage)
        => await WriteLogAsync(logMessage.Severity, logMessage.Message);

    private async Task WriteLogAsync(LogSeverity severity, string message)
    {
        await Task.CompletedTask;
        Console.WriteLine($"[{DateTime.Now}] {severity}: {message}");
    }
}
