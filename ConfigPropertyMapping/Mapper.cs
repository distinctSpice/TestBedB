using System;
using System.Linq;

namespace ConfigPropertyMapping
{
    public class Mapper
    {
        private Config _config = new Config();

        public string GetRequestTypeEndpoint(string type)
        {
            // This is done to attempt to make this a closed method

            // Get all properties of config
            const string defaultEndpointFormat = "Get{0}RateEndpoint";
            var configProperties = _config.GetType().GetProperties();
            var propertyName = configProperties.SingleOrDefault(p => p.Name.Equals($"Get{type}RateEndpoint"))?.Name;

            // Throw an exception if the property is null or empty
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"Oanda endpoint for type {type} is not found.");
            }

            return _config.GetType().GetProperty(propertyName).GetValue(_config, null).ToString();
        }
    }
}
