using CensusProcessing.Models;
using CensusProcessing.Processes.Interfaces;
using System.Threading.Tasks;
using CensusProcessing.Enums;
using CensusProcessing.Processes.Implementations.BaseImplementations;
using System;

namespace CensusProcessing.Processes.Implementations
{
    public class CensusTerminationProcessor : BaseCensusProcessor, ICensusProcessor
    {
        public CensusTerminationProcessor(Census census) : base(census)
        {
        }

        public bool CanProcessType()
        {
            return _census.ProcessType == ProcessType.T;
        }

        public async Task<Result> ProcessAsync()
        {
            try
            {
                ValidateCensus();
                return new Result
                {
                    IsSuccess = false,
                    Message = "Termination process successful",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Termination process unsuccessful",
                    Exception = ex
                };
            }
        }
    }
}
