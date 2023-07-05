using Discord;
using Discord.WebSocket;
using System.Text;

namespace TrustyBot.Modules.GameRouletteCommand
{
    internal class AddGameToListCommand
    {
        private const string COMMAND_NAME = "add-game";
        private const string COMMAND_DESC = "Add a game to your list.";
        const Int32 BufferSize = 128;
        const string FILE_DIR = "D:\\Repos\\Discord Bots\\TrustyBot\\TrustyBot\\Modules\\GameRouletteCommand\\games.txt";

        public AddGameToListCommand()
        {
            Program.Client.SlashCommandExecuted += Handle_SlashCommandExecuted;

            SlashCommandBuilder command = new SlashCommandBuilder()
                .WithName(COMMAND_NAME)
                .WithDescription(COMMAND_DESC)
                .AddOption("name", ApplicationCommandOptionType.String, "Enter the name of the Game", true);

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
            var gameName = command.Data.Options.First().Value.ToString();

            using (FileStream fs = File.OpenWrite(FILE_DIR))
            {
                AddText(fs, $"{gameName}");
            }

            await command.RespondAsync($"{gameName} added to your list.");
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }


}
