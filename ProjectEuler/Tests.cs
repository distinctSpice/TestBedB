using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// Problem 1
        /// </summary>
        [TestMethod]
        public void MultiplesOf3And5()
        {
            const int expectedOutput = 233168;
            var output = Enumerable.Range(1, 999).Where(d => (d % 3 == 0) || (d % 5 == 0)).Sum(d => d);

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 2
        /// </summary>
        [TestMethod]
        public void EvenFibonacci()
        {
            const uint limit = 4000000000;
            var numbers = new List<uint>();
            var evenFibonacci = GenerateFibonacci(numbers, limit);

            //Console.WriteLine(evenFibonacci.Sum());
        }

        private List<uint> GenerateFibonacci(List<uint> numbers, uint limit)
        {
            var length = numbers?.Count ?? 0;

            if (numbers.Last() >= limit)
                return numbers;
            else
            {
                if (length == 1)
                    numbers.Add(2);
                else
                    numbers.Add(numbers[length - 1] + numbers[length - 2]);
                return GenerateFibonacci(numbers, limit);
            }
        }
    }
}
