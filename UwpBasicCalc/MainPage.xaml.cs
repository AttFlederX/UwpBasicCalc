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
        private bool _isOnStandby;
        private string _currentOperation;

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

        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
