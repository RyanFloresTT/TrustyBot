using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;
using Discord.Commands;
using System.Threading.Tasks;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    private static DiscordSocketClient client;
    private string token = Environment.GetEnvironmentVariable("BOT_TOKEN");
    private const ulong GUILD_ID = 1125167218125181071;
    private static List<SlashCommandBuilder> commands = new List<SlashCommandBuilder>();

    public static DiscordSocketClient Client { get => client; set => client = value; }

    public async Task MainAsync()
    {
        Client = new DiscordSocketClient();

        Client.Log += Log;
        Client.Ready += Client_Ready;
        Client.SlashCommandExecuted += SlashCommandHandler;

        await Client.LoginAsync(TokenType.Bot, token);
        await Client.StartAsync();
        await Task.Delay(-1);
    }

    public async Task Client_Ready()
    {
        var guild = Client.GetGuild(GUILD_ID);

        var listRolesCommand = new SlashCommandBuilder()
            .WithName("list-roles")
            .WithDescription("This is my first guild slash command!")
            .AddOption("user", ApplicationCommandOptionType.User, "The users whos roles you want to be listed", isRequired: true);

        commands.Add(listRolesCommand);

        try
        {
            // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
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

    public static async Task AddToCommandList(SlashCommandBuilder command)
    {
        Task taskA = Task.Run(() => commands.Add(command));
        await taskA;
    }

    public static async Task RemoveFromCommandList(SlashCommandBuilder command)
    {
        Task taskA = Task.Run(() => commands.Remove(command));
        await taskA;
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        await CommandHandler.HandleCommand(command);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}