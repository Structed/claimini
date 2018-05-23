using System;

namespace Claimini.Shared.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// Returns the UTC representation of a UNIX Timestamp as a DateTime struct
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime epochStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            epochStartDateTime = epochStartDateTime.AddSeconds(unixTimeStamp);

            return epochStartDateTime;
        }
    }
}
