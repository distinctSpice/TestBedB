using CensusProcessing.Models;

namespace CensusProcessing.Processes.Implementations.BaseImplementations
{
    public class BaseCensusProcessor
    {
        protected Census _census;

        protected BaseCensusProcessor(Census census)
        {
            _census = census;
        }

        public virtual void ValidateCensus() { }
    }
}
