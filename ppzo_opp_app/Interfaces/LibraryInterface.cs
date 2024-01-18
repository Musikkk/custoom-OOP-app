using Newtonsoft.Json;
using ppzo_opp_app.Helpers;
using ppzo_opp_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace ppzo_opp_app.Interfaces
{
    public class LibraryInterface
    {
        public LibraryInterface(UserModel user)
        {
            User = user;
        }
        private List<BookModel> AllBooks => JsonConvert.DeserializeObject<List<BookModel>>(File.ReadAllText(@"..\\..\\..\\Books\\books.json"));

        private List<BookModel> UsersBooks => AllBooks.Where(x => User.BookIds.Contains(x.Id)).ToList();

        private UserModel User { get; }

        public void ShowMainLibraryView()
        {
            Console.WriteLine("Użytkownik: " + User.Login + "\n");


            if (User.BookIds.Count == 0)
            {
                Console.WriteLine("Nie masz jeszcze wypożyczonych żadnych książek.");
            }
            else
            {
                Console.WriteLine("Twoje wypożyczone książki to:");

                foreach (BookModel book in UsersBooks)
                {
                    Console.WriteLine(book.ToStringForUser());
                }
            }

            ShowLibraryMenu();
        }

        private void ShowLibraryMenu()
        {
            Console.WriteLine("\n1. Lista wszystkich książek");
            Console.WriteLine("2. Wypożycz książkę");
            Console.WriteLine("3. Oddaj książkę");
            Console.WriteLine("4. Wyloguj");
            Console.WriteLine("Kliknij \"ESC\" aby wyjść z aplikacji.");

            Console.Write("\nWybierz opcję:\n");
            string option = ReadLineHelper.ReadLettersOrDigitsAndDetectEsc();

            if (option == null) { return; }

            switch (option)
            {
                case "1":
                    Console.Clear();
                    ShowListOfAllBooksView();
                    break;
                case "2":
                    Console.Clear();
                    ShowLoanBookView();
                    break;
                case "3":
                    Console.Clear();
                    ShowReturnBookView();
                    break;
                case "4":
                    Console.Clear();
                    LoginInterface loginInterface = new LoginInterface();
                    loginInterface.ShowLoginInterface();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.\n");

                    ShowMainLibraryView();
                    break;
            }
        }

        private void ShowListOfAllBooksView()
        {
            Console.Clear();
            Console.WriteLine("Użytkownik: " + User.Login + "\n");
            Console.WriteLine("Lista wszystkich książek:");
            PrintAllBooks();
            ShowReturnView();
        }

        private void ShowLoanBookView()
        {
            Console.Clear();
            Console.WriteLine("Użytkownik: " + User.Login + "\n");
            PrintAllBooks();
            LoanBook();
        }
        private void ShowReturnBookView()
        {
            Console.Clear();
            Console.WriteLine("Użytkownik: " + User.Login + "\n");
            PrintUsersBooks();
            ReturnBook();
        }

        private void LoanBook()
        {
            Console.WriteLine("\nWpisz numer książki, którą chcesz wypożyczyć lub naciśnij \"ESC\", aby wyjść.");
            int bookId = GetBookId(false);

            List<BookModel> updatedBooks = AllBooks;
            updatedBooks.Where(x => x.Id == bookId).Single().IsOnLoan = true;
            User.BookIds.Add(bookId);

            UpdateJsons(User, updatedBooks);

            Console.WriteLine($"\nKsiążka o numerze {bookId} została wypożyczona.");
            ShowReturnView();
        }

        public void ReturnBook()
        {
            if (UsersBooks.Count == 0)
            {
                Console.WriteLine("Nie masz jeszcze wypożyczonych żadnych książek.\n");
                Console.WriteLine("Naciśnij \"ESC\", aby wyjść.");
                if (ReadLineHelper.EscDetection())
                {
                    Console.Clear();
                    ShowLibraryMenu();
                }
            }

            Console.WriteLine("\nWpisz numer książki, którą chcesz oddać lub naciśnij \"ESC\", aby wyjść.");
            int bookId = GetBookId(true);

            List<BookModel> updatedBooks = AllBooks;
            updatedBooks.Where(x => x.Id == bookId).Single().IsOnLoan = false;
            User.BookIds.Remove(bookId);

            UpdateJsons(User, updatedBooks);

            Console.WriteLine($"\nKsiążka o numerze {bookId} została oddana.");
            ShowReturnView();
        }

        private int GetBookId(bool isReturn)
        {
            int bookNumber = ReadLineHelper.ReadDigitsAndDetectEsc();

            if (bookNumber == 0)
            {
                Console.Clear();
                ShowLibraryMenu();
            }

            if (!AllBooks.Any(x => x.Id == bookNumber))
            {
                Console.WriteLine("\nKsiążka o takim numerze nie istnieje.");
                return GetBookId(isReturn);
            }

            if (!isReturn && AllBooks.Any(x => x.Id == bookNumber && x.IsOnLoan))
            {
                Console.WriteLine("\nTa książka jest już wypożyczona.");
                return GetBookId(isReturn);
            }

            return bookNumber;
        }

        private void PrintAllBooks()
        {
            foreach (BookModel book in AllBooks)
            {
                Console.WriteLine(book.ToString());
            }
        }

        private void PrintUsersBooks()
        {
            foreach (BookModel book in UsersBooks)
            {
                Console.WriteLine(book.ToString());
            }
        }

        private void ShowReturnView()
        {
            Console.WriteLine("\nKliknij \"ESC\" aby wyjść");

            if (ReadLineHelper.EscDetection())
            {
                Console.Clear();
                ShowMainLibraryView();
            }
        }

        private void UpdateJsons(UserModel user, List<BookModel> books)
        {
            string booksUpdate = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText("books.json", booksUpdate);

            string userUpdate = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(user.Login + ".json", userUpdate);
        }
    }
}
