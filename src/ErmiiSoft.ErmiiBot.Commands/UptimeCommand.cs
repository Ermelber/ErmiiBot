using Discord;
using Discord.WebSocket;
using Humanizer;

namespace ErmiiSoft.ErmiiBot.Commands;

public sealed class UptimeCommand() : BotCommand("uptime", "Returns the current server time and time elapsed since startup")
{
    public override async Task ExecuteAsync(SocketSlashCommand command)
    {
        var embed = new EmbedBuilder
        {
            Title = "Server uptime",
            Color = Color.Green
        };

        string currentTime = $"{DateTime.UtcNow.ToString("HH:mm:ss")} (UTC)";
        string uptime = TimeSpan.FromMilliseconds(Environment.TickCount).Humanize();

        embed.AddField("Current Time", currentTime);
        embed.AddField("Uptime", uptime);

        await command.RespondAsync(embed: embed.Build());
    }
}
