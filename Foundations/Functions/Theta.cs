
using Foundations.Types;

namespace Foundations.Functions
{
    /// <summary>
    /// Jacobi Theta functions.
    /// </summary>
    /// <remarks>
    /// Computation is by Fourier series definitions at http://dlmf.nist.gov/20.2#i
    /// </remarks>
    public static partial class Theta
    {
        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ1(Complex z, Complex q)
        {
            Complex
                qs = q * q,
                qsp = 1;

            Complex
                sum = 0,
                qp = Complex.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ1(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Complex, Complex> θ1ComplexForNome(Complex q)
        {
            var qn = new Complex[20];

            Complex
                qs = q * q,
                qsp = 1;

            Complex
                qp = Complex.Pow(q, .25);

            for (int n = 0; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= -qsp;
            }

            return z =>
            {
                Complex sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Complex 
                        term = qn[n] * Complex.Sin((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ1(Complex z, Double q)
        {
            Double
                qs = q * q,
                qsp = 1;

            Complex
                sum = 0,
                qp = Complex.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1DoubleForNome" /> to create a function of z alone.
        /// </summary>
        public static Double θ1(Double z, Double q)
        {
            Double
                qs = q * q,
                qsp = 1;

            Double
                sum = 0,
                qp = Math.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Double 
                    term = qp * Math.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ1(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Double, Double> θ1DoubleForNome(Double q)
        {
            var qn = new Double[20];

            Double
                qs = q * q,
                qsp = 1;

            Double
                qp = Math.Pow(q, .25);

            for (int n = 0; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= -qsp;
            }

            return z =>
            {
                Double sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Double 
                        term = qn[n] * Math.Sin((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ1(Quad z, Quad q)
        {
            Quad
                qs = q * q,
                qsp = 1;

            Quad
                sum = 0,
                qp = Quad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ1(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Quad, Quad> θ1QuadForNome(Quad q)
        {
            var qn = new Quad[20];

            Quad
                qs = q * q,
                qsp = 1;

            Quad
                qp = Quad.Pow(q, .25);

            for (int n = 0; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= -qsp;
            }

            return z =>
            {
                Quad sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Quad 
                        term = qn[n] * Quad.Sin((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ1(Quad z, Double q)
        {
            Double
                qs = q * q,
                qsp = 1;

            Quad
                sum = 0,
                qp = Quad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ1(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ1ComplexQuadForNome" /> to create a function of z alone.
        /// </summary>
        public static ComplexQuad θ1(ComplexQuad z, ComplexQuad q)
        {
            ComplexQuad
                qs = q * q,
                qsp = 1;

            ComplexQuad
                sum = 0,
                qp = ComplexQuad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                ComplexQuad 
                    term = qp * ComplexQuad.Sin((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ1(z) for a fixed value of nome q.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> θ1ComplexQuadForNome(ComplexQuad q)
        {
            var qn = new ComplexQuad[20];

            ComplexQuad
                qs = q * q,
                qsp = 1;

            ComplexQuad
                qp = ComplexQuad.Pow(q, .25);

            for (int n = 0; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= -qsp;
            }

            return z =>
            {
                ComplexQuad sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    ComplexQuad 
                        term = qn[n] * ComplexQuad.Sin((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ2(Complex z, Complex q)
        {
            if (z == 0)
                return θ2atZero(q);

            Complex
                qs = q * q,
                qsp = 1;

            Complex
                sum = 0,
                qp = Complex.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ2(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Complex, Complex> θ2ComplexForNome(Complex q)
        {
            var atz = θ2atZero(q);
            var qn = new Complex[20];

            {
                Complex
                    qs = q * q,
                    qsp = 1;

                Complex qp = Complex.Pow(q, .25);

                for (int n = 0; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= qsp;
                }
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Complex sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Complex 
                        term = qn[n] * Complex.Cos((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        private static Complex θ2atZero(Complex q)
        {
            Complex
                qs = q * q,
                qsp = 1;

            Complex
                sum = 0,
                qp = Complex.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Complex sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ2(Complex z, Double q)
        {
            if (z == 0)
                return θ2atZero(q);

            Double
                qs = q * q,
                qsp = 1;

            Complex
                sum = 0,
                qp = Complex.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2DoubleForNome" /> to create a function of z alone.
        /// </summary>
        public static Double θ2(Double z, Double q)
        {
            if (z == 0)
                return θ2atZero(q);

            Double
                qs = q * q,
                qsp = 1;

            Double
                sum = 0,
                qp = Math.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Double 
                    term = qp * Math.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ2(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Double, Double> θ2DoubleForNome(Double q)
        {
            var atz = θ2atZero(q);
            var qn = new Double[20];

            {
                Double
                    qs = q * q,
                    qsp = 1;

                Double qp = Math.Pow(q, .25);

                for (int n = 0; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= qsp;
                }
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Double sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Double 
                        term = qn[n] * Math.Cos((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        private static Double θ2atZero(Double q)
        {
            Double
                qs = q * q,
                qsp = 1;

            Double
                sum = 0,
                qp = Math.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Double sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ2(Quad z, Quad q)
        {
            if (z == 0)
                return θ2atZero(q);

            Quad
                qs = q * q,
                qsp = 1;

            Quad
                sum = 0,
                qp = Quad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ2(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Quad, Quad> θ2QuadForNome(Quad q)
        {
            var atz = θ2atZero(q);
            var qn = new Quad[20];

            {
                Quad
                    qs = q * q,
                    qsp = 1;

                Quad qp = Quad.Pow(q, .25);

                for (int n = 0; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= qsp;
                }
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Quad sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    Quad 
                        term = qn[n] * Quad.Cos((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        private static Quad θ2atZero(Quad q)
        {
            Quad
                qs = q * q,
                qsp = 1;

            Quad
                sum = 0,
                qp = Quad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Quad sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ2(Quad z, Double q)
        {
            if (z == 0)
                return θ2atZero(q);

            Double
                qs = q * q,
                qsp = 1;

            Quad
                sum = 0,
                qp = Quad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ2(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ2ComplexQuadForNome" /> to create a function of z alone.
        /// </summary>
        public static ComplexQuad θ2(ComplexQuad z, ComplexQuad q)
        {
            if (z == 0)
                return θ2atZero(q);

            ComplexQuad
                qs = q * q,
                qsp = 1;

            ComplexQuad
                sum = 0,
                qp = ComplexQuad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                ComplexQuad 
                    term = qp * ComplexQuad.Cos((2 * n + 1) * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Returns function θ2(z) for a fixed value of nome q.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> θ2ComplexQuadForNome(ComplexQuad q)
        {
            var atz = θ2atZero(q);
            var qn = new ComplexQuad[20];

            {
                ComplexQuad
                    qs = q * q,
                    qsp = 1;

                ComplexQuad qp = ComplexQuad.Pow(q, .25);

                for (int n = 0; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= qsp;
                }
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                ComplexQuad sum = 0;
                int n = 0;
            
                while (n < 20)
                {
                    ComplexQuad 
                        term = qn[n] * ComplexQuad.Cos((2 * n + 1) * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 2 * sum;
            };
        }

        private static ComplexQuad θ2atZero(ComplexQuad q)
        {
            ComplexQuad
                qs = q * q,
                qsp = 1;

            ComplexQuad
                sum = 0,
                qp = ComplexQuad.Pow(q, .25);

            int n = 0;
            
            while (n < 20)
            {
                ComplexQuad sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ3(Complex z, Complex q)
        {
            if (z == 0)
                return θ3atZero(q);

            Complex
                qp = q,
                qsp = q,
                qs = q * q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ3(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Complex, Complex> θ3ComplexForNome(Complex q)
        {
            var atz = θ3atZero(q);
            var qn = new Complex[20];

            Complex
                qp = q,
                qsp = q,
                qs = q * q;

            for (int n = 1; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= qsp;
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Complex sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Complex 
                        term = qn[n] * Complex.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Complex θ3atZero(Complex q)
        {
            Complex
                qp = q,
                qsp = q,
                qs = q * q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ3(Complex z, Double q)
        {
            if (z == 0)
                return θ3atZero(q);

            Double
                qp = q,
                qsp = q,
                qs = q * q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3DoubleForNome" /> to create a function of z alone.
        /// </summary>
        public static Double θ3(Double z, Double q)
        {
            if (z == 0)
                return θ3atZero(q);

            Double
                qp = q,
                qsp = q,
                qs = q * q;

            Double sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Double 
                    term = qp * Math.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ3(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Double, Double> θ3DoubleForNome(Double q)
        {
            var atz = θ3atZero(q);
            var qn = new Double[20];

            Double
                qp = q,
                qsp = q,
                qs = q * q;

            for (int n = 1; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= qsp;
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Double sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Double 
                        term = qn[n] * Math.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Double θ3atZero(Double q)
        {
            Double
                qp = q,
                qsp = q,
                qs = q * q;

            Double sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Double sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ3(Quad z, Quad q)
        {
            if (z == 0)
                return θ3atZero(q);

            Quad
                qp = q,
                qsp = q,
                qs = q * q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ3(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Quad, Quad> θ3QuadForNome(Quad q)
        {
            var atz = θ3atZero(q);
            var qn = new Quad[20];

            Quad
                qp = q,
                qsp = q,
                qs = q * q;

            for (int n = 1; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= qsp;
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                Quad sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Quad 
                        term = qn[n] * Quad.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Quad θ3atZero(Quad q)
        {
            Quad
                qp = q,
                qsp = q,
                qs = q * q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ3(Quad z, Double q)
        {
            if (z == 0)
                return θ3atZero(q);

            Double
                qp = q,
                qsp = q,
                qs = q * q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ3(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ3ComplexQuadForNome" /> to create a function of z alone.
        /// </summary>
        public static ComplexQuad θ3(ComplexQuad z, ComplexQuad q)
        {
            if (z == 0)
                return θ3atZero(q);

            ComplexQuad
                qp = q,
                qsp = q,
                qs = q * q;

            ComplexQuad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                ComplexQuad 
                    term = qp * ComplexQuad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ3(z) for a fixed value of nome q.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> θ3ComplexQuadForNome(ComplexQuad q)
        {
            var atz = θ3atZero(q);
            var qn = new ComplexQuad[20];

            ComplexQuad
                qp = q,
                qsp = q,
                qs = q * q;

            for (int n = 1; n < 20; n++)
            {
                qn[n] = qp;
                qsp *= qs;
                qp *= qsp;
            }

            return z => 
            {
                if (z == 0)
                    return atz;

                ComplexQuad sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    ComplexQuad 
                        term = qn[n] * ComplexQuad.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static ComplexQuad θ3atZero(ComplexQuad q)
        {
            ComplexQuad
                qp = q,
                qsp = q,
                qs = q * q;

            ComplexQuad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                ComplexQuad sum0 = sum;
                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ4(Complex z, Complex q)
        {
            if (z == 0)
                return θ4atZero(q);

            Complex
                qp = -q,
                qs = q * q,
                qsp = q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ4(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Complex, Complex> θ4ComplexForNome(Complex q)
        {
            var atz = θ4atZero(q);
            var qn = new Complex[20];

            {
                Complex
                    qp = -q,
                    qs = q * q,
                    qsp = q;

                for (int n = 1; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= -qsp;
                }
            }

            return z =>
            {
                if (z == 0)
                    return atz;

                Complex sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Complex 
                        term = qn[n] * Complex.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Complex θ4atZero(Complex q)
        {
            Complex
                qp = -q,
                qs = q * q,
                qsp = q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex sum0 = sum;

                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4ComplexForNome" /> to create a function of z alone.
        /// </summary>
        public static Complex θ4(Complex z, Double q)
        {
            if (z == 0)
                return θ4atZero(q);

            Double
                qp = -q,
                qs = q * q,
                qsp = q;

            Complex sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Complex 
                    term = qp * Complex.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4DoubleForNome" /> to create a function of z alone.
        /// </summary>
        public static Double θ4(Double z, Double q)
        {
            if (z == 0)
                return θ4atZero(q);

            Double
                qp = -q,
                qs = q * q,
                qsp = q;

            Double sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Double 
                    term = qp * Math.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ4(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Double, Double> θ4DoubleForNome(Double q)
        {
            var atz = θ4atZero(q);
            var qn = new Double[20];

            {
                Double
                    qp = -q,
                    qs = q * q,
                    qsp = q;

                for (int n = 1; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= -qsp;
                }
            }

            return z =>
            {
                if (z == 0)
                    return atz;

                Double sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Double 
                        term = qn[n] * Math.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Double θ4atZero(Double q)
        {
            Double
                qp = -q,
                qs = q * q,
                qsp = q;

            Double sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Double sum0 = sum;

                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ4(Quad z, Quad q)
        {
            if (z == 0)
                return θ4atZero(q);

            Quad
                qp = -q,
                qs = q * q,
                qsp = q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ4(z) for a fixed value of nome q.
        /// </summary>
        public static Func<Quad, Quad> θ4QuadForNome(Quad q)
        {
            var atz = θ4atZero(q);
            var qn = new Quad[20];

            {
                Quad
                    qp = -q,
                    qs = q * q,
                    qsp = q;

                for (int n = 1; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= -qsp;
                }
            }

            return z =>
            {
                if (z == 0)
                    return atz;

                Quad sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    Quad 
                        term = qn[n] * Quad.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static Quad θ4atZero(Quad q)
        {
            Quad
                qp = -q,
                qs = q * q,
                qsp = q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad sum0 = sum;

                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4QuadForNome" /> to create a function of z alone.
        /// </summary>
        public static Quad θ4(Quad z, Double q)
        {
            if (z == 0)
                return θ4atZero(q);

            Double
                qp = -q,
                qs = q * q,
                qsp = q;

            Quad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                Quad 
                    term = qp * Quad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Jacobi θ4(z, q) where q is the nome.
        /// If you need to evaluate many times using the same q value, 
        /// use <see cref="Theta.θ4ComplexQuadForNome" /> to create a function of z alone.
        /// </summary>
        public static ComplexQuad θ4(ComplexQuad z, ComplexQuad q)
        {
            if (z == 0)
                return θ4atZero(q);

            ComplexQuad
                qp = -q,
                qs = q * q,
                qsp = q;

            ComplexQuad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                ComplexQuad 
                    term = qp * ComplexQuad.Cos(2 * n * z),
                    sum0 = sum;

                if ((sum += term) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

        /// <summary>
        /// Returns function θ4(z) for a fixed value of nome q.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> θ4ComplexQuadForNome(ComplexQuad q)
        {
            var atz = θ4atZero(q);
            var qn = new ComplexQuad[20];

            {
                ComplexQuad
                    qp = -q,
                    qs = q * q,
                    qsp = q;

                for (int n = 1; n < 20; n++)
                {
                    qn[n] = qp;
                    qsp *= qs;
                    qp *= -qsp;
                }
            }

            return z =>
            {
                if (z == 0)
                    return atz;

                ComplexQuad sum = 0;
                int n = 1;
            
                while (n < 20)
                {
                    ComplexQuad 
                        term = qn[n] * ComplexQuad.Cos(2 * n * z),
                        sum0 = sum;

                    if ((sum += term) == sum0) break;
                    n++;
                }

                return 1 + 2 * sum;
            };
        }

        private static ComplexQuad θ4atZero(ComplexQuad q)
        {
            ComplexQuad
                qp = -q,
                qs = q * q,
                qsp = q;

            ComplexQuad sum = 0;
            int n = 1;
            
            while (n < 20)
            {
                ComplexQuad sum0 = sum;

                if ((sum += qp) == sum0) break;
                n++;
                qsp *= qs;
                qp *= -qsp;
            }

            return 1 + 2 * sum;
        }

    }
}

