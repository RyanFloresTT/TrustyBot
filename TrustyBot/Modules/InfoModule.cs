using Discord.Commands;
using Discord.WebSocket;

public class InfoModule : ModuleBase<SocketCommandContext>
{
    [Command("say")]
    [Summary("Echoes a message.")]
    public Task SayAsync([Remainder][Summary("The text to echo")] string echo) => ReplyAsync(echo);
}