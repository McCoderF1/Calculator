using Calculator.Engine.Interfaces;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
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

        public void ReceiveValue(string input)
        {
            // Check for equals sign
            // otherwise tack on value to text

            if (input.Equals(EqualsSign))
            {
                int[] values = parser.ParseValues(CurrentText);
                int? res = calculator.Add(values);

                if (res.HasValue)
                    CurrentText = res.Value.ToString();

                return;
            }


            CurrentText += input;


        }
    }
}
