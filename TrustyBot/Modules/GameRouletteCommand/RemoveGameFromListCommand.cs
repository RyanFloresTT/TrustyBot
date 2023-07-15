using Discord;
using Discord.WebSocket;
using System.Text;
using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class RemoveGameFromListCommand
    {
        private const string COMMAND_NAME = "remove-game";
        private const string COMMAND_DESC = "Remove a game from your list.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.json";

        public RemoveGameFromListCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("name", ApplicationCommandOptionType.String, "Enter the name of the game.", true);

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
            var gameData = command.Data.Options.ToArray();
            var gameName = gameData[0].Value.ToString();

            GameDataFileUtils.Remove(gameName, FILE_DIR);

            await command.RespondAsync($"{gameName} removed from the list!");
        }
    }
}
