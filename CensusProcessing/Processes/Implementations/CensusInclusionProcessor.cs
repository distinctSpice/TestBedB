using CensusProcessing.Enums;
using CensusProcessing.Models;
using CensusProcessing.Processes.Implementations.BaseImplementations;
using CensusProcessing.Processes.Interfaces;
using System.Threading.Tasks;
using System;

namespace CensusProcessing.Processes.Implementations
{
    public class CensusInclusionProcessor : BaseCensusProcessor, ICensusProcessor
    {
        public CensusInclusionProcessor(Census census) : base(census)
        {
        }

        public bool CanProcessType()
        {
            return _census.ProcessType == ProcessType.I;
        }

        public async Task<Result> ProcessAsync()
        {
            try
            {
                ValidateCensus();
                return new Result
                {
                    IsSuccess = true,
                    Message = "Inclusion process successful",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Inclusion process unsuccessful",
                    Exception = ex
                };
            }
        }

        public override void ValidateCensus()
        {
            base.ValidateCensus();
        }
    }
}
