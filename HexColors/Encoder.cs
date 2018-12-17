namespace HexColors
{
    public class Encoder
    {
        private readonly int _redChannel;
        private readonly int _greenChannel;
        private readonly int _blueChannel;

        public Encoder(int redChannel, int greenChannel, int blueChannel)
        {
            _redChannel = redChannel;
            _greenChannel = greenChannel;
            _blueChannel = blueChannel;
        }

        public string Encode() => $"#{_redChannel.ToByte():X}{_greenChannel.ToByte():X}{_blueChannel.ToByte():X}";
    }
}
