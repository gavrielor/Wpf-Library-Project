using ClassLibraryLibrary.Enums;
using System;

namespace ClassLibraryLibrary.Models
{
    public class Journal : AbstractItem
    {
        public string Publisher { get; internal set; }
        public JournalGenre Genre { get; internal set; }
        public Frequency Frequency { get; internal set; }

        public Journal(string name, string iSBN, double price, uint quantity,
            string publisher, JournalGenre genre, Frequency frequency)
            : base(name, iSBN, price, quantity)
        {
            Validate(publisher, genre);
            Publisher = publisher;
            Genre = genre;
            Frequency = frequency;
        }

        private void Validate(string publisher, JournalGenre genre)
        {
            if (string.IsNullOrEmpty(publisher)) throw new ArgumentException("Publisher cannot be null or empty");
            if (genre == 0) throw new ArgumentException("Genre cannot be empty");
        }
    }
}
