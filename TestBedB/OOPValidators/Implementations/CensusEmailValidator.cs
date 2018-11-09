using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TestBedB.Models;
using TestBedB.OOPValidators.Interfaces;

namespace TestBedB.OOPValidators.Implementations
{
    public class CensusEmailValidator : ICensusValidator
    {
        private bool _invalid;

        public bool IsValid(Census census)
        {
            const string domainRegexp = "(@)(.+)$";
            const string emailRegxp =
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[\w])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                census.Email = Regex.Replace(
                    census.Email,
                    domainRegexp,
                    IdnMapper,
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (_invalid)
            {
                return false;
            }

            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(
                    census.Email,
                    emailRegxp,
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string IdnMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
