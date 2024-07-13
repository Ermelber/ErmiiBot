using Discord;

namespace ErmiiSoft.ErmiiBot;

class LoggerService
{
    public async Task WriteLogAsync(string message)
        => await WriteLogAsync(LogSeverity.Info, message);

    public async Task WriteLogAsync(LogSeverity severity, string message)
    {
        await Task.CompletedTask;
        Console.WriteLine($"[{DateTime.Now}] {severity}: {message}");
    }
}
