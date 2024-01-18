using Newtonsoft.Json;
using ppzo_opp_app.Helpers;
using ppzo_opp_app.Models;
using ppzo_opp_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace ppzo_opp_app.Interfaces
{
    public class UserInterface
    {
        public UserInterface(LoginInterface loginInterface)
        {
            LoginInterface = loginInterface;
        }

        private LoginInterface LoginInterface { get; set; }

        public void ShowLoginView()
        {
            Console.WriteLine("Logowanie\n");

            Console.WriteLine("Kliknij \"ESC\" aby wyjść.\n");

            Console.Write("Podaj login:\n");
            string login = ReadLineHelper.ReadLettersOrDigitsAndDetectEsc();

            if (login == null)
            {
                Console.Clear();
                LoginInterface.ShowLoginInterface();
            }

            Console.Write("Podaj hasło:\n");
            string password = ReadLineHelper.ReadPasswordAndDetectEsc();

            if (password == null)
            {
                Console.Clear();
                LoginInterface.ShowLoginInterface();
            }

            bool isUserAuthenticated = UserAuthenticationService.CheckUserAuthentication(login, password);

            if (isUserAuthenticated)
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(login + ".json"));
                LibraryInterface libraryInterFace = new LibraryInterface(user);
                Console.Clear();
                libraryInterFace.ShowMainLibraryView();
            }
            else
            {
                ShowLoginView();
            }
        }

        public void ShowRegisterView()
        {
            Console.WriteLine("Rejestracja\n");

            Console.WriteLine("Kliknij \"ESC\" aby wyjść.\n");

            Console.Write("Podaj login:\n");
            string login = ReadLineHelper.ReadLettersOrDigitsAndDetectEsc();

            if (login == null)
            {
                Console.Clear();
                LoginInterface.ShowLoginInterface();
            }

            string userString;

            try
            {
                userString = File.ReadAllText(login + ".json");
            }
            catch (Exception ex)
            {
                userString = null;
            }

            if (userString != null)
            {
                Console.Clear();
                Console.WriteLine("Użytkownik o takim loginie już istnieje. Podaj inny login.\n");
                ShowRegisterView();
            }

            Console.Write("Podaj hasło:\n");
            string password = ReadLineHelper.ReadPasswordAndDetectEsc();

            if (password == null)
            {
                Console.Clear();
                LoginInterface.ShowLoginInterface();
            }

            Console.Write("Powtórz hasło:\n");
            string repeatPassword = ReadLineHelper.ReadPasswordAndDetectEsc();

            if (repeatPassword == null)
            {
                Console.Clear();
                LoginInterface.ShowLoginInterface();
            }

            if (password.Equals(repeatPassword))
            {
                UserModel user = UserAuthenticationService.CreateNewUser(login, password);

                string json = JsonConvert.SerializeObject(user, Formatting.Indented);
                File.WriteAllText(login + ".json", json);

                Console.Clear();
                Console.WriteLine("Utworzono nowego użytkownika.\n");
                LoginInterface.ShowLoginInterface();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Hasła nie były takie same.\n");
                ShowRegisterView();
            }
        }
    }
}
