namespace Claimini.Api.Repository
{
    public class PdfUserUnitUtils
    {
        /// <summary>
        /// Measurement conversion from millimeters to points.
        /// </summary>
        /// <param name="value">a value in millimeters</param>
        /// <returns>a value in points</returns>
        public static float MillimetersToPoints(float value)
        {
            return InchesToPoints(MillimetersToInches(value));
        }

        /// <summary>
        /// Measurement conversion from millimeters to inches.
        /// </summary>
        /// <param name="value">a value in millimeters</param>
        /// <returns>a value in inches</returns>
        public static float MillimetersToInches(float value)
        {
            return value / 25.4f;
        }

        /// <summary>
        /// Measurement conversion from inches to points.
        /// </summary>
        /// <param name="value">a value in inches</param>
        /// <returns>a value in points</returns>
        public static float InchesToPoints(float value)
        {
            return value * 72f;
        }
    }
}