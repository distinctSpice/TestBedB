using MathNet.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            const Int64 expectedOutput = 4613732;
            const Int64 limit = 4000000;
            var numbers = new List<Int64>();
            var evenFibonacci = GenerateFibonacci(numbers, limit);

            var evenNumbers = evenFibonacci.Where(e => e % 2 == 0 && e < limit);
            var output = evenNumbers.Sum();

            Assert.AreEqual(output, expectedOutput);
        }

        private static List<Int64> GenerateFibonacci(List<Int64> numbers, Int64 limit)
        {
            var length = numbers?.Count ?? 0;

            if (length > 0 && numbers.Last() >= limit)
            {
                return numbers;
            }
            else
            {
                if (length == 0)
                {
                    numbers = new List<Int64> { 1 };
                }
                else if (length == 1)
                {
                    numbers.Add(2);
                }
                else
                {
                    numbers.Add(numbers[length - 1] + numbers[length - 2]);
                }
                return GenerateFibonacci(numbers, limit);
            }
        }

        /// <summary>
        /// Problem 3
        /// </summary>
        [TestMethod]
        public void LargestPrimeFactor()
        {
            const long expectedOutput = 6857;
            long input = 600851475143;
            long prime = 2;

            while (input >= prime * prime)
            {
                if (input % prime == 0)
                    input /= prime;
                else
                    prime++;
            }

            var output = input; // The remaining input

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 4
        /// </summary>
        [TestMethod]
        public void LargestPalindromeProduct()
        {
            const ulong expectedOutput = 906609;
            ulong max = 0;

            for (ulong i = 100; i < 999; i++)
                for (ulong j = 100; j < 999; j++)
                {
                    var r = i * j;
                    if (IsPalindrome(r) && r > max)
                        max = r;
                }

            var output = max;

            Assert.AreEqual(output, expectedOutput);
        }

        private bool IsPalindrome(ulong number)
        {
            return number.ToString() == string.Concat(number.ToString().Reverse());
        }

        /// <summary>
        /// Problem 5
        /// </summary>
        [TestMethod]
        public void SmallestMultiple()
        {
            const int expectedOutput = 232792560;

            // Brute force
            //var foundIt = false;
            //var counter = 1;

            //while (!foundIt)
            //{
            //    if (Enumerable.Range(1, 20).Any(r => counter % r != 0))
            //        counter++;
            //    else
            //        foundIt = true;
            //}

            //var output = counter;

            //Assert.AreEqual(output, expectedOutput);

            // LCM method
            var result = 1;
            for (int i = 2; i < 20; i++)
            {
                result = GetLcm(result, i);
            }

            var output = result;

            Assert.AreEqual(output, expectedOutput);
        }

        private int GetGcd(int a, int b)
        {
            while (b != 0)
            {
                a %= b;
                a ^= b;
                b ^= a;
                a ^= b;
            }

            return a;
        }

        private int GetLcm(int a, int b)
        {
            return a / GetGcd(a, b) * b;
        }

        /// <summary>
        /// Problem 6
        /// </summary>
        [TestMethod]
        public void SumSquareDifference()
        {
            const int expectedOutput = 25164150;
            //var firstHundred = Enumerable.Range(1, 100);

            //var sumOfSquares = firstHundred.Select(d => d * d).Sum();
            //var squareOfSum = Math.Pow(firstHundred.Sum(), 2);

            //var output = Math.Abs(sumOfSquares - squareOfSum);

            var output = Math.Abs(GaussSumOfSquaresOfFirstNatNumbers(100) - Math.Pow(GaussSumOfNatNumbers(100), 2));
            Assert.AreEqual(output, expectedOutput);
        }

        private double GaussSumOfNatNumbers(double n)
        {
            return n * ((n + 1) / 2);
        }

        private double GaussSumOfSquaresOfFirstNatNumbers(double n)
        {
            return (n * (n + 1) * ((2 * n) + 1)) / 6;
        }

        /// <summary>
        /// Problem 7
        /// </summary>
        [TestMethod]
        public void NthPrime()
        {
            const int expectedOutput = 104743;
            var output = FindNthPrime(10001);

            Assert.AreEqual(output, expectedOutput);
        }

        private int FindNthPrime(int n)
        {
            if (n < 2) return 2;
            if (n == 2) return 3;
            if (n == 3) return 5;
            int limit, root, count = 2;
            limit = (int)(n * (Math.Log(n) + Math.Log(Math.Log(n)))) + 3;
            root = (int)Math.Sqrt(limit);
            switch (limit % 6)
            {
                case 0:
                    limit = 2 * (limit / 6) - 1;
                    break;

                case 5:
                    limit = 2 * (limit / 6) + 1;
                    break;

                default:
                    limit = 2 * (limit / 6);
                    break;
            }
            switch (root % 6)
            {
                case 0:
                    root = 2 * (root / 6) - 1;
                    break;

                case 5:
                    root = 2 * (root / 6) + 1;
                    break;

                default:
                    root = 2 * (root / 6);
                    break;
            }
            int dim = (limit + 31) >> 5;
            int[] sieve = new int[dim];
            for (int j = 0; j < root; ++j)
            {
                if ((sieve[j >> 5] & (1 << (j & 31))) == 0)
                {
                    int start, s1, s2;
                    if ((j & 1) == 1)
                    {
                        start = j * (3 * j + 8) + 4;
                        s1 = 4 * j + 5;
                        s2 = 2 * j + 3;
                    }
                    else
                    {
                        start = j * (3 * j + 10) + 7;
                        s1 = 2 * j + 3;
                        s2 = 4 * j + 7;
                    }
                    for (int k = start; k < limit; k += s2)
                    {
                        sieve[k >> 5] |= 1 << (k & 31);
                        k += s1;
                        if (k >= limit) break;
                        sieve[k >> 5] |= 1 << (k & 31);
                    }
                }
            }
            int i;
            for (i = 0; count < n; ++i)
            {
                count += GetPopCount(~sieve[i]);
            }
            --i;
            int mask = ~sieve[i];
            int p;
            for (p = 31; count >= n; --p)
            {
                count -= (mask >> p) & 1;
            }
            return 3 * (p + (i << 5)) + 7 + (p & 1);
        }

        private int GetPopCount(int n)
        {
            n -= (n >> 1) & 0x55555555;
            n = ((n >> 2) & 0x33333333) + (n & 0x33333333);
            n = ((n >> 4) & 0x0F0F0F0F) + (n & 0x0F0F0F0F);
            return (n * 0x01010101) >> 24;
        }

        /// <summary>
        /// Problem 8
        /// </summary>
        [TestMethod]
        public void LargestProductInASeries()
        {
            const long expectedOutput = 23514624000;
            const string series = "73167176531330624919225119674426574742355349194934" +
                                  "96983520312774506326239578318016984801869478851843" +
                                  "85861560789112949495459501737958331952853208805511" +
                                  "12540698747158523863050715693290963295227443043557" +
                                  "66896648950445244523161731856403098711121722383113" +
                                  "62229893423380308135336276614282806444486645238749" +
                                  "30358907296290491560440772390713810515859307960866" +
                                  "70172427121883998797908792274921901699720888093776" +
                                  "65727333001053367881220235421809751254540594752243" +
                                  "52584907711670556013604839586446706324415722155397" +
                                  "53697817977846174064955149290862569321978468622482" +
                                  "83972241375657056057490261407972968652414535100474" +
                                  "82166370484403199890008895243450658541227588666881" +
                                  "16427171479924442928230863465674813919123162824586" +
                                  "17866458359124566529476545682848912883142607690042" +
                                  "24219022671055626321111109370544217506941658960408" +
                                  "07198403850962455444362981230987879927244284909188" +
                                  "84580156166097919133875499200524063689912560717606" +
                                  "05886116467109405077541002256983155200055935729725" +
                                  "71636269561882670428252483600823257530420752963450";

            long max = 0;

            for (int i = 0; i < series.Length; i++)
            {
                if (i + 13 >= series.Length) break;

                var integers = series.Substring(i, 13).Select(s => (long)char.GetNumericValue(s));
                if (integers.Any(c => c == 0)) continue;
                max = Math.Max(integers.Aggregate((total, next) => total * next), max);
            }

            var output = max;
            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 9
        /// </summary>
        [TestMethod]
        public void PythagoreanTriples()
        {
            const int expectedOutput = 31875000;
            const int sum = 1000;

            var triplet = FindPythagorenTripletForSum(sum);

            Assert.IsNotNull(triplet);

            var output = triplet.Item1 * triplet.Item2 * triplet.Item3;

            Assert.AreEqual(output, expectedOutput);
        }

        private Tuple<int, int, int> FindPythagorenTripletForSum(int sum)
        {
            for (int a = 1; a < sum / 3; a++)
            {
                for (int b = 1; b < sum / 2; b++)
                {
                    var c = sum - a - b;
                    if ((a * a) + (b * b) == c * c)
                    {
                        return new Tuple<int, int, int>(a, b, c);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Problem 10
        /// </summary>
        [TestMethod]
        public void SummationOfPrimes()
        {
            const ulong expectedOutput = 142913828922;
            const ulong n = 2000000;
            var primes = FindPrimesBySieveOfAtkin(n);

            var output = primes.Aggregate((sum, last) => sum += last);
            Assert.AreEqual(output, expectedOutput);
        }

        private List<ulong> FindPrimesBySieveOfAtkin(ulong limit)
        {
            var primes = new List<ulong>();
            var isPrime = new bool[limit + 1];
            var sqrt = Math.Sqrt(limit);

            for (ulong x = 1; x <= sqrt; x++)
                for (ulong y = 1; y <= sqrt; y++)
                {
                    var n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * x * x + y * y;
                    if (n <= limit && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * x * x - y * y;
                    if (x > y && n <= limit && n % 12 == 11)
                        isPrime[n] ^= true;
                }

            for (ulong n = 5; n <= sqrt; n++)
                if (isPrime[n])
                {
                    var s = n * n;
                    for (ulong k = s; k <= limit; k += s)
                        isPrime[k] = false;
                }

            primes.Add(2);
            primes.Add(3);
            for (ulong n = 5; n <= limit; n += 2)
                if (isPrime[n])
                    primes.Add(n);

            return primes;
        }

        /// <summary>
        /// Problem 11
        /// </summary>
        [TestMethod]
        public void LargestProductInAGrid()
        {
            const int expectedOutput = 70600674;
            const int gridDimension = 20;
            const int range = 4;
            var directions = new Dictionary<string, int[]>
            {
                { "North West", new int[2] { -1, -1} },
                { "North East", new int[2] { 1, -1} },
                { "South West", new int[2] { -1, 1} },
                { "South East", new int[2] { 1, 1} }
            };

            var grid = new int[20, 20] {
                { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08 },
                { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00 },
                { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65 },
                { 52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91 },
                { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 },
                { 24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 },
                { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 },
                { 67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21 },
                { 24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 },
                { 21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95 },
                { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92 },
                { 16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57 },
                { 86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 },
                { 19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40 },
                { 04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 },
                { 88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 },
                { 04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36 },
                { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16 },
                { 20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54 },
                { 01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 }
            };

            var max = 0;

            for (int x = 0; x < gridDimension; x++)
                for (int y = 0; y < gridDimension; y++)
                    foreach (var direction in directions)
                    {
                        if (CheckOffset(x, direction.Value[0], range, gridDimension) && CheckOffset(y, direction.Value[1], range, gridDimension))
                            max = Math.Max(max, GetDiagonalProduct(grid, x, y, direction.Value[0], direction.Value[1], range));
                    }

            var output = max;
            Assert.AreEqual(output, expectedOutput);
        }

        private bool CheckOffset(int index, int offset, int range, int dimension)
        {
            return (offset > 0)
                ? index + range <= dimension
                : index - range > -1;
        }

        private int GetDiagonalProduct(int[,] grid, int x, int y, int xOffset, int yOffset, int range)
        {
            int result = 1;
            for (int i = 0; i < range; i++)
            {
                result *= grid[x + (i * xOffset), y + (i * yOffset)];
            }
            return result;
        }

        /// <summary>
        /// Problem 12
        /// </summary>
        [TestMethod]
        public void HighlyDivisibleTriangularNumber()
        {
            const int expectedOutput = 76576500;
            var counter = 1;
            var foundIt = false;
            var triangular = 0;

            try
            {
                while (!foundIt)
                {
                    triangular = Enumerable.Range(0, counter + 1).Sum();
                    foundIt = Factor(triangular).Count() > 500;
                    if (!foundIt)
                        counter++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            var output = triangular;

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 13
        /// </summary>
        [TestMethod]
        public void LargeSum()
        {
            const string expectedOutput = "5537376230";
            const string grid =
                "37107287533902102798797998220837590246510135740250" +
                "46376937677490009712648124896970078050417018260538" +
                "74324986199524741059474233309513058123726617309629" +
                "91942213363574161572522430563301811072406154908250" +
                "23067588207539346171171980310421047513778063246676" +
                "89261670696623633820136378418383684178734361726757" +
                "28112879812849979408065481931592621691275889832738" +
                "44274228917432520321923589422876796487670272189318" +
                "47451445736001306439091167216856844588711603153276" +
                "70386486105843025439939619828917593665686757934951" +
                "62176457141856560629502157223196586755079324193331" +
                "64906352462741904929101432445813822663347944758178" +
                "92575867718337217661963751590579239728245598838407" +
                "58203565325359399008402633568948830189458628227828" +
                "80181199384826282014278194139940567587151170094390" +
                "35398664372827112653829987240784473053190104293586" +
                "86515506006295864861532075273371959191420517255829" +
                "71693888707715466499115593487603532921714970056938" +
                "54370070576826684624621495650076471787294438377604" +
                "53282654108756828443191190634694037855217779295145" +
                "36123272525000296071075082563815656710885258350721" +
                "45876576172410976447339110607218265236877223636045" +
                "17423706905851860660448207621209813287860733969412" +
                "81142660418086830619328460811191061556940512689692" +
                "51934325451728388641918047049293215058642563049483" +
                "62467221648435076201727918039944693004732956340691" +
                "15732444386908125794514089057706229429197107928209" +
                "55037687525678773091862540744969844508330393682126" +
                "18336384825330154686196124348767681297534375946515" +
                "80386287592878490201521685554828717201219257766954" +
                "78182833757993103614740356856449095527097864797581" +
                "16726320100436897842553539920931837441497806860984" +
                "48403098129077791799088218795327364475675590848030" +
                "87086987551392711854517078544161852424320693150332" +
                "59959406895756536782107074926966537676326235447210" +
                "69793950679652694742597709739166693763042633987085" +
                "41052684708299085211399427365734116182760315001271" +
                "65378607361501080857009149939512557028198746004375" +
                "35829035317434717326932123578154982629742552737307" +
                "94953759765105305946966067683156574377167401875275" +
                "88902802571733229619176668713819931811048770190271" +
                "25267680276078003013678680992525463401061632866526" +
                "36270218540497705585629946580636237993140746255962" +
                "24074486908231174977792365466257246923322810917141" +
                "91430288197103288597806669760892938638285025333403" +
                "34413065578016127815921815005561868836468420090470" +
                "23053081172816430487623791969842487255036638784583" +
                "11487696932154902810424020138335124462181441773470" +
                "63783299490636259666498587618221225225512486764533" +
                "67720186971698544312419572409913959008952310058822" +
                "95548255300263520781532296796249481641953868218774" +
                "76085327132285723110424803456124867697064507995236" +
                "37774242535411291684276865538926205024910326572967" +
                "23701913275725675285653248258265463092207058596522" +
                "29798860272258331913126375147341994889534765745501" +
                "18495701454879288984856827726077713721403798879715" +
                "38298203783031473527721580348144513491373226651381" +
                "34829543829199918180278916522431027392251122869539" +
                "40957953066405232632538044100059654939159879593635" +
                "29746152185502371307642255121183693803580388584903" +
                "41698116222072977186158236678424689157993532961922" +
                "62467957194401269043877107275048102390895523597457" +
                "23189706772547915061505504953922979530901129967519" +
                "86188088225875314529584099251203829009407770775672" +
                "11306739708304724483816533873502340845647058077308" +
                "82959174767140363198008187129011875491310547126581" +
                "97623331044818386269515456334926366572897563400500" +
                "42846280183517070527831839425882145521227251250327" +
                "55121603546981200581762165212827652751691296897789" +
                "32238195734329339946437501907836945765883352399886" +
                "75506164965184775180738168837861091527357929701337" +
                "62177842752192623401942399639168044983993173312731" +
                "32924185707147349566916674687634660915035914677504" +
                "99518671430235219628894890102423325116913619626622" +
                "73267460800591547471830798392868535206946944540724" +
                "76841822524674417161514036427982273348055556214818" +
                "97142617910342598647204516893989422179826088076852" +
                "87783646182799346313767754307809363333018982642090" +
                "10848802521674670883215120185883543223812876952786" +
                "71329612474782464538636993009049310363619763878039" +
                "62184073572399794223406235393808339651327408011116" +
                "66627891981488087797941876876144230030984490851411" +
                "60661826293682836764744779239180335110989069790714" +
                "85786944089552990653640447425576083659976645795096" +
                "66024396409905389607120198219976047599490197230297" +
                "64913982680032973156037120041377903785566085089252" +
                "16730939319872750275468906903707539413042652315011" +
                "94809377245048795150954100921645863754710598436791" +
                "78639167021187492431995700641917969777599028300699" +
                "15368713711936614952811305876380278410754449733078" +
                "40789923115535562561142322423255033685442488917353" +
                "44889911501440648020369068063960672322193204149535" +
                "41503128880339536053299340368006977710650566631954" +
                "81234880673210146739058568557934581403627822703280" +
                "82616570773948327592232845941706525094512325230608" +
                "22918802058777319719839450180888072429661980811197" +
                "77158542502016545090413245809786882778948721859617" +
                "72107838435069186155435662884062257473692284509516" +
                "20849603980134001723930671666823555245252804609722" +
                "53503534226472524250874054075591789781264330331690";

            var digits = SplitInParts(grid, 50).Select(BigInteger.Parse).ToArray();
            BigInteger sum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sum += digits[i];
            }
            var output = sum.ToString().Substring(0, 10);

            Assert.AreEqual(output, expectedOutput);
        }

        private IEnumerable<String> SplitInParts(String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
            {
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
            }
        }

        /// <summary>
        /// Problem 14
        /// </summary>
        [TestMethod]
        public void LongestCollatzSequence()
        {
            const int expectedOutput = 837799;
            const int number = 1000000;

            int sequenceLength = 0;
            int startingNumber = 0;
            long sequence;

            int[] cache = new int[number + 1];
            //Initialise cache
            for (int i = 0; i < cache.Length; i++)
            {
                cache[i] = -1;
            }
            cache[1] = 1;

            for (int i = 2; i <= number; i++)
            {
                sequence = i;
                int k = 0;
                while (sequence != 1 && sequence >= i)
                {
                    k++;
                    if ((sequence % 2) == 0)
                    {
                        sequence /= 2;
                    }
                    else
                    {
                        sequence = (sequence * 3) + 1;
                    }
                }
                //Store result in cache
                cache[i] = k + cache[sequence];

                //Check if sequence is the best solution
                if (cache[i] > sequenceLength)
                {
                    sequenceLength = cache[i];
                    startingNumber = i;
                }
            }

            var output = startingNumber;

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 15
        /// </summary>
        [TestMethod]
        public void LatticePaths()
        {
            const double expectedOutput = 137846528820;
            const int grid = 20;

            // Central binomial coefficient
            var output = SpecialFunctions.Factorial(2 * grid) / Math.Pow(SpecialFunctions.Factorial(grid), 2);

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 16
        /// </summary>
        [TestMethod]
        public void PowerDigitSum()
        {
            const int expectedOutput = 1366;
            const int pow = 1000;
            BigInteger n = 2;

            var nPow = BigInteger.Pow(n, pow);
            var array = nPow.ToString().ToCharArray().Select(a => char.GetNumericValue(a));
            var output = array.Sum();

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 17
        /// </summary>
        [TestMethod]
        public void NumberLetterCounts()
        {
            const int expectedOutput = 21124;
            var dictionary = new Dictionary<int, string>
            {
                { 0, "ty" },
                { 1, "one" },
                { 2, "two"},
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
                { 10, "ten" },
                { 11, "eleven" },
                { 12, "twelve" },
                { 13, "thirteen" },
                { 15, "fifteen" },
                { 18, "eighteen" },
                { 20, "twenty" },
                { 30, "thirty" },
                { 40, "forty" },
                { 50, "fifty" },
                { 80, "eighty" },
            };
            const string and = "and";
            const string hundred = "hundred";
            const string thousand = "thousand";

            var listWords = new List<string>();

            for (int i = 1; i <= 1000; i++)
            {
                var digits = i.ToString().ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray();

                switch (digits.Length)
                {
                    case 1:
                        listWords.Add(dictionary[i]);
                        break;

                    case 2:
                        listWords.Add(TensToWords(digits, dictionary));
                        break;

                    case 3:
                        if (digits[1] == 0 && digits[2] == 0)
                        {
                            listWords.Add(dictionary[digits[0]] + hundred);
                        }
                        else
                        {
                            listWords.Add(dictionary[digits[0]] + hundred + and + TensToWords(new int[2] { digits[1], digits[2] }, dictionary));
                        }
                        break;

                    case 4:
                        listWords.Add(dictionary[digits[0]] + thousand);
                        break;
                }
            }
            var output = listWords.Select(c => c.Length).Sum();

            Assert.AreEqual(output, expectedOutput);
        }

        private string TensToWords(int[] digits, Dictionary<int, string> dictionary)
        {
            const string teen = "teen";
            const string ty = "ty";

            if (digits[0] == 0)
            {
                return dictionary[digits[1]];
            }
            else if (digits[0] == 1)
            {
                if (new int[6] { 0, 1, 2, 3, 5, 8 }.Contains(digits[1]))
                {
                    return dictionary[10 + digits[1]];
                }
                else
                {
                    return dictionary[digits[1]] + teen;
                }
            }
            else if (new int[5] { 2, 3, 4, 5, 8 }.Contains(digits[0]))
            {
                if (digits[1] > 0)
                {
                    return dictionary[digits[0] * 10] + dictionary[digits[1]];
                }
                else
                {
                    return dictionary[digits[0] * 10];
                }
            }
            else
            {
                var length = dictionary[digits[0]] + ty;
                return (digits[1] > 0) ? length + dictionary[digits[1]] : length;
            }
        }

        /// <summary>
        /// Problem 18
        /// </summary>
        [TestMethod]
        public void MaximumPathSumI()
        {
            const int expectedOutput = 1074;
            var data = new List<List<int>> {
                new List<int> { 75 },
                new List<int> { 95, 64 },
                new List<int> { 17, 47, 82 },
                new List<int> { 18, 35, 87, 10 },
                new List<int> { 20, 04, 82, 47, 65 },
                new List<int> { 19, 01, 23, 75, 03, 34 },
                new List<int> { 88, 02, 77, 73, 07, 63, 67 },
                new List<int> { 99, 65, 04, 28, 06, 16, 70, 92 },
                new List<int> { 41, 41, 26, 56, 83, 40, 80, 70, 33 },
                new List<int> { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 },
                new List<int> { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 },
                new List<int> { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 },
                new List<int> { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 },
                new List<int> { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 },
                new List<int> { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 }
            };

            var lines = data.Count;

            for (int i = lines - 2; i >= 0; i--)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    data[i][j] += Math.Max(data[i + 1][j], data[i + 1][j + 1]);
                }
            }

            var output = data[0][0];

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 19
        /// </summary>
        [TestMethod]
        public void CountingSundays()
        {
            const int expectedOutput = 171;
            var result = 0;
            for (int year = 1901; year <= 2000; year++)
                for (int month = 1; month <= 12; month++)
                    if (new DateTime(year, month, 1).DayOfWeek == DayOfWeek.Sunday)
                        result++;

            var output = result;

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 20
        /// </summary>
        [TestMethod]
        public void FactorialDigitSum()
        {
            const int expectedOutput = 648;

            BigInteger n = 100;
            var factorial = SpecialFunctions.Factorial(n);
            var sum = factorial.ToString().Sum(c => (int)char.GetNumericValue(c));

            var output = sum;

            Assert.AreEqual(output, expectedOutput);
        }

        /// <summary>
        /// Problem 21 https://www.mathblog.dk/project-euler-21-sum-of-amicable-pairs/
        /// </summary>
        [TestMethod]
        public void AmicableNumbers()
        {
            const int expectedOutput = 31626;
            const int limit = 10000;
            var sums = new int[limit];
            var amicables = new Dictionary<int, int>();

            for (int i = 1; i <= limit; i++)
            {
                var sumOfFactorsInitial = Factor(i).Except(new int[1] { i }).Sum();
                if (sumOfFactorsInitial > i && sumOfFactorsInitial <= limit)
                {
                    var sumOfFactorsNext = Factor(sumOfFactorsInitial).Except(new int[1] { sumOfFactorsInitial }).Sum();
                    if (sumOfFactorsNext == i)
                    {
                        amicables.Add(i, sumOfFactorsInitial);
                    }
                }
            }

            var output = amicables.Keys.Sum() + amicables.Values.Sum();

            Assert.AreEqual(output, expectedOutput);
        }

        private IEnumerable<int> Factor(int n)
        {
            var l = new List<int>();
            for (int i = 1; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != n / i)
                        yield return n / i;
                }
            }
        }

        /// <summary>
        /// Problem 22
        /// </summary>
        [TestMethod]
        public void NameScores()
        {
            const ulong expectedOutput = 871198282;
            var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

            var lines = Utils.ReadResource(Constants.PROBLEM22RESOURCE);
            var names = lines[0]
                .Split(',')
                .Select(name => name.Trim('"'))
                .OrderBy(name => name)
                .ToList();

            ulong nameScoreTotal = 0;

            for (int i = 0; i < names.Count; i++)
            {
                ulong nameScore = 0;
                for (int j = 0; j < names[i].Length; j++)
                {
                    nameScore += (ulong)alpha.IndexOf(names[i][j]) + 1;
                }
                nameScoreTotal += (nameScore * ((ulong)i + 1));
            }

            var output = nameScoreTotal;

            Assert.AreEqual(output, expectedOutput);
        }
    }
}