using Discord;
using Discord.WebSocket;
using ErmiiSoft.ErmiiBot.Commands;

namespace ErmiiSoft.ErmiiBot;

class ClientHandler(DiscordSocketClient client, LoggerService log, CommandHandler commands, ScheduleHandler schedule)
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

        await client.LoginAsync(TokenType.Bot, token);
        return true;
    }

    private async Task OnClientReadyAsync()
    {
        await commands.ConfigureAsync();
        await schedule.ConfigureAsync();
    }

    private async Task OnLogAsync(LogMessage logMessage)
        => await log.WriteAsync(logMessage.Severity, logMessage.Message);
}
