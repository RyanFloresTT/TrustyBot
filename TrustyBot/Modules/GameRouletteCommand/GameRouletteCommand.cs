using Discord;
using Discord.WebSocket;
using System.Text;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class GameRouletteCommand
    {
        private const string COMMAND_NAME = "game-roulette";
        private const string COMMAND_DESC = "Spin the wheel to get a random game from your list.";
        const Int32 BufferSize = 128;
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.txt";

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
            List<string> games = new();
            var rand = new Random();

            using (var fileStream = File.OpenRead(FILE_DIR))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    games.Add(line);
                }
            }

            var game = games[rand.Next(games.Count)];

            await command.RespondAsync($"Guess what? It's time to play some {game}!");
        }
    }
}
