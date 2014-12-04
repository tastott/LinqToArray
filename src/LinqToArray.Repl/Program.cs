using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToArray;

namespace LinqToArray.Repl
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Enumerable.Range(0, 10000000).Select(i => i.ToString()).ToArray();
            var repeats = 100;

            var lastItem = new Dictionary<string, Action>
            {
                {"Reading last value by index", () =>
                    {
                        System.Diagnostics.Debug.WriteLine(array[array.Length - 1]);
                    }
                },
                {"Reading last value with Linq", () =>
                    {
                        System.Diagnostics.Debug.WriteLine(Enumerable.Reverse(array).First());
                    }
                },
                {"Reading last value with LinqToArray", () =>
                    {
                        System.Diagnostics.Debug.WriteLine(LinqArrayExtensions.Reverse(array).First());
                    }
                },

            };

            TimeActions(lastItem, repeats);


            var skip = new Dictionary<string, Action>
            {
                {"Skipping values by index", () =>
                    {
                        for (int i = 5000000; i < 5000100; i++) System.Diagnostics.Debug.WriteLine(array[i]);
                    }
                },
                {"Skipping values with Linq", () =>
                    {
                         foreach(var value in Enumerable.Skip(array,4999999).Take(100))
                            System.Diagnostics.Debug.WriteLine(value);
                    }
                },
                {"Skipping values with LinqToArray", () =>
                    {
                        foreach (var value in LinqArrayExtensions.Skip(array, 4999999).Take(100))
                            System.Diagnostics.Debug.WriteLine(value);
                    }
                },

            };

            TimeActions(skip, repeats);


            var reverseSkip = new Dictionary<string, Action>
            {
                {"Reversing then skipping values by index", () =>
                    {
                        for (int i = 5000099; i >= 5000000; i--) System.Diagnostics.Debug.WriteLine(array[i]);
                    }
                },
                {"Reversing then skipping values with Linq", () =>
                    {
                         foreach (var value in Enumerable.Reverse(array).Skip(4999900).Take(100))
                            System.Diagnostics.Debug.WriteLine(value);
                    }
                },
                {"Reversing then skipping values with LinqToArray", () =>
                    {
                        foreach (var value in LinqArrayExtensions.Reverse(array).Skip(4999900).Take(100))
                            System.Diagnostics.Debug.WriteLine(value);
                    }
                },

            };

            TimeActions(reverseSkip, repeats);

            //var reverseSkipTimeArray = Timer.GetAverageExecutionTime(() =>
            //{
            //    for (int i = 5000099; i >= 5000000; i--) System.Diagnostics.Debug.WriteLine(array[i]);
            //}, 100);
            //Console.WriteLine("Reading reverse skip values by index took an average of {0}ms over {1} repetitions", reverseSkipTimeArray, repeats);

            //var reverseSkipTimeLinq = Timer.GetAverageExecutionTime(() =>
            //{
            //    foreach (var value in array.Reverse().Skip(4999900).Take(100))
            //        System.Diagnostics.Debug.WriteLine(value);
            //}, 100);

            //Console.WriteLine("Reading reverse skip values with Linq took an average of {0}ms over {1} repetitions", reverseSkipTimeLinq, repeats);

            Console.ReadLine();
        }

        private static void TimeActions(IDictionary<string, Action> actions, int repeats)
        {
            foreach(var item in actions)
            {
                var averageTime = Timer.GetAverageExecutionTime(() =>
                {
                    item.Value();
                }, repeats);

                Console.WriteLine("{0} took an average of {1}ms over {2} repetitions", item.Key, averageTime, repeats);
            }
        }
    }
}
