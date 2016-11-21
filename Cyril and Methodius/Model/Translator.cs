using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyril_and_Methodius.Model
{
    public class Translator
    {
        /// <summary>
        /// This is the conversion method used to switch between Serbian/Montenegrin Cyrillic and Latin text. Please handle text formating before passing to this method.
        /// </summary>
        /// <param name="inputText">Input text, to be converted</param>
        /// <param name="cyrillic">Indicator if input text is cyrillic or latin (True -> Cyrillic)</param>
        /// <param name="choice">Option for handling X,Y,W,Q which are not part of Serbian language. They can be left included, marked (e.g. |Y|) or removed from string.</param>
        /// <param name="replacementSign">In case of choice = LatinEnglishCharacter.Mark this will be the sign to surround the characters with.</param>
        /// <returns></returns>
        public string Translate(string inputText, bool cyrillic, LatinEnglishCharacter choice = LatinEnglishCharacter.Include, string replacementSign = "|")
        {
            string[] lat_up = { "Dž", "DŽ", "Nj", "NJ", "Lj", "LJ", "А", "B", "V", "G", "D", "Đ", "E", "Ž", "Z", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "Ć", "U", "F", "H", "C", "Č", "Š" };
            string[] lat_low = { "dž", "dŽ", "nj", "nJ", "lj", "lJ", "а", "b", "v", "g", "d", "đ", "e", "ž", "z", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "ć", "u", "f", "h", "c", "č", "š" };
            string[] rs_up = { "Џ", "Џ", "Њ", "Њ", "Љ", "Љ", "А", "Б", "В", "Г", "Д", "Ђ", "Е", "Ж", "З", "И", "Ј", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "Ћ", "У", "Ф", "Х", "Ц", "Ч", "Ш" };
            string[] rs_low = { "џ", "џ", "њ", "њ", "љ", "љ", "а", "б", "в", "г", "д", "ђ", "е", "ж", "з", "и", "ј", "к", "л", "м", "н", "о", "п", "р", "с", "т", "ћ", "у", "ф", "х", "ц", "ч", "ш" };
            string[] latEngChars = { "X", "x", "Y", "y", "Q", "q", "W", "w" };
            if (cyrillic)
            {
                for (int i = 0; i < lat_up.Length; i++)
                {
                    if (!(i == 1 || i == 3 || i ==5))
                    {
                        inputText = inputText.Replace(rs_up[i], lat_up[i]);
                        inputText = inputText.Replace(rs_low[i], lat_low[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < lat_up.Length; i++)
                {
                    inputText = inputText.Replace(lat_up[i], rs_up[i]);
                    inputText = inputText.Replace(lat_low[i], rs_low[i]);
                }
                if (choice == LatinEnglishCharacter.Delete)
                {
                    foreach (var character in latEngChars)
                    {
                        inputText = inputText.Replace(character, "");
                    }
                }
                else if (choice == LatinEnglishCharacter.Mark)
                {
                    foreach (var character in latEngChars)
                    {
                        inputText = inputText.Replace(character, replacementSign+character+replacementSign);
                    }
                }
            }

            return inputText;
        }
        /// <summary>
        /// Include - Leave Q,W,Y,X inside the string
        /// Delete - Remove Q,W,Y,X characters
        /// Mark - surrounds all the Q,W,Y,X characters with sign that was passed as last argument (replacementSign)
        /// </summary>
        public enum LatinEnglishCharacter
        {
            Include,
            Delete,
            Mark
        }

        public bool Detect() {
            return true;
            //TODO
        }
    }
}
