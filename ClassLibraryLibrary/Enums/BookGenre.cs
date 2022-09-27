using System;

namespace ClassLibraryLibrary.Enums
{
    [Flags]
    public enum BookGenre
    {
        Adventure = 1,
        Autobiographies = 2,
        Comic = 4,
        Cookbook = 8,
        Detective = 16,
        Fantasy = 32,
        History = 64,
        Horror = 128,
        Mystery = 256,
        Poetry = 512,
        Romance = 1024,
    }
}
