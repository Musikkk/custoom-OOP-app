using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppzo_opp_app.Helpers
{
    public static class ReadLineHelper 
    {
        public static string ReadPasswordAndDetectEsc()
        {
            ConsoleKeyInfo key;
            string input = "";

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }

            } while (true);

            return null;
        }

        public static string ReadLettersOrDigitsAndDetectEsc()
        {
            ConsoleKeyInfo key;
            string input = "";

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    Console.Write(key.KeyChar);
                    input += key.KeyChar;
                }


            } while (true);

            return null;
        }

        public static int ReadDigitsAndDetectEsc()
        {
            ConsoleKeyInfo key;
            string input = "";

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    return int.Parse(input);
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (char.IsDigit(key.KeyChar))
                {
                    Console.Write(key.KeyChar);
                    input += key.KeyChar;
                }

            } while (true);

            return 0;
        }
        public static bool EscDetection()
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return true;
                }

            } while (key.Key != ConsoleKey.Escape);

            return false;
        }
    }
    
}
