using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectEuler
{
    public static class Utils
    {
        public static string[] ReadResource(string resourceName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.RESOURCEPATH, resourceName);

            if (File.Exists(path))
            {
                return File.ReadAllLines(path);
            }
            return null;
        }
    }

    public static class Constants
    {
        public const string RESOURCEPATH = "Resources";

        public const string PROBLEM22RESOURCE = "p022_names.txt";
    }
}
