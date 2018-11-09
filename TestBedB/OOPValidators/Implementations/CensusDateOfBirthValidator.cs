using System;
using TestBedB.Models;
using TestBedB.OOPValidators.Interfaces;

namespace TestBedB.OOPValidators.Implementations
{
    public class CensusDateOfBirthValidator : ICensusValidator
    {
        public bool IsValid(Census census)
        {
            return Convert.ToDateTime(census.DateOfBirth) < DateTime.UtcNow;
        }
    }
}
