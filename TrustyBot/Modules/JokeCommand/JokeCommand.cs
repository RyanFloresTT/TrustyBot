using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
namespace TrustyBot.Modules.JokeCommand
{
    internal class JokeCommand
    {
        private const string COMMAND_NAME = "tell-joke";
        private const string COMMAND_DESC = "Tells a joke using a user's name.";

        public JokeCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            
            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("user", ApplicationCommandOptionType.User, "The user you want to tell a joke about", isRequired: true);

            Program.RegisterCommand(command);
        }

        private async Task Handle_SlashCommandExecuted(SocketSlashCommand command)
        {
            switch (command.Data.Name)
            {
                case COMMAND_NAME:
                    await PerformCommand(command);
                    break;
            }
        }

        private async Task<JokeData> GetNewJokeAsync(string user)
        {
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

        private async Task PerformCommand(SocketSlashCommand command)
        {
            var guildUser = (SocketGuildUser)command.Data.Options.First().Value;
            var joke = GetNewJokeAsync(guildUser.Mention).Result;
            await command.RespondAsync(joke.Value);
        }
    }
}
