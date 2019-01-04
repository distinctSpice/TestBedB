namespace UPC
{
    public class DigitChecker
    {
        private readonly string _stringUpc;

        public DigitChecker(long upc)
        {
            _stringUpc = upc.ToString();
            if (_stringUpc.Length < 11)
            {
                while (_stringUpc.Length < 11)
                {
                    _stringUpc = '0' + _stringUpc;
                }
            }
        }

        public string GetFullValidUpc()
        {
            return _stringUpc + GetCheckDigit().ToString();
        }

        public int GetCheckDigit()
        {
            var sumEven = 0;
            var sumOdd = 0;
            for (int i = 0; i < _stringUpc.Length; i++)
            {
                var value = (int)char.GetNumericValue(_stringUpc[i]);
                if (i % 2 == 0)
                    sumEven += value;
                else
                    sumOdd += value;
            }
            var m = ((sumEven * 3) + sumOdd) % 10;
            return m != 0 ? 10 - m : 0;
        }
    }
}
