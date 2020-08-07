using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;
using NumberBaseConvertionLibrary;

namespace NumberBaseConvertionLibraryUnitTest
{

    public class NumberBaseConvertingTest
    {
        [Theory]
        [InlineData("A4", 16, true)]
        [InlineData("A4", 9, false)]
        [InlineData("A4", 10, false)]
        [InlineData("E", 15, true)]
        [InlineData("E", 12, false)]
        [InlineData("5", 3, false)]
        [InlineData("5", 6, true)]
        public static void IsValidNumberWithBase_ShouldTellIfNumberIsValidWithGivenBase(string textNumber, int sourceBase, bool expected)
            => Assert.Equal(expected, NumberBaseConverting.IsValidNumberWithBase(textNumber, sourceBase));

        [Fact]
        public static void IsValidNumberWithBase_ShouldThrowWhenTextNumberIsNull() => Assert.Throws<ArgumentNullException>("textNumber", () => NumberBaseConverting.IsValidNumberWithBase(null, 2));

        [Theory]
        [InlineData("/*", 1)]
        [InlineData("2-5", 6)]
        [InlineData("\n", 6)]
        public static void IsValidNumberWithBase_ShouldThrowWhenTextNumberHasInvalidSymbol(string unvalidTextNumber, int validBase)
            => Assert.Throws<ArgumentException>("textNumber", () => NumberBaseConverting.IsValidNumberWithBase(unvalidTextNumber, validBase));


        [Theory]
        [MemberData(nameof(TestData.TestNegativeOrZeroBases), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.TestTooBigPositiveValue), MemberType = typeof(TestData))]
        public static void IsValidNumberWithBase_ShouldThrowWhenBaseIsOutOfValidRange(int invalidBase)
            => Assert.Throws<ArgumentOutOfRangeException>("sourceBase", () => NumberBaseConverting.IsValidNumberWithBase("1", invalidBase));


        [Fact]
        public static void IsValidNumberWithBase_ShouldThrowTextNumberIsEmpty() => Assert.Throws<ArgumentException>("textNumber", () => NumberBaseConverting.IsValidNumberWithBase("", 2));

        [Theory]
        [InlineData("1", 2, 1)]
        [InlineData("0", 2, 0)]
        [InlineData("8", 10, 8)]
        [InlineData("A", 16, 10)]
        [InlineData("A4", 16, 164)]
        [InlineData("a4b", 16, 2635)]
        [InlineData("45", 8, 37)]
        [InlineData("1000110011", 2, 563)]
        [InlineData("8", 7, -1)]
        [InlineData("DE4A", 16, 56906)]
        [InlineData("31", 4, 13)]
        [InlineData("0000", 1, 3)]
        [InlineData("0", 1, 0)]
        [InlineData("Z", 36, 35)]

        public static void NumberValueFromTextNumber_ShouldConvertTextNumberToNumericValueRight(string textNumber, int sourceBase, int expected)
            => Assert.Equal(expected, NumberBaseConverting.NumberValueFromTextNumber(textNumber, sourceBase));

        [Theory]
        [InlineData(20, 2, "10100")]
        [InlineData(0, 10, "0")]
        [InlineData(10, 10, "10")]
        [InlineData(74, 8, "112")]
        [InlineData(852, 16, "354")]
        [InlineData(840734, 16, "CD41E")]
        [InlineData(3, 1, "0000")]
        [InlineData(0, 1, "0")]
        [InlineData(35, 36, "Z")]
        [InlineData(37, 36, "11")]
        public static void TextNumberFromNumberValue_ShouldConvertNumericValueToTextNumber(int numericValue, int targetBase, string expected)
            => Assert.Equal(expected, NumberBaseConverting.TextNumberFromNumberValue(numericValue, targetBase));


        [Theory]
        [MemberData(nameof(TestData.TestNegativeOrZeroBases), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.TestTooBigPositiveValue), MemberType = typeof(TestData))]
        public static void TextNumberFromNumberValue_ShouldThrowWhenTargetBaseOutOfValidRange(int invalidBase) => Assert.Throws<ArgumentOutOfRangeException>(() => NumberBaseConverting.TextNumberFromNumberValue(1, invalidBase));


        private class TestData
        {
            public static IEnumerable<object[]> TestNegativeOrZeroBases => 
                new List<object[]>
                {
                    new object[] {0},
                    new object[] {-2},
                    new object[] {int.MinValue}
                };


            public static IEnumerable<object[]> TestTooBigPositiveValue =>
                new List<object[]>
                {
                    new object[] {NumberBaseConverting.MaxBase + 1},
                    new object[] {NumberBaseConverting.MaxBase + 10},
                    new object[] {int.MaxValue}
                };
        }

    }   
}
