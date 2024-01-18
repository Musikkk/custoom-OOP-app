using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppzo_opp_app.Models
{
    public class BookModel
    {
        public BookModel() { }

        public BookModel(int id, string title, string author, int releaseYear, string genre, bool isOnLoan)
        {
            Id = id;
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
            Genre = genre;
            IsOnLoan = isOnLoan;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int ReleaseYear { get; set; }

        public string Genre { get; set; }

        public bool IsOnLoan { get; set; }

        public override string ToString()
        {
            string isOnLoan = IsOnLoan ? "Tak" : "Nie";
            return $"{Id}. Tytuł: {Title}, Autor: {Author}, Rok Wydania: {ReleaseYear}, Gatunek: {Genre}, Wypożyczona: {isOnLoan}";
        }

        public string ToStringForUser()
        {
            return $"Tytuł: {Title}, Autor: {Author}, Rok Wydania: {ReleaseYear}, Gatunek: {Genre}";
        }
    }
}
