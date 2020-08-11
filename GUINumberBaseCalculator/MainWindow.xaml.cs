using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NumberBaseConversionLibrary;

namespace GUINumberBaseCalculator
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
          
        private void Button_NumberConvert_Click(object sender, RoutedEventArgs e)
        {            
            bool validInput;
            
            numberResultTextBlock.Text = this.GetConvertedTextNumberFromBaseTextNumber(
                numberInputTextBox.Text,
                numberSourceBaseTextBox.Text, 
                numberTargetBaseTextBox.Text, 
                out validInput
                );

            numberResultTextBlock.Foreground = validInput ? Brushes.Green : Brushes.Red;
        }

        private string GetConvertedTextNumberFromBaseTextNumber(string inputNumberText, string sourceBaseText, string targetBaseText, out bool validInput)
        {
            
            validInput = false;
            if (inputNumberText == String.Empty)
            {                
                return "Enter a number to be converted !";
            }
            else if (sourceBaseText == String.Empty)
            {                
                return "Enter a number as source base for the number to be converted !";
            }
            else if (targetBaseText == String.Empty)
            {                
                return "Enter number as target base for the number to be converted !";
            }

            if (this.IsOnlyDigits(sourceBaseText) == false)
            {                
                return "Number as source base must be only digits !";
            }
            else if (this.IsOnlyDigits(targetBaseText) == false)
            {                
                return "Number as target base must be only digits !";
            }


            int sourceBase;
            int targetBase;

            if (Int32.TryParse(sourceBaseText, out sourceBase) == false)
            {                
                return "The number for the source base must be only digits !";
            }
            if (Int32.TryParse(targetBaseText, out targetBase) == false)
            {                
                return "The number for the target base must be only digits !";
            }

            int numericValue;
            try
            {
                numericValue = NumberBaseConverting.NumberValueFromTextNumber(inputNumberText, sourceBase);

                if (numericValue == -1)
                {                    
                    return $"source base \"{sourceBase}\" is not high enough for the input text number !";
                }
            }
            catch (ArgumentOutOfRangeException)
            {                
                return $"The number for source base \"{sourceBase}\" is not greater than zero!";
            }
            catch (ArgumentException)
            {                
                return $"The input text number \"{inputNumberText}\" is has not valid symbols !";
            }

            try
            {
                string result = NumberBaseConverting.TextNumberFromNumberValue(numericValue, targetBase);
                validInput = true;
                return result;
            }
            catch (ArgumentOutOfRangeException)
            {                
                return $"The number for target base \"{targetBase}\" is not greater than zero !";
            }

        }

        private bool IsOnlyDigits(string digitText)
        {
            foreach (char symbol in digitText)
            {
                if (Char.IsDigit(symbol) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
