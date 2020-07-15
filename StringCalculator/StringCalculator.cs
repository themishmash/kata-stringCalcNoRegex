using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            if (input == "") return 0; 
            var defaultDelimiters = new[] {
                ",",
                "\n"
            };
            
            var delimiters = HasCustomDelimiters(input) ? GetCustomDelimiters(input) : defaultDelimiters;
            var stringToCalculate = HasCustomDelimiters(input) ? GetStringToCalculate(input) : input;
            var stringNumbers = stringToCalculate.Split(delimiters, StringSplitOptions.None);
            
            if (HasNegatives(stringNumbers)) ThrowNegativeNumberException(stringNumbers);

            var numbersToAdd = GetNumbersLessThan1000(stringNumbers);
            return numbersToAdd.Sum();
        }
        
        private static bool HasCustomDelimiters(string input)
        {
            return input.StartsWith("//");
        }

        private string [] GetCustomDelimiters(string input)
        {
            if (!input.StartsWith("//[")) return new[]{input[2].ToString()}; // //;\n without the square bracket
            
            if (!input.Contains("]")) return new[]{input[2].ToString()}; //implicitly this starts with //[
            
            //implicitly saying starts with //[ and contains ]
            var delimiterDeclaration = input.Substring(3).Split("]\n")[0];
            var customDelimiters = delimiterDeclaration.Split("][");  
            return customDelimiters;
        }
        
        private string GetStringToCalculate(string input)
        {
            if (!input.StartsWith("//[")) return input.Substring(4);
            
            if(!input.Contains("]")) return input.Substring(4); //added in this as we want to have [ to be a delimiter 
            
            var stringToCalculate = input.Substring(3).Split("]\n")[1];
            return stringToCalculate;
        }
        
        private static bool HasNegatives(IEnumerable<string> stringNumbers)
        {
            return stringNumbers.Any(x => int.Parse(x) < 0);
        }

        private static void ThrowNegativeNumberException(string[] stringNumbers)
        {
            var negatives = stringNumbers.Where(x => int.Parse(x) < 0);
            throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}");
        }

        private static IEnumerable<int> GetNumbersLessThan1000(IEnumerable<string> stringNumbers)
        {
             return stringNumbers.Select(int.Parse).Where(number => number < 1000);
        }
    }
}