using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var source = new CancellationTokenSource();

        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true)
        .Build();

        var client = new DiscordClient(
            new DiscordConfiguration { Token = config["discordToken"], TokenType = TokenType.Bot }
        );

        client.MessageCreated += async (DiscordClient sender, MessageCreateEventArgs e) =>
        {
            if (e.Message.Content.StartsWith("ping"))
            {
                await e.Message.RespondAsync("pong"); // UPDATED: Changed SendMessageAsync to RespondAsync
            }
        };

        var token = source.Token;
        // await client.ConnectAsync();

        while (!token.IsCancellationRequested)
        {
            await Task.Delay(100);
        }     // UPDATED: Changed ConnectAsync to DisconnectAsync
    }
}