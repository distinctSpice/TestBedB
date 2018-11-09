using System;
using TestBedB.Models;
using TestBedB.OOPValidators.Interfaces;

namespace TestBedB.OOPValidators.Implementations
{
    public class CensusEffectiveDateValidator : ICensusValidator
    {
        public bool IsValid(Census census)
        {
            if (string.IsNullOrEmpty(census.EffectiveDate) && string.IsNullOrEmpty(census.DeletionDate))
                return false;

            return Convert.ToDateTime(census.EffectiveDate) < Convert.ToDateTime(census.DeletionDate);
        }
    }
}
