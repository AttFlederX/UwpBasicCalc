using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpBasicCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool _isOnStandby; // determines whether the next digit entered should replace the previous content
        private string _currentOperation;
        private double _accumulator;

        public MainPage()
        {
            this.InitializeComponent();

            _isOnStandby = true;
            _currentOperation = string.Empty;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button digitButton)
            {
                if (outputTextBlock.Text != "0")
                {
                    var digit = digitButton.Content.ToString();
                    if (_isOnStandby)
                    {
                        outputTextBlock.Text = digit;
                        _isOnStandby = false;
                    }
                    else
                    {
                        outputTextBlock.Text += digit;
                    }
                }
            }
        }

        private void DecimalPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOnStandby && !outputTextBlock.Text.Contains('.'))
            {
                outputTextBlock.Text += ".";
            }
        }

        private void ChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOnStandby && outputTextBlock.Text != "0" && outputTextBlock.Text != "0.")
            {
                if (outputTextBlock.Text.First() != '-')
                {
                    outputTextBlock.Text = "-" + outputTextBlock.Text;
                }
                else
                {
                    outputTextBlock.Text = outputTextBlock.Text.Substring(1); // remove leading minus
                }
            }
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOnStandby)
            {
                outputTextBlock.Text = "0.";
                _isOnStandby = true;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBlock.Text = "0.";

            _isOnStandby = true;
            _currentOperation = string.Empty;
            _accumulator = 0;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOnStandby)
            {
                var len = outputTextBlock.Text.Length;
                if (len > 1)
                {
                    outputTextBlock.Text = outputTextBlock.Text.Substring(0, len - 1);
                }
                else
                {
                    outputTextBlock.Text = "0.";
                    _isOnStandby = true;
                }
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button operatorButton)
            {
                if (!_isOnStandby && string.IsNullOrWhiteSpace(_currentOperation))
                {
                    _accumulator = double.Parse(outputTextBlock.Text);
                    _currentOperation = operatorButton.Content.ToString();
                    _isOnStandby = true;
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOnStandby && !string.IsNullOrWhiteSpace(_currentOperation))
            {
                var operand = double.Parse(outputTextBlock.Text);
                double? result = 0;

                switch (_currentOperation)
                {
                    case "+":
                        result = _accumulator + operand;
                        break;
                    case "-":
                        result = _accumulator - operand;
                        break;
                    case "*":
                        result = _accumulator * operand;
                        break;
                    case "/":
                        if (operand == 0) { result = null; }
                        else
                        {
                            result = _accumulator / operand;
                        }
                        break;

                    default:
                        throw new InvalidOperationException("Invalid operation received in EqualsButton_Click");
                }

                outputTextBlock.Text = result?.ToString() ?? "Error";

                _isOnStandby = true;
                _accumulator = 0;
                _currentOperation = string.Empty;
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
