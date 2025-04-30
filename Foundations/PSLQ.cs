
namespace Foundations.Algorithms;

public static class PSLQ
{
    public static int[] Find(double[] x)
    {
        var γ = 2 / Math.Sqrt(3);
        var n = x.Length;
        var A = new double[n + 1, n + 1];
        var B = new double[n + 1, n + 1];

        for (var i = 1; i <= n; i++)
            A[i, i] = B[i, i] = 1.0;

        var s = new double[n + 1];

        for (var k = 1; k <= n; k++)
            s[k] = Math.Sqrt(Enumerable.Range(k, n - k + 1).Select(j => x[j - 1] * x[j - 1]).Sum());

        var y = new double[n + 1];
        var t = s[1];

        for (var k = 1; k <= n; k++)
        {
            y[k] = x[k - 1] / t;
            s[k] /= t;
        }

        var H = new double[n + 1, n];

        for (var i = 1; i <= n; i++)
        {
            if (i < n) H[i, i] = s[i + 1] / s[i];

            for (var j = 1; j < i; j++)
                H[i, j] = -y[i] * y[j] / (s[j] * s[j + 1]);
        }

        for (var i = 2; i <= n; i++)
        {
            for (var j = i - 1; j >= 1; j--)
            {
                t = Math.Round(H[i, j] / H[j, j]);
                y[j] += t * y[i];

                for (var k = 1; k <= j; k++)
                    H[i, k] -= t * H[j, k];

                for (var k = 1; k <= n; k++)
                {
                    A[i, k] -= t * A[j, k];
                    B[k, j] += t * B[k, i];
                }
            }
        }

        while (true)
        {
            var max = double.MinValue;
            var m = -1;

            for (var i = 1; i < n; i++)
            {
                var q = Math.Pow(γ, i) * Math.Abs(H[i, i]);
                if (q > max) { max = q; m = i; }
            }

            (y[m], y[m + 1]) = (y[m + 1], y[m]);

            for (var i = 1; i <= n; i++)
                (A[m, i], A[m + 1, i]) = (A[m + 1, i], A[m, i]);

            for (var i = 1; i < n; i++)
                (H[m, i], H[m + 1, i]) = (H[m + 1, i], H[m, i]);

            for (var i = 1; i <= n; i++)
                (B[i, m], B[i, m + 1]) = (B[i, m + 1], B[i, m]);

            if (m <= n - 2)
            {
                var t0 = Math.Sqrt(Math.Pow(H[m, m], 2) + Math.Pow(H[m, m + 1], 2));
                var t1 = H[m, m] / t0;
                var t2 = H[m, m + 1] / t0;

                for (var i = m; i <= n; i++)
                {
                    var t3 = H[i, m];
                    var t4 = H[i, m + 1];
                    H[i, m] = t1 * t3 + t2 * t4;
                    H[i, m + 1] = -t2 * t3 + t1 * t4;
                }
            }

            for (var i = m + 1; i <= n; i++)
            {
                for (var j = Math.Min(i - 1, m + 1); j >= 1; j--)
                {
                    t = Math.Round(H[i, j] / H[j, j]);
                    y[j] += t * y[i];

                    for (var k = 1; k <= j; k++)
                        H[i, k] -= t * H[j, k];

                    for (var k = 1; k <= n; k++)
                    {
                        A[i, k] -= t * A[j, k];
                        B[k, j] += t * B[k, i];
                    }
                }
            }

            var min = double.MaxValue;
            var c = -1;

            for (var i = 1; i <= n; i++)
            {
                if (Math.Abs(y[i]) < min) { min = Math.Abs(y[i]); c = i; }
            }

            if (min < 1e-7)
            {
                var result = new int[n];

                for (var i = 1; i <= n; i++)
                    result[i - 1] = (int)Math.Round(B[i, c]);

                return result;
            }
        }
    }
}