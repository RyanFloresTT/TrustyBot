using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    public record EventData(string Name, DateTime DateTime) : JSONData(Name);
}
