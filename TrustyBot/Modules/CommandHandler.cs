using Discord;
using Discord.Commands;
using Discord.WebSocket;

public class CommandHandler : ModuleBase<SocketCommandContext>
{
    public static async Task HandleCommand(SocketSlashCommand command)
    {
        switch (command.Data.Name)
        {
            case "list-roles":
                await HandleListRoleCommand(command);
                break;
            case "tell-joke":
                await HandleTellJokeCommand(command);
                break;
        }
    }

    private static async Task HandleListRoleCommand(SocketSlashCommand command)
    {
        // We need to extract the user parameter from the command. since we only have one option and it's required, we can just use the first option.
        var guildUser = (SocketGuildUser)command.Data.Options.First().Value;

        // We remove the everyone role and select the mention of each role.
        var roleList = string.Join(",\n", guildUser.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));

        var embedBuiler = new EmbedBuilder()
            .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            .WithTitle("Roles")
            .WithDescription(roleList)
            .WithColor(Color.Green)
            .WithCurrentTimestamp();

        // Now, Let's respond with the embed.
        await command.RespondAsync(embed: embedBuiler.Build());
    }

    private static async Task HandleTellJokeCommand(SocketSlashCommand command)
    {
        var guildUser = (SocketGuildUser)command.Data.Options.First().Value;
        var joke = Joke.GetNewJokeAsync(guildUser.Mention).Result;
        await command.RespondAsync(joke.Value);
    }
}