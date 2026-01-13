using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Engine.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parse_SingleDigit_DigitReturned()
        {
            Parser parser = new();

            var result = parser.ParseValues("1");

            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(1));
        }

        [Test]
        public void Parse_TwoValuesWithAddOperator_ValuesReturned()
        {
            Parser parser = new();

            var result = parser.ParseValues("1+1");

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(1));
            Assert.That(result[1], Is.EqualTo(1));
        }

        [TestCase('1', ExpectedResult = true)]
        [TestCase('2', ExpectedResult = true)]
        [TestCase('3', ExpectedResult = true)]
        [TestCase('4', ExpectedResult = true)]
        [TestCase('5', ExpectedResult = true)]
        [TestCase('6', ExpectedResult = true)]
        [TestCase('7', ExpectedResult = true)]
        [TestCase('8', ExpectedResult = true)]
        [TestCase('9', ExpectedResult = true)]
        public bool IsDigit_CharacterIsDigit_True(char character)
        {
            Parser parser = new();

            return Parser.IsDigit(character);
        }

        [TestCase('A', ExpectedResult = false)]
        public bool IsDigit_CharacterIsNotDigit_False(char character)
        {
            Parser parser = new();

            return Parser.IsDigit(character);
        }

        [TestCase('+', ExpectedResult = true)]
        [TestCase('-', ExpectedResult = true)]
        [TestCase('/', ExpectedResult = true)]
        [TestCase('*', ExpectedResult = true)]
        [TestCase('=', ExpectedResult = true)]
        public bool IsOperator_CharacterIsOperator_True(char character)
        {
            Parser parser = new();

            return parser.IsOperator(character);
        }

        [TestCase('A', ExpectedResult = false)]
        public bool IsDigit_CharacterIsNotOperator_False(char character)
        {
            Parser parser = new();

            return parser.IsOperator(character);
        }

    }
}
