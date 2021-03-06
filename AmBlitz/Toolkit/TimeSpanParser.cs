﻿using System;
using System.Globalization;

namespace AmBlitz.ToolKit
{
    internal static class TimeSpanParser
    {
        // methods
        public static string ToString(TimeSpan value)
        {
            const int msInOneSecond = 1000;
            const int msInOneMinute = 60 * msInOneSecond;
            const int msInOneHour = 60 * msInOneMinute;

            var ms = (long)value.TotalMilliseconds;
            if ((ms % msInOneHour) == 0)
            {
                return $"{ms / msInOneHour}h";
            }
            if ((ms % msInOneMinute) == 0 && ms < msInOneHour)
            {
                return $"{ms / msInOneMinute}m";
            }
            if ((ms % msInOneSecond) == 0 && ms < msInOneMinute)
            {
                return $"{ms / msInOneSecond}s";
            }
            if (ms < 1000)
            {
                return $"{ms}ms";
            }
            return value.ToString();
        }

        public static bool TryParse(string value, out TimeSpan result)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.ToLowerInvariant();
                var end = value.Length - 1;

                var multiplier = 1000; // default units are seconds
                if (value[end] == 's')
                {
                    if (value[end - 1] == 'm')
                    {
                        value = value.Substring(0, value.Length - 2);
                        multiplier = 1;
                    }
                    else
                    {
                        value = value.Substring(0, value.Length - 1);
                        multiplier = 1000;
                    }
                }
                else if (value[end] == 'm')
                {
                    value = value.Substring(0, value.Length - 1);
                    multiplier = 60 * 1000;
                }
                else if (value[end] == 'h')
                {
                    value = value.Substring(0, value.Length - 1);
                    multiplier = 60 * 60 * 1000;
                }
                else if (value.IndexOf(':') != -1)
                {
                    return TimeSpan.TryParse(value, out result);
                }

                var numberStyles = NumberStyles.None;
                if (double.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out var multiplicand))
                {
                    result = TimeSpan.FromMilliseconds(multiplicand * multiplier);
                    return true;
                }
            }

            result = default(TimeSpan);
            return false;
        }
    }
}
