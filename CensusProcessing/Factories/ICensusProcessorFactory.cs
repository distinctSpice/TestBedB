using CensusProcessing.Models;
using System.Threading.Tasks;

namespace CensusProcessing.Factories
{
    public interface ICensusProcessorFactory
    {
        Task<Result> ProcessAsync(Census census);
    }
}
