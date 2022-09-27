using System;

namespace ClassLibraryLibrary.Enums
{
    [Flags]
    public enum JournalGenre
    {
        News = 1,
        Cars = 2,
        Culture = 4,
        Celebrity = 8,
        Citizen = 16,
        History = 64,
    }
}
