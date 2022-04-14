using System;

namespace WalletManager.Domain.Helper
{
    public class TimestampHelper
    {
        public static long Now()
        {
            return UtcDateTimeToUtcTimeStamp(DateTime.UtcNow);
        }

        public static DateTime ToLocalDateTime(long utcTimeStamp)
        {
            return GTM().AddMilliseconds(utcTimeStamp).ToLocalTime();
        }

        private static DateTime GTM()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // GTM時間
        }

        private static long UtcDateTimeToUtcTimeStamp(DateTime utcDatetime)
        {
            if (utcDatetime.Kind != DateTimeKind.Utc)
                throw new ArgumentException(
                    $"UtcDateTimeToUtcTimeStamp, {utcDatetime}, utcDatetime.Kind({utcDatetime.Kind}) != System.DateTimeKind.Utc");

            var gtm = GTM();
            return Convert.ToInt64(utcDatetime.Subtract(gtm).TotalMilliseconds);
        }
    }
}