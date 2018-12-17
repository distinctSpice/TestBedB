using System;
using System.Collections.Generic;

namespace HexColors
{
    public static class Extensions
    {
        public static int ToInt(this string hexValue) => int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

        public static byte ToByte(this int value) => Convert.ToByte(value);

        public static List<string> SplitToHexChannels(this string hexColor)
        {
            var hexes = hexColor.Substring(1);
            const int partLength = 2;

            var results = new List<string>();
            for (var i = 0; i < hexes.Length; i += partLength)
            {
                results.Add(hexes.Substring(i, Math.Min(partLength, hexes.Length - i)));
            }
            return results;
        }
    }
}
