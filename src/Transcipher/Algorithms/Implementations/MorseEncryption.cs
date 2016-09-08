using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transcipher.Algorithms.Implementations
{
    public class MorseEncryption : IEncryptionAlgorithm
    {
        private readonly Dictionary<char, string> _morseDictionary = new Dictionary<char, string>()
        {
            {'a', ".-"}, {'b', "-..."}, {'c', "-.-."},
            {'d', "-.."}, {'e', "."}, {'f', "..-."},
            {'g', "--."}, {'h', "...."}, {'i', ".."},
            {'j', ".---"}, {'k', "-.-"}, {'l', ".-.."},
            {'m', "--"}, {'n', "-."}, {'o', "---"},
            {'p', ".--."}, {'q', "--.-"}, {'r', ".-."},
            {'s', "..."}, {'t', "-"}, {'u', "..-"},
            {'v', "...-"}, {'w', ".--"}, {'x', "-..-"},
            {'y', "-.--"}, {'z', "--.."},
            {'0', "-----"}, {'1', ".----"}, {'2', "..---"},
            {'3', "...--"}, {'4', "....-"}, {'5', "....."},
            {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}
        };

        public string Encrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            input = input.Trim().ToLower();

            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < input.Length; index++)
            {
                if (_morseDictionary.ContainsKey(input[index]))
                {
                    sb.Append(_morseDictionary[input[index]]);
                }

                if (input[index] == ' ')
                {
                    sb.Append("/");
                }
                else if (index != input.Length - 1 && input[index + 1] != ' ')
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public string Decrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            var words = input.Split('/');

            StringBuilder sb = new StringBuilder();
            foreach (var word in words)
            {
                var letters = word.Split(' ');
                foreach (var letter in letters)
                {
                    var morseValue = _morseDictionary.FirstOrDefault(x => x.Value == letter).Key;
                    sb.Append(morseValue);
                }

                sb.Append(" ");
            }

            return sb.ToString().Trim();
        }
    }
}