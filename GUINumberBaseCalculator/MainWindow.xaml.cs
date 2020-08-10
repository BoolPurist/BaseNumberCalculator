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
            string inputNumberText = numberInputTextBox.Text.Trim();
            string sourceBaseText = numberSourceBaseTextBox.Text.Trim();
            string targetBaseText = numberTargetBaseTextBox.Text.Trim();
            TextBox resultTextBlock = numberResultTextBlock;

            SolidColorBrush errorColor = Brushes.Red;
            if (inputNumberText == String.Empty)
            {
                resultTextBlock.Text = "Enter a number to be converted !";
                resultTextBlock.Foreground = errorColor;
                return;
            }
            else if (sourceBaseText == String.Empty)                
            {
                resultTextBlock.Text = "Enter a number as source base for the number to be converted !";
                resultTextBlock.Foreground = errorColor;
                return;
            }
            else if (targetBaseText == String.Empty)
            {
                resultTextBlock.Text = "Enter number as target base for the number to be converted !";
                resultTextBlock.Foreground = errorColor;
                return;
            }

            if (this.IsOnlyDigits(sourceBaseText) == false)
            {
                resultTextBlock.Text = "Number as source base must be only digits !";
                resultTextBlock.Foreground = errorColor;
                return;
            }
            else if (this.IsOnlyDigits(targetBaseText) == false)
            {
                resultTextBlock.Text = "Number as target base must be only digits !";
                resultTextBlock.Foreground = errorColor;
                return;
            }


            int sourceBase;
            int targetBase;

            if (Int32.TryParse(sourceBaseText, out sourceBase) == false)
            {
                resultTextBlock.Text = "The number for the source base must be only digits !";
                resultTextBlock.Foreground = errorColor;
                return;
            }
            if (Int32.TryParse(targetBaseText, out targetBase) == false)
            {
                resultTextBlock.Text = "The number for the target base must be only digits !";
                resultTextBlock.Foreground = errorColor;
                return;
            }

            int numericValue;
            try
            {
                numericValue = NumberBaseConverting.NumberValueFromTextNumber(inputNumberText, sourceBase);

                if (numericValue == -1)
                {
                    resultTextBlock.Text = $"source base \"{sourceBase}\" is not high enough for the input text number !";
                    resultTextBlock.Foreground = errorColor;
                    return;

                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                resultTextBlock.Text = $"The number for source base \"{sourceBase}\" is not greater than zero!";
                resultTextBlock.Foreground = errorColor;
                return;
            }
            catch (ArgumentException exception)
            {
                resultTextBlock.Text = $"The input text number \"{inputNumberText}\" is has not valid symbols !";
                resultTextBlock.Foreground = errorColor;
                return;
            }            

            try
            {
                resultTextBlock.Text = NumberBaseConverting.TextNumberFromNumberValue(numericValue, targetBase);
                resultTextBlock.Foreground = Brushes.Green;
            } catch (ArgumentOutOfRangeException exception)
            {
                resultTextBlock.Text = $"The number for target base \"{targetBase}\" is not greater than zero !";
                resultTextBlock.Foreground = errorColor;
                return;
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
