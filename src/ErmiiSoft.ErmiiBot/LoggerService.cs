using Discord;

namespace ErmiiSoft.ErmiiBot;

class LoggerService
{
    public async Task WriteAsync(string message)
        => await WriteAsync(LogSeverity.Info, message);

    public async Task WriteAsync(LogSeverity severity, string message)
    {
        await Task.CompletedTask;
        Console.WriteLine($"[{DateTime.Now}] {severity}: {message}");
    }
}
