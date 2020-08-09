using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NumberBaseConvertionLibrary
{
    public static class NumberBaseConverting
    {
        private static readonly int maxBase = 36;
  
        public static int MaxBase => maxBase;
        
        private static bool IsValidTextNumberWithBase(string textNumber, int sourceBase)
        {
            if (textNumber == null)
            {
                throw new ArgumentNullException(nameof(textNumber));
            }
            else if (sourceBase <= 0 || sourceBase > maxBase)
            {
                string paramName = nameof(sourceBase);
                throw new ArgumentOutOfRangeException(paramName, sourceBase, $"{paramName} needs be greater than zero");
            }
            else if (textNumber == String.Empty)
            {
                string paramName = nameof(textNumber);
                throw new ArgumentException($"{paramName} must not be empty", paramName);
            }
            

            foreach (char symbol in textNumber)
            {
                int currentNumericCharValue = -1;

                if (Char.IsDigit(symbol))
                {
                    currentNumericCharValue = symbol - '0';

                    if (currentNumericCharValue >= sourceBase)
                    {
                        return false;
                    }
                }
                else if (Char.IsLetter(symbol))
                {

                    // Removes case sensitivity for letters.
                    char upperLetter = Char.ToUpper(symbol);

                    // For numbers of higher base than 10 only A-Z are accepted.
                    if (upperLetter >= 'A' && upperLetter <= 'Z')
                    {
                        currentNumericCharValue = (int)(upperLetter - 'A') + 10;

                        if (currentNumericCharValue >= sourceBase)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        string paramName = nameof(textNumber);
                        throw new ArgumentException($"{paramName} has a not valid symbol", paramName);
                    }
                }
                else
                {
                    string paramName = nameof(textNumber);
                    throw new ArgumentException($"{paramName} has a not valid symbol", paramName);
                }
                
            }

            return true;
        }

        public static int NumberValueFromTextNumber(string textNumber, int sourceBase)
        {

            if (NumberBaseConverting.IsValidTextNumberWithBase(textNumber, sourceBase) == false)
            {
                return -1;
            }
            else if (sourceBase == 1)
            {
                return textNumber.Length - 1;
            }

            int numericValueOfTextNumber = 0;
            double currentBaseMultiplier = 0.0;

            for (var i = textNumber.Length - 1; i > -1; i--)
            {
                char symbol = textNumber[i];
                int currrentSymbolPlaceValue;
                if (Char.IsDigit(symbol))
                {
                    currrentSymbolPlaceValue = symbol - '0';
                }
                else
                {
                    currrentSymbolPlaceValue = (Char.ToUpper(symbol) - 'A') + 10;
                }

                numericValueOfTextNumber += currrentSymbolPlaceValue * (int)Math.Pow((double)sourceBase, currentBaseMultiplier);
                currentBaseMultiplier++;
            }
            
            return numericValueOfTextNumber;

        }      
        

        public static string TextNumberFromNumberValue(int numericValue, int targetBase)
        {
            var textNumberReverted = new StringBuilder();
            int currrentNumericValue = numericValue;

            if (numericValue < 0)
            {
                string paramName = nameof(numericValue);
                throw new ArgumentOutOfRangeException(paramName, numericValue, $"{paramName} must not be negative.");
            }
            else if (targetBase <= 0 || targetBase > MaxBase)
            {
                string paramName = nameof(targetBase);
                throw new ArgumentOutOfRangeException(paramName, targetBase, $"{paramName} must be greater than zero.");
            }

            StringBuilder textNumber;

            if (targetBase == 1)
            {
                textNumber  = new StringBuilder(numericValue + 1);
                for (var i = 0; i < numericValue + 1; i++)
                {
                    textNumber.Append('0');
                }

                return textNumber.ToString();
            }




            do
            {
                int currentNumberValue = currrentNumericValue % targetBase;
                char currentChar;

                if (currentNumberValue >= 0 && currentNumberValue < 10)
                {
                    currentChar = (char)('0' + currentNumberValue);
                }
                else
                {
                    currentChar = (char)('A' + (currentNumberValue - 10));
                }

                textNumberReverted.Append(currentChar);
                currrentNumericValue /= targetBase;

            } while (currrentNumericValue > 0);

            textNumber = new StringBuilder(textNumberReverted.Length);
            
            for (var i = textNumberReverted.Length - 1; i > -1;  i-- )
            {
                textNumber.Append(textNumberReverted[i]);    
            }


            return textNumber.ToString();
        }

    }
}
