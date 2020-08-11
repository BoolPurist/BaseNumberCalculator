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
                        
            numberResultTextBlock.Text = this.GetConvertedTextNumberFromBaseTextNumber(
                numberInputTextBox.Text,
                numberSourceBaseTextBox.Text, 
                numberTargetBaseTextBox.Text, 
                out bool validInput
                );

            numberResultTextBlock.Foreground = validInput ? Brushes.Green : Brushes.Red;
        }

        // Converts parameter "inputNumberText" with parameter "sourceBaseText" into
        // a text number with the base of "targetBaseText" and returns it.
        private string GetConvertedTextNumberFromBaseTextNumber(string inputNumberText, string sourceBaseText, string targetBaseText, out bool validInput)
        {                        
            validInput = false;
            // Check if any input by the user is empty or has only spaces
            if (inputNumberText.Trim() == String.Empty)
            {                
                return "Enter a number to be converted !";
            }
            else if (sourceBaseText.Trim() == String.Empty)
            {                
                return "Enter a number as source base for the number to be converted !";
            }
            else if (targetBaseText.Trim() == String.Empty)
            {                
                return "Enter number as target base for the number to be converted !";
            }

            // Checks if strings for source base and target base are made of only digits.
            if (this.IsOnlyDigits(sourceBaseText) == false)
            {                
                return "Number as source base must be only digits !";
            }
            else if (this.IsOnlyDigits(targetBaseText) == false)
            {                
                return "Number as target base must be only digits !";
            }

            if (Int32.TryParse(sourceBaseText, out int sourceBase) == false)
            {                
                return "The number for the source base must be only digits !";
            }
            if (Int32.TryParse(targetBaseText, out int targetBase) == false)
            {                
                return "The number for the target base must be only digits !";
            }
            
            // First step for conversion by converting input text number into a numeric value.
            // Additionally checking if input text number has only valid chars and the base is greater than zeor
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

            // Last step of conversion. Converting numeric value into text number as a 
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
