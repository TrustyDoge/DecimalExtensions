using System;
using System.Linq;

namespace TrustedDoge.DecimalExtensions
{
    /// <summary>
    ///     Contains all the functions of <see cref="TrustedDoge.DecimalExtensions"/>.
    /// </summary>
    public static class MuchExtensions
    {
        /// <summary>
        ///     Tells you how many decimal digits a number has.
        /// </summary>
        /// <param name="number">The number of which you want to know the amount of decimal digits.</param>
        /// <param name="ignoreTrailingZeros">If <see langword="true"/>, trailing zeros are ignored, so 0.01000 can result in 2 if set to <see langword="true"/>, or 5 if it is set to <see langword="false"/>./></param>
        public static int GetDecimalDigits(this decimal number, bool ignoreTrailingZeros)
        {
            if (ignoreTrailingZeros)
            {
                var fullString = $"{number:#.################################}";
                if ((number == 0) | !fullString.Contains(".")) return 0;

                var decimalsString = fullString.Split('.').LastOrDefault();
                return decimalsString?.Length ?? 0;
            }
            else
            {
                var bits = decimal.GetBits(number);
                var bytes = BitConverter.GetBytes(bits[3]);
                var decimalsInPrice = bytes[2];
                return (int)decimalsInPrice;
            }
        }

        /// <summary>
        ///     Rounds up a number to next bigger number with the chosen decimal precision.
        /// </summary>
        /// <param name="value">Number to be rounded up.</param>
        /// <param name="decimalPlaces">The decimal precision for the result.</param>
        /// <returns></returns>
        public static decimal RoundUp(this decimal value, int decimalPlaces) => Round(value, decimalPlaces, true);

        /// <summary>
        ///     Rounds down a number to next smaller number with the chosen decimal precision.
        /// </summary>
        /// <param name="value">Number to be rounded down.</param>
        /// <param name="decimalPlaces">The decimal precision for the result.</param>
        /// <returns></returns>
        public static decimal RoundDown(this decimal value, int decimalPlaces) => Round(value, decimalPlaces, false);

        private static decimal Round(decimal value, int decimals, bool up)
        {
            var roundMultiplicator = (decimal)Math.Pow(10, decimals);
            var multiplicatedAmount = value * roundMultiplicator;
            var powedAmountRounded = up ? Math.Ceiling(multiplicatedAmount) : Math.Floor(multiplicatedAmount);
            var roundedValue = powedAmountRounded / roundMultiplicator;
            return roundedValue;
        }

    }



}
