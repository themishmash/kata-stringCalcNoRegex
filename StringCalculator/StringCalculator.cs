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
            var defaultDelimiters = new[]{',','\n'};
            var inputComponents = input.Split('\n');
            //return input.StartsWith("//") && input.EndsWith("\n");
            var delimiters = HasCustomDelimiters(input) ? GetCustomDelimiters(input) : defaultDelimiters;
            var stringToCalculate = HasCustomDelimiters(input) ? input.Substring(4): input;
            var stringNumbers = stringToCalculate.Split(delimiters);
            
            if (HasNegatives(stringNumbers))
            {
                var negatives = stringNumbers.Where(x => int.Parse(x) < 0);
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}");
            }
            return GetNumbersLessThan1000(stringNumbers).Sum();
        }

        private static IEnumerable<int> GetNumbersLessThan1000(IEnumerable<string> stringNumbers)
        {
             return stringNumbers.Select(int.Parse).Where(number => number < 1000);
        }

        private static bool HasNegatives(IEnumerable<string> stringNumbers)
        {
            return stringNumbers.Any(x => int.Parse(x) < 0);
        }

        private char[] GetCustomDelimiters(string input) //just take in left side component and see if starts with //[ and ends with ] . Split on \n character. Split before call getcustomdelimiters. 
        {
            //
            return new[]{input[2]};
        }

        private static bool HasCustomDelimiters(string input)
        {
            return input.StartsWith("//");
        }
        
    }
}