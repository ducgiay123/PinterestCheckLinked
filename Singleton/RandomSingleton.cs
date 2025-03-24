using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PinterestCheckLinked.Singleton
{
    public static class RandomSingleton
    {
        private static int seed = Environment.TickCount;
        public static ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref RandomSingleton.seed)));
        public static string RandomItemInList(this List<string> list)
        {
            return list.Count > 0 ? list[random.Value.Next(list.Count)] : string.Empty;
        }
    }
}
