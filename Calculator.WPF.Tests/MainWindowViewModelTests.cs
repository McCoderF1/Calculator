using Calculator.Engine;
using Calculator.Engine.Interfaces;
using Calculator.WPF.ViewModels;
using NSubstitute;

namespace Calculator.WPF.Tests
{
    public class Tests
    {
        [Test]
        public void CurrentText_OnConstruction_EmptyString()
        {
            MainWindowViewModel mwvm = new MainWindowViewModel(Substitute.For<IParser>(), Substitute.For<ICalculator>());

            Assert.That(mwvm.CurrentText, Is.EqualTo(string.Empty));
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        public void CurrentText_ReceivesNumber_IsNumber(string input)
        {
            MainWindowViewModel mwvm = new MainWindowViewModel(Substitute.For<IParser>(), Substitute.For<ICalculator>());

            mwvm.ReceiveValue(input);

            Assert.That(mwvm.CurrentText, Is.EqualTo(input));
        }

        public static object[] AddCases =
        {
            new string[] { "1", "+", "1", "=" }
        };

        [TestCaseSource(nameof(AddCases))]
        public void CurrentText_WithExpression_ExpressionEvaluated(string input1, string input2, string input3, string input4)
        {
            MainWindowViewModel mwvm = new MainWindowViewModel(new Parser(), new Calculator.Engine.Calculator());

            mwvm.ReceiveValue(input1);
            mwvm.ReceiveValue(input2);
            mwvm.ReceiveValue(input3);
            mwvm.ReceiveValue(input4);

            Assert.That(mwvm.CurrentText, Is.EqualTo("2"));
        }

    }
}
