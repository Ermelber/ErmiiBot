using Discord.WebSocket;
using ErmiiSoft.ErmiiBot.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ErmiiSoft.ErmiiBot;

static class Program
{
    private static async Task Main(string[] args)
    {
        var serviceProvider = CreateServiceProvider();

        var clientHandler = serviceProvider.GetService<ClientHandler>();

        if (clientHandler is null)
            return;

        await clientHandler.RunAsync();
    }

    private static IServiceProvider CreateServiceProvider()
    {
        var config = new DiscordSocketConfig();

        var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<ClientHandler>()
            .AddCommands();

        return collection.BuildServiceProvider();
    }

    private static IServiceCollection AddCommands(this IServiceCollection collection) => collection
        .AddSingleton<BotCommand, UptimeCommand>()
        .AddSingleton<BotCommand, PizzaCommand>();
}