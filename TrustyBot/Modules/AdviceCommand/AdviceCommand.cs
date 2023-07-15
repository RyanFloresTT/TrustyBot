using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
namespace TrustyBot.Modules.AdviceCommand
{
    internal class AdviceCommand
    {
        private const string COMMAND_NAME = "give-advice";
        private const string COMMAND_DESC = "Let the bot give you some advice.";
        private const string API_ENDPOINT = "https://api.adviceslip.com/advice";

        public AdviceCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC);

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

        private async Task<AdviceData> GetNewAdviceAsync()
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(API_ENDPOINT);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var advice = JsonConvert.DeserializeObject<AdviceData>(jsonResponse);
                return advice;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private async Task PerformCommand(SocketSlashCommand command)
        {
            var advice = GetNewAdviceAsync().Result;
            await command.RespondAsync(advice.Slip.Advice);
        }
    }
}
