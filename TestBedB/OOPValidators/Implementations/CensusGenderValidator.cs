using System;
using TestBedB.Enums;
using TestBedB.Models;
using TestBedB.OOPValidators.Interfaces;

namespace TestBedB.OOPValidators.Implementations
{
    public class CensusGenderValidator : ICensusValidator
    {
        public bool IsValid(Census census)
        {
            int gender = 0;
            foreach (var key in Enum.GetNames(typeof(Gender)))
            {
                if (string.Equals(key, census.Gender, StringComparison.CurrentCultureIgnoreCase))
                {
                    gender = (int)Enum.Parse(typeof(Gender), key);
                    break;
                }
            }
            return gender != 0;
        }
    }
}
