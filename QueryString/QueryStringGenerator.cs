using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace QueryStringProcessing
{
    /// <summary>
    /// This class allows you to generate query strings from objects.
    /// </summary>
    public class QueryStringGenerator<TObject> where TObject : BaseModel
    {
        private QueryStringGeneratorOptions _options;
        private TObject _source;
        private Inflector.Inflector _inflector;

        /// <summary>
        /// Uses default options.
        /// </summary>
        /// <param name="source"></param>
        public QueryStringGenerator(TObject source)
        {
            _source = source;
            _options = new QueryStringGeneratorOptions();

            if (_options.SingularizeProperties)
                SetupInflector();
        }

        private void SetupInflector()
        {
            Inflector.Inflector.SetDefaultCultureFunc = () => Thread.CurrentThread.CurrentUICulture;
            _inflector = new Inflector.Inflector(new CultureInfo("en"));
        }

        /// <summary>
        /// Customized options.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="options"></param>
        public QueryStringGenerator(TObject obj, QueryStringGeneratorOptions options)
        {
            _source = obj;
            _options = options;

            if (_options.SingularizeProperties)
                SetupInflector();
        }

        /// <summary>
        /// Generates query string from complex objects.
        /// </summary>
        /// <returns>Query string</returns>
        public string Generate()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            MapObjectToKeyValuePair(keyValuePairs, null, _source);

            // Prevents duplicate parameters from being created
            keyValuePairs = keyValuePairs.Distinct().ToList();

            var queryKeysAndParameters = string.Join("&", keyValuePairs.Select(p => $"{Uri.EscapeDataString(FormatQueryKey(p.Key).ToLower())}={Uri.EscapeDataString(p.Value)}"));

            return !string.IsNullOrEmpty(queryKeysAndParameters)
                ? "?" + queryKeysAndParameters
                : string.Empty;
        }

        /// <summary>
        /// This will check each property type and will attempt to create a key value pair if the
        /// value is convertible to string.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void CreateKeyValuePairFromPropertyInfo<T>(List<KeyValuePair<string, string>> keyValuePairs, string key, T value)
        {
            var valueType = value.GetType();

            if (valueType.IsPrimitive || valueType == typeof(String))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>(key, value.ToString()));
            }
            else if (valueType == typeof(DateTime))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>(key, string.Format(_options.InterpolatedDateTimeFormat, value)));
            }
            else if (Guid.TryParse(value.ToString(), out var guidOutput))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>(key, guidOutput.ToString()));
            }
            else if (value is IEnumerable enumerable)
            {
                var count = 0;
                foreach (object o in enumerable)
                {
                    CreateKeyValuePairFromPropertyInfo(keyValuePairs, $"{key}[{0}]", o);
                    count++;
                }
            }
            else
            {
                MapObjectToKeyValuePair(keyValuePairs, valueType.Name, value);
            }
        }

        /// <summary>
        /// Prepares query key parameter.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string FormatQueryKey(string key)
        {
            if (_options.SingularizeProperties)
                key = Singularize(key);

            if (_options.SplitAndJoinPropertyNames)
            {
                var separatedKeys = SplitStringByUppercase(key);
                key = JoinSeparatedKeys(separatedKeys);
            }

            return key;
        }

        /// <summary>
        /// Combines strings with a separator.
        /// </summary>
        /// <param name="separatedKeys"></param>
        /// <returns></returns>
        private string JoinSeparatedKeys(string[] separatedKeys)
        {
            return string.Join(_options.PropertyNameSeparator, separatedKeys);
        }

        /// <summary>
        /// Deep maps objects to key value pair.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="source"></param>
        private void MapObjectToKeyValuePair<T>(List<KeyValuePair<string, string>> keyValuePairs, string key, T source)
        {
            var type = source.GetType();
            foreach (var property in type.GetProperties())
            {
                key = !string.IsNullOrEmpty(key) ? $"{key}.{property.Name}" : property.Name;
                object value = property.GetValue(source);

                if (value == null)
                    continue;

                CreateKeyValuePairFromPropertyInfo(keyValuePairs, key, value);
            }
        }

        /// <summary>
        /// Singularize property names using Inflector class.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private string Singularize(string word)
        {
            return _inflector.Singularize(word);
        }

        /// <summary>
        /// Splits string by upper case.
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        private string[] SplitStringByUppercase(string source)
        {
            return Regex.Split(source, "(?<!^)(?=[A-Z])");
        }
    }

    /// <summary>
    /// Options used generating query strings from objects.
    /// </summary>
    public class QueryStringGeneratorOptions
    {
        /// <summary>
        /// Default format is yyyy-MM-ddTHH:mm:sszzz.
        /// </summary>
        public string DateTimeFormat { get; set; } = "yyyy-MM-ddTHH:mm:sszzz";

        /// <summary>
        /// Returns interpolated datetime format that is ready for string formatting
        /// </summary>
        public string InterpolatedDateTimeFormat { get { return "{0:" + DateTimeFormat + "}"; } }

        /// <summary>
        /// Used in combining split property names. Default separator is underscore.
        /// </summary>
        public string PropertyNameSeparator { get; set; } = "_";

        /// <summary>
        /// Splits a property name along capitalized characters and joins them with a separator. Default value is true.
        /// </summary>
        public bool SplitAndJoinPropertyNames { get; set; } = true;

        /// <summary>
        /// Singularize property names. Default value is true.
        /// </summary>
        public bool SingularizeProperties { get; set; } = true;
    }
}