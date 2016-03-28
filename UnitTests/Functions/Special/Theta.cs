
/*
Theta.cs

Copyright (c) 2016 Pluto Scarab Software. All Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Functions.Special;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Foundations.UnitTests.Functions
{
    [TestClass]
    public class ThetaTests
    {
        [TestMethod]
        public void Theta1ComplexTest()
        {
            var data = new[]
            {
                new Complex(0.325809816971265, 0.370161570985216), new Complex(-0.554674516560841, 0.499945368673331), new Complex(-1.93056857428662, 2.34534826872622), new Complex(-0.1472793517103625, 0.1560224176074929),
                new Complex(0.198956036583271, -1.87317433564776), new Complex(0.000948623894881476, -0.0231214497788391), new Complex(-0.413480618291979, -2.501046888987470), new Complex(0.2310770688461198, -1.0933062123454492),
                new Complex(0.383535878655032, -0.123381469441342), new Complex(-0.0410955986444651, 0.0539153508370558), new Complex(0.3880648652198836, 0.1080708332301739), new Complex(0.3119095691176429, 0.1661628828111056),
                new Complex(1.42936113058871, 1.01544618991625), new Complex(0.0159107820654157, 0.0511556824774055), new Complex(1.333570576905667, 0.614655026222637), new Complex(1.0997563008807847, 0.1208498036545937),
                new Complex(-1.01814545441105, 2.53426831012354), new Complex(-0.000747703469610204, 0.00024075059847843), new Complex(-2.089398894953623, -0.338003060204557), new Complex(-2.031668534521739, -0.493468543237303),
            };

            for (int i = 0; i < data.Length; i += 4)
            {
                var z = data[i];
                var q = data[i + 1];
                var th = Theta.θ1(z, q);
                var err = th - data[i + 2];
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
                var cfn = Theta.θ1ComplexForNome(q);
                th = cfn(z);
                err = th - data[i + 2];
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
                th = Theta.θ1(z, q.Real);
                err = th - data[i + 3];
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
            }
        }

        [TestMethod]
        public void Theta1DoubleTest()
        {
            var data = new[]
            {
                .12478414350, .82872474670, .0000590523,
               -.32903686210, .95465479750, .0000000045,
               -.32701981860, .41588039490, -0.2933109256,
                .08385962364, .79913716820, 0.000176717,
               1.4888543850,  .22263345920, 1.4354035775,
            };

            for (int i = 0; i < data.Length; i += 3)
            {
                var t = Theta.θ1(data[i], data[i + 1]);
                var e = data[i + 2];
                Assert.IsTrue(Math.Abs(t - e) < 1e-10);
                var dfn = Theta.θ1DoubleForNome(data[i + 1]);
                t = dfn(data[i]);
                Assert.IsTrue(Math.Abs(t - e) < 1e-10);
            }
        }

        [TestMethod]
        public void Theta2ComplexTest()
        {
            var data = new[]
            {
                new Complex(-0.712045573932278, 1.30561516966583), new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(-1.02921651633888, -0.55663233114557), new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(-0.270601468194827, -0.958161202750493), new Complex(0.212623597863526, -0.480192562215063),
                new Complex(2.06182432952114, 0.605868394894129), new Complex(-0.018128943757431, 0.175402508876567),
                new Complex(-0.0161641166929001, 1.12406115610682), new Complex(0.0974889810203627, -0.317370944403016),
            };

            var ans = new[] {
                "0.9510034016694605+0.7818307996124119 i",
                "0.4318298974220141-0.4811293513824665 i",
                "-3.491983713766145-1.639180977911250 i",
                "-0.4606794630399191-1.0510325356860531 i",
                "0.141664390294264-1.839493486453470 i",
            };

            for (int i = 0; i < data.Length; i += 2)
            {
                var z = data[i];
                var q = data[i + 1];
                var th = Theta.θ2(z, q);
                var ex = Parse(ans[i / 2]);
                var err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
                var tf = Theta.θ2ComplexForNome(q);
                th = tf(z);
                err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
            }
        }

        [TestMethod]
        public void Theta2DoubleTest()
        {
            var data = new[]
            {
                -0.712045573932278,  0.0115779438837883, 0.496601444431198,
                -1.02921651633888,   0.0251305156397967, 0.409986239688389,
                -0.270601468194827,  0.212623597863526,  1.35096326526392,
                 2.06182432952114,   0.018128943757431, -0.3458068199639,
                -0.0161641166929001, 0.0974889810203627, 1.128018761735100,
            };

            for (int i = 0; i < data.Length; i += 3)
            {
                var z = data[i];
                var q = data[i + 1];
                var th = Theta.θ2(z, q);
                var ex = data[i + 2];
                var err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
                var tf = Theta.θ2DoubleForNome(q);
                th = tf(z);
                err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
            }
        }

        private Complex Parse(string complex)
        {
            complex = complex.TrimEnd('i');
            int i = complex.IndexOf("+", 1);
            if (i == -1) i = complex.IndexOf("-", 1);
            if (i == -1) throw new FormatException();
            return new Complex(double.Parse(complex.Substring(0, i)), double.Parse(complex.Substring(i)));
        }

        [TestMethod]
        public void Theta2ComplexForNomeTest()
        {
            var data = new[]
            {
                new Complex(-0.712045573932278, 1.30561516966583), new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(-1.02921651633888, -0.55663233114557), new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(-0.270601468194827, -0.958161202750493), new Complex(0.212623597863526, -0.480192562215063),
                new Complex(2.06182432952114, 0.605868394894129), new Complex(-0.018128943757431, 0.175402508876567),
                new Complex(-0.0161641166929001, 1.12406115610682), new Complex(0.0974889810203627, -0.317370944403016),
            };

            var ans = new[] {
                "0.9510034016694605+0.7818307996124119 i",
                "0.4318298974220141-0.4811293513824665 i",
                "-3.491983713766145-1.639180977911250 i",
                "-0.4606794630399191-1.0510325356860531 i",
                "0.141664390294264-1.839493486453470 i",
            };

            for (int i = 0; i < data.Length; i += 2)
            {
                var z = data[i];
                var q = data[i + 1];
                var tn = Theta.θ2ComplexForNome(q);
                var th = tn(z);
                var ex = Parse(ans[i / 2]);
                var err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
            }
        }

        [TestMethod]
        public void Theta2ComplexAtZeroTest()
        {
            var data = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(0.212623597863526, -0.480192562215063),
                new Complex(-0.018128943757431, 0.175402508876567),
                new Complex(0.0974889810203627, -0.317370944403016),
            };

            var ans = new[] {
                "0.6582370796109659+0.0303389984653170 i",
                "0.8227835878337571-0.1180651209669994 i",
                "1.252099829145631-0.757243748784380 i",
                "1.1514971709760006+0.5030378061113643 i",
                "1.2808167514270764-0.5228813837190602 i",
            };

            for (int i = 0; i < data.Length; i++)
            {
                var q = data[i];
                var th = Theta.θ2(0, q);
                var ex = Parse(ans[i]);
                var err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
            }
        }

        [TestMethod]
        public void Theta2ComplexDoubleTest()
        {
            var data = new[]
            {
                -0.712045573932278,  1.30561516966583,   0.0115779438837883, 
                -1.02921651633888,  -0.55663233114557,   0.0251305156397967, 
                -0.270601468194827, -0.958161202750493,  0.212623597863526, 
                2.06182432952114,    0.605868394894129, -0.018128943757431, 
                -0.0161641166929001, 1.12406115610682,   0.0974889810203627,  
            };

            var ans = new[] {
                "0.982419608291746+0.734637332793804 i",
                "0.47435925857122-0.39980828620516 i",
                "2.3339170647049-0.80428867268903 i",
                "0.004025373680221-0.5849390348420 i",
                "2.05576743007898+0.0323625056143800 i",
            };

            for (int i = 0; i < data.Length; i += 3)
            {
                var z = new Complex(data[i], data[i + 1]);
                var q = data[i + 2];
                var th = Theta.θ2(z, q);
                var ex = Parse(ans[i / 3]);
                var err = th - ex;
                Assert.IsTrue(Complex.Abs(err) < 1e-13);
            }
        }

        [TestMethod]
        public void Theta3ComplexTest()
        {
            var data = new[]
            {
                1, .5,  .8, .2,   -8.5650514071151,  2.4686976173121,
                .5, 1,  .8, .2,  163.66474018387,   61.293085451771,
                .5, 0,  .5, .7,    2.3266061005072,  0.57989205450763,
            };

            for (int i = 0; i < data.Length; i += 6)
            {
                var z = new Complex(data[i], data[i + 1]);
                var q = new Complex(data[i + 2], data[i + 3]);
                var ex = new Complex(data[i + 4], data[i + 5]);
                var th = Theta.θ3(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
                var t3 = Theta.θ3ComplexForNome(q);
                th = t3(z);
                err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-14);
            }
        }

        [TestMethod]
        public void Theta3ComplexDoubleTest()
        {
            var data = new[]
            {
                1, .5,  .8,  -0.029804108096716, 0.12673663794188,
                .5, 1,  .8, -24.754831613744,  105.2655423467,
                1, -2,  .5, 141.53976744402,   -78.927794100850,
            };

            for (int i = 0; i < data.Length; i += 5)
            {
                var z = new Complex(data[i], data[i + 1]);
                var q = data[i + 2];
                var ex = new Complex(data[i + 3], data[i + 4]);
                var th = Theta.θ3(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-12);
            }
        }

        [TestMethod]
        public void Theta3DoubleTest()
        {
            var data = new[]
            {
                1,  .8,   0.04246457514070379,
                .5, .8,   1.223823417425446,
                .5, .5,   1.484396862425167,
            };

            for (int i = 0; i < data.Length; i += 3)
            {
                var z = data[i];
                var q = data[i + 1];
                var ex = data[i + 2];
                var th = Theta.θ3(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
                var t3 = Theta.θ3DoubleForNome(q);
                th = t3(z);
                err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
            }
        }

        [TestMethod]
        public void Theta4ComplexTest()
        {
            var data = new[]
            {
                1, .5,  .8, .2,     0.11267273837782, 0.63114326027714,
                .5, 1,  .8, .2,  -149.86895286679,   53.695192284056,
                .5, 0,  .5, .7,     0.5835418615326, -0.12747179582096,
            };

            for (int i = 0; i < data.Length; i += 6)
            {
                var z = new Complex(data[i], data[i + 1]);
                var q = new Complex(data[i + 2], data[i + 3]);
                var ex = new Complex(data[i + 4], data[i + 5]);
                var th = Theta.θ4(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-13);
                var t4 = Theta.θ4ComplexForNome(q);
                th = t4(z);
                err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-13);
            }
        }

        [TestMethod]
        public void Theta4ComplexDoubleTest()
        {
            var data = new[]
            {
                1, .5,  .8,   -2.2292262746206,  1.4720623460696,
                .5, 1,  .8,   -1.9162438634838, -0.33406441973926,
                1, -2,  .5, -421.90684150439,   64.81131918179,
            };

            for (int i = 0; i < data.Length; i += 5)
            {
                var z = new Complex(data[i], data[i + 1]);
                var q = data[i + 2];
                var ex = new Complex(data[i + 3], data[i + 4]);
                var th = Theta.θ4(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-13);
            }
        }

        [TestMethod]
        public void Theta4DoubleTest()
        {
            var data = new[]
            {
                1,  .8,   0.8713168498521653,
                .5, .8,   0.02201388267155668,
                .5, .5,   0.411526533253406,
            };

            for (int i = 0; i < data.Length; i += 3)
            {
                var z = data[i];
                var q = data[i + 1];
                var ex = data[i + 2];
                var th = Theta.θ4(z, q);
                var err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
                var t3 = Theta.θ4DoubleForNome(q);
                th = t3(z);
                err = Complex.Abs(th - ex) / Complex.Abs(th);
                Assert.IsTrue(Complex.Abs(err) < 1e-15);
            }
        }

        [TestMethod]
        public void ThetasAtZeroTest()
        {
            Complex th;

            double ex = 0.0;
            th = Theta.θ1(Complex.Zero, (Complex)(.5));
            Assert.AreEqual(0, th.Imaginary);
            Assert.AreEqual(ex, th.Real);
            th = Theta.θ1(Complex.Zero, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.AreEqual(ex, th.Real);
            th = Theta.θ1(0, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.AreEqual(ex, th.Real);
            th = Theta.θ1ComplexForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.AreEqual(ex, th.Real);
            th = Theta.θ1DoubleForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.AreEqual(ex, th.Real);

            ex = 2.12893125051303;
            th = Theta.θ2(Complex.Zero, (Complex)(.5));
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ2(Complex.Zero, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ2(0, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ2ComplexForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ2DoubleForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);

            ex = 2.12893682721188;
            th = Theta.θ3(Complex.Zero, (Complex)(.5));
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ3(Complex.Zero, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ3(0, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ3ComplexForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ3DoubleForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);

            ex = 0.121124208002581;
            th = Theta.θ4(Complex.Zero, (Complex)(.5));
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ4(Complex.Zero, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ4(0, .5);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ4ComplexForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);
            th = Theta.θ4DoubleForNome(.5)(0);
            Assert.AreEqual(0, th.Imaginary);
            Assert.IsTrue(Math.Abs(ex - th.Real) < 1e-14);

        }
    }
}
