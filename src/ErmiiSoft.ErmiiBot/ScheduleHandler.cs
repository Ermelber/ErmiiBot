using CronNET;
using Discord;
using Discord.WebSocket;
using ErmiiSoft.ErmiiBot.Models;
using Newtonsoft.Json;

namespace ErmiiSoft.ErmiiBot;

class ScheduleHandler(DiscordSocketClient client, LoggerService log, ICronDaemon daemon)
{
    private const string SCHEDULED_MESSAGES_JSON_PATH = "Assets/Json/ScheduledMessages.json";
    private const string SCHEDULED_MESSAGES_FILE_BASE_PATH = "Assets/Images/";

    public async Task ConfigureAsync()
    {
        await Task.CompletedTask;

        string json = File.ReadAllText(SCHEDULED_MESSAGES_JSON_PATH);
        var scheduledMessages = JsonConvert.DeserializeObject<IEnumerable<ScheduledMessage>>(json) ?? [];

        foreach (var scheduledMessage in scheduledMessages)
        {
            daemon.AddJob(scheduledMessage.CronExpression, async () =>
            {
                try
                {
                    await log.WriteAsync($"Executing Daemon Job '{scheduledMessage.Name}'.");
                    var channel = client.GetGuild(scheduledMessage.GuildId).GetTextChannel(scheduledMessage.ChannelId);

                    if (scheduledMessage.Files.Length > 0)
                    {
                        string randomFilePath = Path.Combine(SCHEDULED_MESSAGES_FILE_BASE_PATH, 
                            scheduledMessage.Files[Random.Shared.Next(scheduledMessage.Files.Length)]);

                        await channel.SendFileAsync(randomFilePath, text: scheduledMessage.Text);
                    }
                    else
                    {
                        await channel.SendMessageAsync(scheduledMessage.Text);
                    }
                }
                catch (Exception ex)
                {
                    await log.WriteAsync(LogSeverity.Error, $"Error executing Daemon Job '{scheduledMessage.Name}': {ex.Message}");
                }
            });
            await log.WriteAsync($"Registered Daemon Job '{scheduledMessage.Name}'.");
        }

        daemon.Start();
    }
}
