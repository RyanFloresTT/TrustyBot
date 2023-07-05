using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;
using TrustyBot.Modules.JokeCommand;
using TrustyBot.Modules.AdviceCommand;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    private static DiscordSocketClient client;
    private string token = Environment.GetEnvironmentVariable("BOT_TOKEN");
    private const ulong GUILD_ID = 1125167218125181071;
    private static List<SlashCommandBuilder> commands = new List<SlashCommandBuilder>();

    public static DiscordSocketClient Client { get => client; private set => client = value; }

    public async Task MainAsync()
    {
        Client = new DiscordSocketClient();

        Client.Log += Log;
        Client.Ready += Client_Ready;

        await Client.LoginAsync(TokenType.Bot, token);
        await Client.StartAsync();
        await Task.Delay(-1);
    }

    public async Task Client_Ready()
    {
        Console.WriteLine("Client Ready.");
        _ = new JokeCommand();
        _ = new AdviceCommand();

        var guild = Client.GetGuild(GUILD_ID);

        try
        {
            foreach (var command in commands)
            {
                await guild.CreateApplicationCommandAsync(command.Build());
            }
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Reason, Formatting.Indented);
            Console.WriteLine(json);
        }
    }

    public static void RegisterCommand(SlashCommandBuilder command)
    {
        commands.Add(command);
        Console.WriteLine(command.Name + " registered.");
    }

    public static void UnregisterCommand(SlashCommandBuilder command)
    {
        commands.Remove(command);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}