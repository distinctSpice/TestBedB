using CensusProcessing.Enums;
using CensusProcessing.Factories;
using CensusProcessing.Models;
using QueryStringProcessing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigPropertyMapping;
using System.Linq;
using Derangement;

namespace TestBedConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ////var result = ProcessTestCensus().GetAwaiter().GetResult();

            ////Console.WriteLine("Result successful? {0}", result.IsSuccess);
            ////Console.WriteLine("Message: {0} ", result.Message);
            ////if (result.Exception != null)
            ////{
            ////    Console.WriteLine("Exception: {0} ", result.Exception.Message);
            ////}

            ////Console.ReadKey();

            ////Console.WriteLine(GenerateQueryString());
            ////Console.ReadKey();

            //const int nth = 10000;

            //Console.WriteLine(string.Join(Environment.NewLine, GenerateFibonacci(null, nth)));
            //Console.ReadKey();

            ////Console.WriteLine(ConfigMapper("Candles"));
            ////Console.ReadKey();

            //Console.WriteLine(new HexColors.Encoder(255, 99, 71).Encode());

            //Console.WriteLine(new HexColors.Blender("#000000", "#778899").Blend());

            //Console.WriteLine(new HexColors.Blender("#E6E6FA", "#FF69B4", "#B0C4DE").Blend());

            ////const string control = "https://marinebenefits.sharepoint.com/sites/G2-Provider-P26/Shared%20Documents/JUL%20%2718/18CL-387298PHM/18CL-387298PHM.xlsx?d=wa365881618794d42a24283da2aa0d473&csf=1&e=etmizN";
            ////const string experiment = "https://marinebenefits.sharepoint.com";

            ////Console.WriteLine(ValidateFileUrl(control, experiment));

            ////Console.ReadKey();

            //Console.WriteLine(IsFibonacci(2788724563990792802));
            //Console.ReadKey();

            //var santa = new Santa();                                            // Init Santa
            //var childrenList = santa.MakeList().ToList();                       // He's making a list
            //bool @checked = false;
            //for (int i = 0; i < 2; i++)
            //{
            //    @checked = childrenList.Check();                                // He's checking it twice
            //}
            //if (@checked)
            //{
            //    var niceKids = childrenList.Where(child => child.IsNice);   // Gonna find out who's naughty and nice
            //    santa.ComeToTown(niceKids);                                     // Santa Claus is coming to town
            //}

            var computer = new DerangementComputer();
            Console.WriteLine(computer.GetDerangement(5));
            Console.WriteLine(string.Join(Environment.NewLine, computer.GetDerangements(new List<ulong> { 6, 9, 14 })));
            Console.WriteLine(string.Join(Environment.NewLine, computer.GetDerangements(Enumerable.Range(0, 100).Select(n => (ulong)n))));

            Console.ReadKey();
        }

        public static string ConfigMapper(string type)
        {
            var mapper = new Mapper();
            return mapper.GetRequestTypeEndpoint(type);
        }

        private static List<ulong> GenerateFibonacci(List<ulong> numbers, int limit)
        {
            var length = numbers?.Count ?? 0;

            if (length == limit)
            {
                return numbers;
            }
            else
            {
                if (length == 0)
                {
                    numbers = new List<ulong> { 0 };
                }
                else if (length == 1)
                {
                    numbers.Add(1);
                }
                else
                {
                    numbers.Add(numbers[length - 1] + numbers[length - 2]);
                }
                return GenerateFibonacci(numbers, limit);
            }
        }

        private static bool IsFibonacci(ulong number)
        {
            //Uses a closed form solution for the fibonacci number calculation.
            //http://en.wikipedia.org/wiki/Fibonacci_number#Closed-form_expression

            double fi = (1 + Math.Sqrt(5)) / 2.0; //Golden ratio
            ulong n = (ulong)Math.Floor(Math.Log((number * Math.Sqrt(5)) + 0.5, fi)); //Find's the index (n) of the given number in the fibonacci sequence

            ulong actualFibonacciNumber = (ulong)Math.Floor((Math.Pow(fi, n) / Math.Sqrt(5)) + 0.5); //Finds the actual number corresponding to given index (n)

            return actualFibonacciNumber == number;
        }

        public static string GenerateQueryString()
        {
            var model = new TestModel
            {
                StringProperty = "String test",
                DateTimeProperty = DateTime.UtcNow,
                ListStringProperties = new List<string>
                {
                    "List string test 1",
                    "List string test 2"
                },
                People = new List<string>
                {
                    "Person 1",
                    "Person 2"
                },
                TestModel2 = new TestModel2
                {
                    IntProperty = 10,
                    TestModel3 = new TestModel3
                    {
                        GuidProperty = Guid.NewGuid()
                    }
                }
            };

            var utcNow = DateTime.UtcNow;

            var generator = new QueryStringGenerator<TestModel>(model);
            return generator.Generate();
        }

        public static async Task<Result> ProcessTestCensus()
        {
            var testCensus = new Census()
            {
                EffectiveDate = "11/8/2018",
                DeletionDate = "11/8/2019",
                DateOfBirth = "10/15/1992",
                Gender = "Male",
                Email = "mail.@mail.com",
                ProcessType = ProcessType.S
            };

            var factory = new CensusProcessorFactory();

            return await factory
                .ProcessAsync(testCensus)
                .ConfigureAwait(false);
        }

        private static bool ValidateFileUrl(string _url, string _tenantRootUrl)
        {
            string FileName;
            if (_url.StartsWith(_tenantRootUrl) && !_url.Contains(":s:") && !_url.Contains(":x:"))
            {
                var fullurlArray = _url.Split("/");
                var lastSplit = fullurlArray[fullurlArray.Length - 1];

                if (lastSplit.Contains("."))
                {
                    // old style url
                    var temp = _url.Replace(_tenantRootUrl, "");
                    temp = temp.TrimStart('/');
                    var tempArray = temp.Split("/");
                    // 0 = :x:
                    // 1 = r or s
                    // 2 = site (managed path) 0 
                    // 3 = provider site name 1
                    // 4 = drive name 2
                    // 5 -> folder and file name 3
                    var filePath = "/";
                    var lastPart = tempArray[tempArray.Length - 1];
                    if (lastPart.Contains("?"))
                    {
                        tempArray[tempArray.Length - 1] = tempArray[tempArray.Length - 1].Split("?")[0];
                    }

                    for (var i = 3; i < tempArray.Length; i++)
                    {
                        if (!filePath.Equals("."))
                        {
                            filePath += tempArray[i] + "/";
                        }
                        else
                        {
                            break;
                        }
                        if (i == tempArray.Length - 1)
                        {
                            FileName = tempArray[i];
                        }

                    }
                    if (tempArray.Length > 4)
                    {
                        return true;
                        //RelativePathToSite = "/" + tempArray[0] + "/" + tempArray[1];
                        //DriveRealtiveName = tempArray[2];
                        //IsValid = true;
                        //RelativePathToFile = filePath.TrimEnd('/');
                        //RelativeFolder = RelativePathToFile.Replace(FileName, "");
                    }
                    return false;
                }
            }
            return false;
        }
    }

    public class Santa
    {
        public IEnumerable<Child> MakeList()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            return Enumerable
                .Range(5, rand.Next(6, 10))
                .Select(_ => new Child
                {
                    Name = new string(Enumerable
                        .Repeat(chars, 5)
                        .Select(s => s[rand.Next(s.Length)])
                        .ToArray())
                });
        }
    }

    public class Child
    {
        public bool IsNice => new Random().NextDouble() >= 0.5;

        public string Name { get; set; }
    }

    public static class Extensions
    {
        public static bool Check(this IEnumerable<Child> children)
        {
            return true;
        }

        public static void ComeToTown(this Santa santa, IEnumerable<Child> children)
        {
            foreach (var child in children)
            {
                Console.WriteLine("Gift delivered to " + child.Name);
            }
        }
    }
}