using Calculator.Engine.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Calculator.WPF.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private const string EqualsSign = "=";

        private readonly IParser parser;
        private readonly ICalculator calculator;

        private string currentText = string.Empty;

        public string CurrentText
        {
            get => currentText;
            private set => SetProperty(ref currentText, value);
        }

        public ICommand ReceiveInputCommand { get; }

        public MainWindowViewModel(IParser parser, ICalculator calculator)
        {
            this.parser = parser;
            this.calculator = calculator;

            ReceiveInputCommand = new RelayCommand<object>(input => ReceiveValue((string)input));
        }

        private void ReceiveValue(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            if (input == EqualsSign)
            {
                long? res = calculator.EvaluateExpression(CurrentText);
                if (res.HasValue)
                    CurrentText = res.Value.ToString();

                return;
            }

            // Basic validation: don’t allow two operators in a row
            if (input.Length == 1 && parser.IsOperator(input[0]))
            {
                if (CurrentText.Length == 0)
                    return;

                char last = CurrentText[^1];
                if (parser.IsOperator(last))
                    return;
            }

            CurrentText += input;
        }
    }
}
