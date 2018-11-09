using CensusProcessing.Models;
using System;
using System.Threading.Tasks;

namespace CensusProcessing.Processes.Interfaces
{
    public interface ICensusProcessor
    {
        bool CanProcessType();
        Task<Result> ProcessAsync();
    }
}
