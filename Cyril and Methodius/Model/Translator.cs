using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyril_and_Methodius.Model
{
    class Translator
    {
        public static string Translate(string str, bool cyr, LatinEnglishCharacterChoice choice = LatinEnglishCharacterChoice.Include, string replacementSign = "|")
        {
            string[] lat_up = { "Dž", "DŽ", "Nj", "NJ", "Lj", "LJ", "А", "B", "V", "G", "D", "Đ", "E", "Ž", "Z", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "Ć", "U", "F", "H", "C", "Č", "Š" };
            string[] lat_low = { "dž", "dŽ", "nj", "nJ", "lj", "lJ", "а", "b", "v", "g", "d", "đ", "e", "ž", "z", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "ć", "u", "f", "h", "c", "č", "š" };
            string[] rs_up = { "Џ", "Џ", "Њ", "Њ", "Љ", "Љ", "А", "Б", "В", "Г", "Д", "Ђ", "Е", "Ж", "З", "И", "Ј", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "Ћ", "У", "Ф", "Х", "Ц", "Ч", "Ш" };
            string[] rs_low = { "џ", "џ", "њ", "њ", "љ", "љ", "а", "б", "в", "г", "д", "ђ", "е", "ж", "з", "и", "ј", "к", "л", "м", "н", "о", "п", "р", "с", "т", "ћ", "у", "ф", "х", "ц", "ч", "ш" };
            string[] latEngChars = { "X", "x", "Y", "y", "Q", "q", "W", "w" };
            if (cyr)
            {
                for (int i = 0; i < lat_up.Length; i++)
                {
                    if (!(i == 1 || i == 3 || i ==5))
                    {
                        str = str.Replace(rs_up[i], lat_up[i]);
                        str = str.Replace(rs_low[i], lat_low[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < lat_up.Length; i++)
                {
                    str = str.Replace(lat_up[i], rs_up[i]);
                    str = str.Replace(lat_low[i], rs_low[i]);
                }
                if (choice == LatinEnglishCharacterChoice.Delete)
                {
                    foreach (var character in latEngChars)
                    {
                        str = str.Replace(character, "");
                    }
                }
                else if (choice == LatinEnglishCharacterChoice.Mark)
                {
                    foreach (var character in latEngChars)
                    {
                        str = str.Replace(character, replacementSign);
                    }
                }
            }

            return str;
        }
        public enum LatinEnglishCharacterChoice
        {
            Include,
            Delete,
            Mark
        }
    }
}
