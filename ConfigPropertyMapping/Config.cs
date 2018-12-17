using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigPropertyMapping
{
    public class Config
    {
        public string GetCandleRateEndpoint => "rates/candle.json";

        public string GetCandlesRateEndpoint => "rates/candles.json";
    }
}
