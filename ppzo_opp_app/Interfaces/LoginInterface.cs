using ppzo_opp_app.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppzo_opp_app.Interfaces
{
    public class LoginInterface
    {
        public void ShowLoginInterface()
        {
            UserInterface userInterface = new UserInterface(this);

            Console.WriteLine("Witaj w wypożyczalni książek!\n");
            Console.WriteLine("Zaloguj się lub zarejestruj nowego użytkownika.\n");

            Console.WriteLine("1. Zaloguj się");
            Console.WriteLine("2. Zarejestruj się");
            Console.WriteLine("Kliknij \"ESC\" aby wyjść z aplikacji.");

            Console.Write("Wybierz opcję:\n");
            string option = ReadLineHelper.ReadLettersOrDigitsAndDetectEsc();

            if (option == null) { return; }

            switch (option)
            {
                case "1":
                    Console.Clear();
                    userInterface.ShowLoginView();
                    break;
                case "2":
                    Console.Clear();
                    userInterface.ShowRegisterView();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.\n");

                    ShowLoginInterface();
                    break;
            }
        }
    }

}
