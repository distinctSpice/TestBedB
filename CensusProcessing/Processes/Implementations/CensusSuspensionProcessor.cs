using CensusProcessing.Models;
using CensusProcessing.Processes.Interfaces;
using System.Threading.Tasks;
using CensusProcessing.Enums;
using CensusProcessing.Processes.Implementations.BaseImplementations;
using System;

namespace CensusProcessing.Processes.Implementations
{
    public class CensusSuspensionProcessor : BaseCensusProcessor, ICensusProcessor
    {
        public CensusSuspensionProcessor(Census census) : base(census)
        {
        }

        public bool CanProcessType()
        {
            return _census.ProcessType == ProcessType.S;
        }
        
        public async Task<Result> ProcessAsync()
        {
            try
            {
                ValidateCensus();
                return new Result
                {
                    IsSuccess = false,
                    Message = "Suspension process successful",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Suspension process unsuccessful",
                    Exception = ex
                };
            }
        }
    }
}
