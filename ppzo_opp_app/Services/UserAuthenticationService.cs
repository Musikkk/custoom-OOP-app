using Newtonsoft.Json;
using ppzo_opp_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ppzo_opp_app.Services
{
    public static class UserAuthenticationService
    {
        public static bool CheckUserAuthentication(string login, string password)
        {
            string jsonPath = login + ".json";

            if (File.Exists(jsonPath))
            {
                string userString = File.ReadAllText(jsonPath);
                UserModel loadedUser = JsonConvert.DeserializeObject<UserModel>(userString);

                string userPassword = EncryptPasswordAsString(password);

                if (userPassword.Equals(loadedUser.Password))
                {
                    Console.Clear();
                    Console.WriteLine("Logowanie powiodło się.\n");
                    return true;
                }
            }

            Console.Clear();
            Console.WriteLine("Użytkownik nie istnieje lub hasło jest nieprawidłowe.\n");

            return false;
        }

        public static UserModel CreateNewUser(string login, string password)
        {
            string hashedPassword = EncryptPasswordAsString(password);

            UserModel user = new UserModel(login, hashedPassword);

            return user;
        }

        private static string EncryptPasswordAsString(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bites = Encoding.UTF8.GetBytes(password);
                byte[] hashedBites = sha256Hash.ComputeHash(bites);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashedBites.Length; i++)
                {
                    stringBuilder.Append(hashedBites[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
