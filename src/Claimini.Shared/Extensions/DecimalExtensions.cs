using System.Globalization;

namespace Claimini.Shared.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Converts <paramref name="number"/> to Currency of the given <paramref name="locale"/>
        /// </summary>
        /// <param name="number">The decimal to get teh CUrrency representation for</param>
        /// <param name="locale">The Currency's desired Locale</param>
        /// <returns></returns>
        public static string ToCurrency(this decimal number, string locale)
        {
            return number.ToString("C", new CultureInfo(locale, false));
        }
    }
}
