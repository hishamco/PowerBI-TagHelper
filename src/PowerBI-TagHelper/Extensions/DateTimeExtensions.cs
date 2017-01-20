using System;

namespace PowerBI_TagHelper.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToUnixTimestamp(this DateTime date) =>
            (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
