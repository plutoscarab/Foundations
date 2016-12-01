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
            var g = new Generator();
            List<int> x, y;
            double nk = 0;
            int n2 = 0, n3 = 0;
            var v1 = Enumerable.Range(0, 64).Select(i => 1UL << (63 - i)).ToArray();
            var s1 = new Sobol(v1);
            var g1 = new UniformGenerator(s1);
            //for (int i = 0; i < 255; i++) s1.Next();
            x = g1.Int32s(16).Take(256).ToList();
            var vdc = x;

            while (true)
            {
                while (true)
                {
                    n2++;
                    var v2 = MakeDirectionVectors(g);
                    var s2 = new Sobol(v2);
                    var g2 = new UniformGenerator(s2);
                    //s2.Skip(255);
                    x = vdc;
                    y = g2.Int32s(16).Take(256).ToList();
                    var h = new int[256];
                    int m = 0;

                    for (int i = 0; i < 256; i++)
                    {
                        m = Math.Max(m, ++h[x[i] + 16 * y[i]]);
                    }

                    if (m == 1) break;
                }

                while (true)
                {
                    n3++;
                    var v3 = MakeDirectionVectors(g);
                    var s3 = new Sobol(v3);
                    var g3 = new UniformGenerator(s3);
                    //s3.Skip(255);
                    var save = y;
                    y = g3.Int32s(16).Take(256).ToList();
                    var h = new int[256];
                    int m = 0;

                    for (int i = 0; i < 256; i++)
                    {
                        m = Math.Max(m, ++h[x[i] + 16 * y[i]]);
                    }

                    if (m != 1) continue;
                    x = save;
                    h = new int[256];
                    m = 0;

                    for (int i = 0; i < 256; i++)
                    {
                        m = Math.Max(m, ++h[x[i] + 16 * y[i]]);
                    }
                    if (m == 1) break;
                }

                nk++;
                Trace.Write($"{n2 / nk:N3}\t{n3 / nk:N3}\t{n3 / (double)n2:N3} \r");
            }
        }

        private static ulong[] MakeDirectionVectors(Generator g)
        {
            var v = g.CreateUInt64s(64);
            for (int i = 0; i < v.Length; i++) v[i] = (v[i] | 1UL) << (63 - i);
            return v;
        }
    }
}
