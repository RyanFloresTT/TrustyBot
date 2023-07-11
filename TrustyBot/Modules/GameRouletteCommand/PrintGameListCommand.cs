using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Xml.Schema;
using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class PrintGameListCommand
    {
        private const string COMMAND_NAME = "show-games";
        private const string COMMAND_DESC = "Show all games on the list.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.json";
        Random rand = new();

        public PrintGameListCommand()
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

        private async Task PerformCommand(SocketSlashCommand command)
        {
            var games = GameDataFileUtils.ReadFromJSON(FILE_DIR);
            List<Task> tasks = new();
                
            foreach (var game in games)
            {
                tasks.Add(PrintGameAsync(game, command));
            }
            await Task.WhenAll(tasks);
        }
        
        private async Task PrintGameAsync(GameData game, SocketSlashCommand command)
        {
            var embedBuiler = new EmbedBuilder()
                    .WithTitle(game.Name)
                    .WithDescription(game.Description)
                    .WithColor(Color.Default)
                    .WithCurrentTimestamp();

            await command.RespondAsync(embed: embedBuiler.Build());
        }
    }
}
