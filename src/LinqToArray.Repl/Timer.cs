using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToArray.Repl
{
    public static class Timer
    {
        public static IEnumerable<int> GetExecutionTimes<T>(this Func<T> func, int repeats)
        {
            Action action = () =>
            {
                T value = func();
            };

            return action.GetExecutionTimes(repeats);
        }

        public static double GetAverageExecutionTime<T>(this Func<T> func, int repeats)
        {
            Action action = () =>
            {
                T value = func();
            };

            return action.GetAverageExecutionTime(repeats);
        }


        public static IEnumerable<int> GetExecutionTimes(this Action action, int repeats)
        {
            for (var i = 0; i < repeats; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                action();
                sw.Stop();

                yield return (int)sw.ElapsedMilliseconds;
            }
        }

        public static double GetAverageExecutionTime(this Action action, int repeats)
        {
            return action.GetExecutionTimes(repeats).Average();
        }
    }
}
