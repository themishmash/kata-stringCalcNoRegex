using System;
using Xunit;
using Xunit.Sdk;

namespace StringCalculator.Tests
{
    public class StringCalculatorShould
    {
        private readonly StringCalculator _calculator;

        public StringCalculatorShould()
        {
            _calculator = new StringCalculator();
        }
        
        [Fact]
        public void ReturnZeroGivenEmptyString()
        {
            Assert.Equal(0, _calculator.Add(""));
        }
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        public void ReturnNumberGivenSingleStringNumber(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void ReturnSumGivenTwoStringNumbers(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("3,5,3,9", 20)]
        public void ReturnSumGivenManyStringNumbers(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        [Theory]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void AllowNewLineAndCommaDelimiters(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void AllowCustomDelimiters(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }

        [Theory]
        [InlineData("-1,2,-3", "Negatives not allowed: -1, -3")]
        [InlineData("2,-3,4,5", "Negatives not allowed: -3")]
        public void AllowOnlyNonNegatives(string input, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => _calculator.Add(input));
            Assert.Equal(expectedMessage, exception.Message);
        }
        
        [Theory]
        [InlineData("1000,1001,2", 2)]
        public void IgnoreNumbersGreaterThanOrEqualTo1000(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        public void DelimitersCanBeAnyLength(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _calculator.Add(input));
        }
        
        
    }
}