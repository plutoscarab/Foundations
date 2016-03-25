
/*
Theta.cs

Copyright (c) 2016 Pluto Scarab Software. All Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Functions.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Foundations.UnitTests.Functions
{
    /// <summary>
    /// Summary description for CarlsonSymmetric
    /// </summary>
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
    }
}
