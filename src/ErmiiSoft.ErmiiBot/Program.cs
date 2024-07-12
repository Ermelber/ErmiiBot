using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ErmiiSoft.ErmiiBot;

class Program
{
    private static async Task Main(string[] args)
    {
        var serviceProvider = CreateServiceProvider();

        var clientHandler = serviceProvider.GetService<ClientHandler>();

        if (clientHandler is null)
            return;

        await clientHandler.ConfigureAsync();
        await clientHandler.RunAsync();
    }

    private static IServiceProvider CreateServiceProvider()
    {
        var config = new DiscordSocketConfig();
        var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<ClientHandler>();

        return collection.BuildServiceProvider();
    }
}