using Discord;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

class Joke
{
    public static async Task<JokeData> GetNewJokeAsync(string user)
    {
        Program.Client.Ready += Handle_ClientReady;

        string URL = $"https://api.chucknorris.io/jokes/random?name={user}&category=dev";
        HttpClient client = new HttpClient();

        try
        {
            var response = await client.GetAsync(URL);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var joke = JsonConvert.DeserializeObject<JokeData>(jsonResponse);
            return joke;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    private static async Task Handle_ClientReady()
    {
        SlashCommandBuilder tellJokeCommand = new SlashCommandBuilder()
            .WithName("tell-joke")
            .WithDescription("Tells a joke using a user's name.")
            .AddOption("user", ApplicationCommandOptionType.User, "The user you want to tell a joke about", isRequired: true);

        await Program.AddToCommandList(tellJokeCommand);
    }
}