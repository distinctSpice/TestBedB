using CensusProcessing.Models;
using CensusProcessing.Processes.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CensusProcessing.Factories
{
    public class CensusProcessorFactory : ICensusProcessorFactory
    {
        private readonly Type[] _censusProcessorTypes;

        public CensusProcessorFactory()
        {
            // Get all validators and add them into the validators list
            _censusProcessorTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsInterface
                    && !t.IsAbstract
                    && t.Namespace == "CensusProcessing.Processes.Implementations"
                    && typeof(ICensusProcessor).IsAssignableFrom(t))
                .ToArray();
        }

        public async Task<Result> ProcessAsync(Census census)
        {
            var processors = _censusProcessorTypes.Select(t => (ICensusProcessor)Activator.CreateInstance(t, census)).ToList();

            return await processors
                .Single(processor => processor.CanProcessType())
                .ProcessAsync()
                .ConfigureAwait(false);
        }
    }
}