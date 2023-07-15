using Discord;
using Discord.WebSocket;
using System.Text;
using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    public class GameRouletteCommand
    {
        private const string COMMAND_NAME = "game-roulette";
        private const string COMMAND_DESC = "Spin the wheel to get a random game from your list.";
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.json";
        Random rand = new();

        public GameRouletteCommand()
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
            var game = GetRandomFromList(games);

            var embedBuiler = new EmbedBuilder()
                .WithTitle(game.Name)
                .WithDescription(game.Description)
                .WithColor(Color.DarkBlue)
                .WithCurrentTimestamp();

            await command.RespondAsync(embed: embedBuiler.Build());
        }

        private GameData GetRandomFromList(List<GameData> list)
        {
            return list[rand.Next(list.Count)];
        }
    }
}
