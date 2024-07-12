using Discord;
using Discord.WebSocket;

string? token = Environment.GetEnvironmentVariable("DiscordBotToken");

if (string.IsNullOrEmpty(token))
{
    Console.WriteLine("Please provide a bot token inside the \"DiscordBotToken\" environment variable.");
    return;
}

var client = new DiscordSocketClient();

client.Log += async (logMessage) =>
{
    Console.WriteLine($"[{DateTime.Now}] {logMessage.Severity}: {logMessage.Message}");
};

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();
await Task.Delay(Timeout.Infinite);