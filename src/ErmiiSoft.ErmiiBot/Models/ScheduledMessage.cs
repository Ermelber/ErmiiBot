namespace ErmiiSoft.ErmiiBot.Models;

record ScheduledMessage(string Name, string CronExpression, ulong GuildId, ulong ChannelId, string Text, string[] Files, string Notes);