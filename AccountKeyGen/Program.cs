using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AccountKeyGen
{
    public static class KeyGen
    {
        private static Random RND = new Random();
        static void Main(string[] args)
        {
            Title = "Account Key Generator";
            WriteLine("This program will produce a key that is personal for each account.\nThe user will receive an email with this key and must enter it at first login.");
            for (int i = 0; i < 30; i++)
            {
                WriteLine("Key" + i + ": " + Key(25));
            }
            Read();
        }

        public static string Key(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RND.Next(s.Length)]).ToArray());
        }
    }
}
