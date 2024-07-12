using Discord;
using Discord.WebSocket;

namespace ErmiiSoft.ErmiiBot;

class ClientHandler(DiscordSocketClient client)
{
    private const string DISCORD_BOT_TOKEN_ENV_VAR_KEY = "DiscordBotToken";

    public async Task ConfigureAsync()
    {
        string? token = Environment.GetEnvironmentVariable(DISCORD_BOT_TOKEN_ENV_VAR_KEY);

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"Please provide a bot token inside the \"{DISCORD_BOT_TOKEN_ENV_VAR_KEY}\" environment variable.");
            return;
        }

        client.Log += async (logMessage) =>
        {
            await Task.CompletedTask;
            Console.WriteLine($"[{DateTime.Now}] {logMessage.Severity}: {logMessage.Message}");
        };

        await client.LoginAsync(TokenType.Bot, token);
        return;
    }

    public async Task RunAsync()
    {
        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }
}
