using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace OpenPager
{
    public static class Profiler
    {
        private static readonly ConcurrentDictionary<string, Stopwatch> Watches = new ConcurrentDictionary<string, Stopwatch>();

        private static readonly ConcurrentQueue<string> Results = new ConcurrentQueue<string>();
        
        public static void Start(string tag)
        {
            Console.WriteLine("Starting Stopwatch {0}", tag);

            var watch = Watches[tag] = new Stopwatch();
            watch.Start();
        }

        public static void Stop(string tag)
        {
            if (!Watches.TryGetValue(tag, out var watch)) return;

            var result = $"Stopwatch {tag} took {watch.Elapsed}";
            Results.Enqueue(result);
            Console.WriteLine(result);
        }

        public static string Result()
        {
            var sb = new StringBuilder();
            foreach (var result in Results)
            {
                sb.AppendLine(result);
            }

            Console.WriteLine("All Profiler results:");
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }
    }
}
