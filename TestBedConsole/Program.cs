using System;
using CensusProcessing.Models;
using CensusProcessing.Enums;
using CensusProcessing.Factories;
using System.Threading.Tasks;

namespace TestBedConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var result = ProcessTestCensus().GetAwaiter().GetResult();

            Console.WriteLine("Result successful? {0}", result.IsSuccess);
            Console.WriteLine("Message: {0} ", result.Message);
            if (result.Exception != null)
            {
                Console.WriteLine("Exception: {0} ", result.Exception.Message);
            }

            Console.ReadKey();
        }

        public static async Task<Result> ProcessTestCensus()
        {
            var testCensus = new Census()
            {
                EffectiveDate = "11/8/2018",
                DeletionDate = "11/8/2019",
                DateOfBirth = "10/15/1992",
                Gender = "Male",
                Email = "mail.@mail.com",
                ProcessType = ProcessType.S
            };

            var factory = new CensusProcessorFactory();

            return await factory
                .ProcessAsync(testCensus)
                .ConfigureAwait(false);
        }
    }
}
