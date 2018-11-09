using System;
using TestBedB.Models;
using TestBedB.OOPValidators.Factories;

namespace TestBedB
{
    static class Program
    {
        static void Main(string[] args)
        {
            var testCensus = new Census()
            {
                EffectiveDate = "11/8/2018",
                DeletionDate = "11/8/2019",
                DateOfBirth = "10/15/1992",
                Gender = "Male",
                Email = "mail.@mail.com"
            };

            var validationFactory = new CensusValidationFactory();
            var validationResult = validationFactory.Validate(testCensus);

            Console.WriteLine("Validation result: {0}", validationResult);
            Console.ReadKey();

            //var stopWatch = Stopwatch.StartNew();
            //double validCount = 0;
            //double invalidCount = 0;
            //foreach (var emailAddress in File.ReadAllLines(@"C:\Users\itadmin\OneDrive - Marine Benefits AS\MBAS-DEV\Documents\g2prodemails.txt"))
            //{
            //    if (emailAddress.Trim().IsValidEmail())
            //    {
            //        validCount++;
            //        //Console.WriteLine("Valid: {0}", emailAddress);
            //    }
            //    else
            //    {
            //        invalidCount++;
            //        Console.WriteLine("Invalid: {0}", emailAddress);
            //    }
            //}

            //Console.WriteLine("Invalid percentage {0}%", Math.Round((invalidCount / validCount) * 100, 2));
            //Console.WriteLine("Elapsed time {0}.", stopWatch.Elapsed);

            //Console.ReadKey();

            //const decimal usedAmountLoc = 1950.4m;
            //const decimal exchangeRate = 54.0969m;
            //const string format = "{0:0.00}";

            //decimal withOutRoundingAmountUSD = usedAmountLoc / exchangeRate;
            //decimal withRoundingAmountUSD = Math.Round(withOutRoundingAmountUSD, 2);
            //decimal withDecimalConversion = Convert.ToDecimal(Math.Round(withOutRoundingAmountUSD, 2));
            //string withStringFormatting = string.Format(format, withOutRoundingAmountUSD);

            //decimal defectiveAmountUSD = Math.Round(Convert.ToDecimal(string.Format(format, withOutRoundingAmountUSD)), 2);
            //decimal effectiveAmountUSD = Convert.ToDecimal(string.Format(format, withRoundingAmountUSD));

            //Console.WriteLine("without rounding: {0}", withOutRoundingAmountUSD);
            //Console.WriteLine("with rounding: {0}", withRoundingAmountUSD);
            //Console.WriteLine("with decimal conversion: {0}", withDecimalConversion);
            //Console.WriteLine("with string formatting: {0}", withStringFormatting);
            //Console.WriteLine("defective: {0} vs effective: {1}", defectiveAmountUSD, effectiveAmountUSD);
            //Console.ReadKey();

            //var enable1WordList = File.ReadAllLines(@"C:\Users\itadmin\OneDrive - Marine Benefits AS\Personal\Daily Programmer\enable1.txt");

            //Console.Write("Enter minimum amount of words displayed: ");

            //var take = Convert.ToInt32(Console.ReadLine());
            //var wordsWithUniqueLetters = enable1WordList
            //    .Where(word => word.ToCharArray().Distinct().Count() == word.Length)
            //    .OrderByDescending(word => word.Length)
            //    .Take(take);

            //foreach (var word in wordsWithUniqueLetters)
            //{
            //    Console.WriteLine(word);
            //}
            //Console.ReadKey();
        }
    }
}