using ClassLibraryLibrary.Enums;
using System;

namespace ClassLibraryLibrary.Models
{
    public class Book : AbstractItem
    {
        public string Author { get; internal set; }
        public string Publisher { get; internal set; }
        public BookGenre Genre { get; internal set; }
        public int RelaseYear { get; internal set; }
        public string Edition { get; internal set; }

        public Book(string name, string iSBN, double price,  uint quantity,
            string author, string publisher, BookGenre genre, int relaseYear, string edition)
            : base(name, iSBN, price, quantity)
        {
            Validate(author, publisher, genre, relaseYear, edition);
            Author = author;
            Publisher = publisher;
            Genre = genre;
            RelaseYear = relaseYear;
            Edition = edition;
        }

        private void Validate(string author, string publisher, BookGenre genre, int relaseDate, string edition)
        {
            if (string.IsNullOrEmpty(author)) throw new ArgumentException("Author cannot be null or empty");
            if (string.IsNullOrEmpty(publisher)) throw new ArgumentException("Publisher cannot be null or empty");
            if (genre == 0) throw new ArgumentException("Genre cannot be empty");
            if (relaseDate > DateTime.Now.Year) throw new ArgumentException("Relase year cannot be later than today");
            if (string.IsNullOrEmpty(edition)) throw new ArgumentException("Edition cannot be null or empty");
        }
    }
}
