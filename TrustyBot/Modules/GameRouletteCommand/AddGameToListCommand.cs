using Discord;
using Discord.WebSocket;
using System.Text;
using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class AddGameToListCommand
    {
        private const string COMMAND_NAME = "add-game";
        private const string COMMAND_DESC = "Add a game to your list.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.json";
            
        public AddGameToListCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("name", ApplicationCommandOptionType.String, "Enter the name of the game.", true)
                .AddOption("desc", ApplicationCommandOptionType.String, "Enter a Description for the game.", true);

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
            var gameDesc = gameData[1].Value.ToString();
            var game = new GameData(gameName, gameDesc);
            Console.WriteLine($"{game}");

            GameDataFileUtils.Add(game, FILE_DIR);

            await command.RespondAsync($"{gameName} added to the list!");
        }
    }
}
