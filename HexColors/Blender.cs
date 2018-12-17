using System;
using System.Linq;

namespace HexColors
{
    public class Blender
    {
        private readonly string[] _hexColors;

        public Blender(params string[] hexColors)
        {
            _hexColors = hexColors;
        }

        public string Blend()
        {
            var hexColorChannels =
                _hexColors.Select(color =>
                    color.SplitToHexChannels().Select(hex => hex.ToInt()
                    ).ToList());

            return new Encoder(
                GetCeilingForHexConversion(hexColorChannels.Sum(c => c[0])),    // All red channels
                GetCeilingForHexConversion(hexColorChannels.Sum(c => c[1])),    // All blue channels
                GetCeilingForHexConversion(hexColorChannels.Sum(c => c[2]))     // All green channels
                ).Encode();
        }

        private int GetCeilingForHexConversion(int value)
        {
            return (int)Math.Round(Convert.ToDouble(value) / Convert.ToDouble(_hexColors.Length));
        }
    }
}
