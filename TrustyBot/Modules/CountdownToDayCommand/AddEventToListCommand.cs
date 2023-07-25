using Discord;
using Discord.WebSocket;
using System.Text;
using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class AddEventToListCommand
    {
        private const string COMMAND_NAME = "add-event";
        private const string COMMAND_DESC = "Add an event to your list.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\CountdownToDayCommand\\events.json";

        public AddEventToListCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("name", ApplicationCommandOptionType.String, "Enter the name of the event.", true)
                .AddOption("date", ApplicationCommandOptionType.String, "Enter a date for the event.", true);

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

        private async Task PerformCommand(SocketSlashCommand command)
        {
            var eventData = command.Data.Options.ToArray();
            var eventName = eventData[0].Value.ToString();
            var eventDate = eventData[1].Value.ToString();
            DateTime date;
            if (DateTime.TryParse(eventDate, out date) && date > DateTime.Now)
            {
                var @event = new EventData(eventName, date);
                @event.AddToJSON(FILE_DIR);
                await command.RespondAsync($"{eventName} - {date} : added to the list.");
            }
            else
            {
                await command.RespondAsync($"Sorry, but \"{date}\" is either not a date, or earlier than today.");
            }
        }
    }
}
