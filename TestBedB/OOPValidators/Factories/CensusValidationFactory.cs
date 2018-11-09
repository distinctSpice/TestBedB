using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestBedB.Models;
using TestBedB.OOPValidators.Interfaces;

namespace TestBedB.OOPValidators.Factories
{
    public class CensusValidationFactory
    {
        private readonly List<ICensusValidator> _validatorInstances = new List<ICensusValidator>();

        public CensusValidationFactory()
        {
            // Get all validators and add them into the validators list
            var validatorClasses = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsInterface
                    && !t.IsAbstract
                    && t.Namespace == "TestBedB.OOPValidators.Implementations"
                    && typeof(ICensusValidator).IsAssignableFrom(t))
                .ToArray();

            _validatorInstances = validatorClasses
                .Select(t => (ICensusValidator)Activator.CreateInstance(t))
                .ToList();
        }

        public bool Validate(Census census)
        {
            bool isCensusValid = false;

            foreach (var validator in _validatorInstances)
            {
                if (isCensusValid)
                    isCensusValid = validator.IsValid(census);
            }

            return isCensusValid;
        }
    }
}
