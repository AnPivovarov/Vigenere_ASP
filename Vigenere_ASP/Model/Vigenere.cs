using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vigenere_ASP.Model
{
    public class Vigenere
    {
        static char[] alphabet = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        public static string letters = new string(alphabet);
        static int N = alphabet.Length;

        public static string VigenereEncrypt(string input, string keyword)
        {
            input = input.ToLower();
            keyword = keyword.ToLower();
            string result = "";
            int keyword_index = 0;
            foreach (char symbol in input)
            {
                if (letters.Contains(symbol))
                {
                    int c = (Array.IndexOf(alphabet, symbol) + 
                        Array.IndexOf(alphabet, keyword[keyword_index])) % N;
                    result += alphabet[c];
                    keyword_index++;
                }
                else
                {
                    result += symbol;
                }
                if (keyword_index + 1 > keyword.Length)
                {
                    keyword_index = 0;
                }
            }

            return result;
        }
        public static string VigenereDecrypt(string input, string keyword)
        {
            input = input.ToLower();
            keyword = keyword.ToLower();
            string result = "";
            int keyword_index = 0;
            foreach (char symbol in input)
            {
                if (letters.Contains(symbol))
                {
                    int p = (Array.IndexOf(alphabet, symbol) + N - Array.IndexOf(alphabet, keyword[keyword_index])) % N;
                    result += letters[p];
                    keyword_index++;
                }
                else
                {
                    result += symbol;
                }

                if (keyword_index + 1 > keyword.Length)
                {
                    keyword_index = 0;
                }
            }

            return result;
        }
    }
}
