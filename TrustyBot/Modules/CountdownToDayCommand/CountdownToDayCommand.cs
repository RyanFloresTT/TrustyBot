using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustyBot.Modules.GameRouletteCommand;
using TrustyBot.Utils;

namespace TrustyBot.Modules.CountdownToDayCommand
{
    internal class CountdownToDayCommand
    {
        private const string COMMAND_NAME = "countdown";
        private const string COMMAND_DESC = "Outputs the remaining days until a specified date.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.json";

        public CountdownToDayCommand()
        {   
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("date", ApplicationCommandOptionType.String, "Enter the date you wish to countdown to.", true);

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
            var userInput = command.Data.Options.First().ToString();
            DateTime date;
            if (DateTime.TryParse("29/02/2021", out date))
            {
                await command.RespondAsync($"That is a date. {date}");
            }
            else
            {
                await command.RespondAsync($"That is NOT a date. {date}");
            }
        }
    }
}
