using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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
            var max = double.MinValue;
            var fullMax = double.MinValue;

            var tasks = Enumerable.Range(0, 10).Select(taskNum =>
            Task.Run(() =>
            {
                while (true)
                {
                    int s1 = g.Int32(1, 31) * (g.Boolean() ? 1 : -1);
                    int s2 = g.Int32(1, 31) * (g.Boolean() ? 1 : -1);
                    int s3 = g.Int32(1, 31) * (g.Boolean() ? 1 : -1);
                    int s4 = g.Int32(1, 31) * (g.Boolean() ? 1 : -1);
                    int s5 = g.Int32(1, 31) * (g.Boolean() ? 1 : -1);
                    uint sm = g.UInt32();
                    uint sa = g.UInt32();

                    //Func<uint, uint> mf = u =>
                    //{
                    //    if (s1 > 0) u += u << s1; else u += u >> (-s1);
                    //    if (s2 > 0) u += u << s2; else u += u >> (-s2);
                    //    if (s3 > 0) u += u << s3; else u += u >> (-s3);
                    //    if (s4 > 0) u += u << s4; else u += u >> (-s4);
                    //    if (s5 > 0) u += u << s5; else u += u >> (-s5);
                    //    return u;// * sm + sa;
                    //};

                    var p = Expression.Parameter(typeof(uint), "u");
                    var e1 = MakeExpr(p, g);
                    var e2 = Expression.MultiplyAssign(p, Expression.Constant(g.UInt32() | 1U));

                    var expr = Expression.Block(
                        e1,
                        e2,
                        e1,
                        e2,
                        e1,
                        p
                    );

                    var s = MakeStr(expr);
                    var mf = Expression.Lambda<Func<uint, uint>>(expr, p).Compile();

                    //mf = Foundations.Functions.MixingFunctions.CreateUInt32Mixer(g);

                    var h = new int[32 * 32];

                    for (int i = 0; i < 1000; i++)
                    {
                        var u = g.UInt32();
                        var v = mf(u);

                        for (int j = 0; j < 32; j++)
                        {
                            var w = v ^ mf(u ^ (1U << j));

                            for (int k = 0; k < 32; k++)
                            {
                                if ((w & 1) != 0) h[k + 32 * j]++;
                                w >>= 1;
                            }
                        }
                    }

                    var min = h.Min() / (double)h.Max();
                    
                    while (min > max)
                    {
                        Interlocked.CompareExchange(ref max, min, max);
                    }

                    if (min > 0.845)
                    {
                        Console.WriteLine($"{s}, {min}");

                        Task.Run(() =>
                        {
                            var fg = new Generator();
                            var fh = new int[32 * 32];

                            for (int i = 0; i < 1000000; i++)
                            {
                                var u = fg.UInt32();
                                var v = mf(u);

                                for (int j = 0; j < 32; j++)
                                {
                                    var w = v ^ mf(u ^ (1U << j));

                                    for (int k = 0; k < 32; k++)
                                    {
                                        if ((w & 1) != 0) fh[k + 32 * j]++;
                                        w >>= 1;
                                    }
                                }
                            }

                            var fmin = fh.Min() / (double)fh.Max();

                            while (fmin > fullMax)
                            {
                                Interlocked.CompareExchange(ref fullMax, fmin, fullMax);
                            }

                            if (fmin > 0.989)
                            {
                                Console.WriteLine($"*** {s}, {fmin}");
                                uint per = 0;
                                uint u = 1;
                                do { u = mf(u); per++; } while (u != 1);
                                uint nfixed = 0;
                                u = 1;
                                while (u != 0) { if (u == mf(u)) nfixed++; u++; }
                                Console.WriteLine($"Period {per:X}, fixed {nfixed}: {s}");

                                using (var file = File.AppendText("results.txt"))
                                {
                                    file.WriteLine($"Period {per:X}, fixed {nfixed}: {s}");
                                }
                            }
                        });
                    }
                }
            })).ToArray();

            Task.WaitAll(tasks);
        }

        private static string MakeStr(BlockExpression expr)
        {
            var s = new StringBuilder();

            foreach (var subexpr in expr.Expressions)
            {
                s.Append(MakeStr(subexpr));
                s.Append("; ");
            }

            return s.ToString();
        }

        private static string MakeStr(Expression expr)
        {
            if (expr is BinaryExpression) return MakeStr((BinaryExpression)expr);
            if (expr is ParameterExpression) return ((ParameterExpression)expr).Name;
            if (expr is ConstantExpression) return ((ConstantExpression)expr).Value.ToString();
            var u = expr as UnaryExpression;

            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    return "~" + MakeStr(u.Operand);
            }

            throw new NotImplementedException();
        }

        private static string MakeStr(BinaryExpression expr)
        {
            string op;
            
            switch (expr.NodeType)
            {
                case ExpressionType.MultiplyAssign:
                    op = "*=";
                    break;

                case ExpressionType.AddAssign:
                    op = "+=";
                    break;

                case ExpressionType.ExclusiveOrAssign:
                    op = "^=";
                    break;

                case ExpressionType.LeftShift:
                    op = "<<";
                    break;

                case ExpressionType.RightShift:
                    op = ">>";
                    break;

                default:
                    throw new NotImplementedException();
            }

            return MakeStr(expr.Left) + " " + op + " " + MakeStr(expr.Right);
        }

        private static Expression MakeExpr(ParameterExpression p, Generator g)
        {
            return Expression.ExclusiveOrAssign(p, ShiftExpr(p, g));
        }

        private static Expression ShiftExpr(ParameterExpression p, Generator g)
        {
            return Expression.RightShift(p, Expression.Constant(g.Int32(13, 7)));
        }
    }
}
