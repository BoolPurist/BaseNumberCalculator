using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NumberBaseConversionLibrary
{
    /// <summary>
    /// Class provides a way of converting a text number with a certain base into either a integer or 
    /// a string as a number with a different base.
    /// </summary>
    /// <example>
    /// Converting text number "A4" with base 16 into the number "10010100" with base "2".
    /// That example shows how to do it.
    /// <code>
    /// string sourceNumber = "A4";
    /// int sourceInteger = NumberBaseConverting.NumberValueFromTextNumber(sourceNumber, 16);
    /// // sourceInteger = 164, with the method <see cref = "NumberBaseConverting.NumberValueFromTextNumber" />
    /// string targetNumber = NumberBaseConverting.TextNumberFromNumberValue(sourceInteger, 2 );
    /// // tragetNumber = "10010100", with the method <see cref = "NumberBaseConverting.TextNumberFromNumberValue" />
    /// // targetNumber is the result.
    /// </code>
    /// </example>
    public static class NumberBaseConverting
    {
        private static readonly int maxBase = 36;
        /// <summary> Determines the possible highest base for conversion numbers </summary>
        /// <value> Get-Accessors on the readonly field maxBase with the value 36 currently</value>
        public static int MaxBase => maxBase;
        /// <summary> Gets the numeric value from a number as text. </summary>
        /// <param name="textNumber"> text number which is converted to a numeric value</param>
        /// <param name="sourceBase"> Base which the text number is based on. </param>
        /// <returns> 
        /// Numeric value that comes from the conversion out of the text number <paramref name = "textNumber" />.
        /// Returns -1 if the text number has at least one char with values above the <paramref name = "sourceBase" />.
        /// Example: With "A4" for <paramref name = "textNumber" /> and 10 for <paramref name = "sourceBase" /> returns -1,
        /// because 'A' only exit in a number systems with a base of above 10.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">  Occurs if parameter <paramref name = "textNumber" /> is null </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Occurs if parameter <paramref name = "sourceBase" /> is zero or smaller.
        /// Occurs if the parameter is greater than the value of <see cref = "NumberBaseConverting.MaxBase" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Occurs if the <paramref name = "textNumber" /> is an empty string or has no char which is from 0-9, a-z or A-Z.
        /// </exception>
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

        private static bool IsValidTextNumberWithBase(string textNumber, int sourceBase)
        {
            if (textNumber == null)
            {
                throw new ArgumentNullException(nameof(textNumber));
            }
            else if (sourceBase <= 0 || sourceBase > maxBase)
            {
                string paramName = nameof(sourceBase);
                throw new ArgumentOutOfRangeException(
                    paramName,
                    sourceBase,
                    $"{paramName} needs be greater than zero must not be greater {NumberBaseConverting.MaxBase}."
                    );
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
                
        /// <summary> Converts an numeric value into a text number <paramref name = "targetBase" /> </summary>
        /// <param name="targetBase"> The base for the new text number </param>
        /// <param name="numericValue"> 
        /// numeric value as base for the text number. 
        /// It is converted to a text number with <paramref name = "targetBase" /> as a base. 
        /// </param>
        /// <returns> text number which hat the base <paramref name = "targetBase" /> </returns>
        /// <exception cref="System.ArgumentOutOfRangeException"> 
        /// Occurs if <paramref name = "targetBase" /> is equal to or smaller than zero.
        /// Occurs if the parameter is bigger than the maximum base, <see cref = "MaxBase" />.
        /// </exception>
        public static string TextNumberFromNumberValue(int numericValue, int targetBase)
        {
            var textNumberReverted = new StringBuilder();
            int currrentNumericValue = numericValue;

            if (targetBase <= 0 || targetBase > MaxBase)
            {
                string paramName = nameof(targetBase);
                throw new ArgumentOutOfRangeException(paramName, targetBase, $"{paramName} must be greater than zero.");
            }

            StringBuilder textNumber;

            if (targetBase == 1)
            {
                textNumber = new StringBuilder(numericValue + 1);
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

            for (var i = textNumberReverted.Length - 1; i > -1; i--)
            {
                textNumber.Append(textNumberReverted[i]);
            }


            return textNumber.ToString();
        }



    }
}
