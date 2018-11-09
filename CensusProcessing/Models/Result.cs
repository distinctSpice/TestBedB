using System;

namespace CensusProcessing.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
