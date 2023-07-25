﻿using TrustyBot.Utils;

namespace TrustyBot.Modules.GameRouletteCommand
{
    public record GameData(string Name, string Description) : JSONData(Name);
}
