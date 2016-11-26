using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundations.RandomNumbers;

namespace PerformanceHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener());
            var t = new SobolInitialValuesTests();
            t.SobolInitialValues(20000);
        }
    }
}
