
/*
Elliptic.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Functions;
using Foundations.UnitTesting;
using System;
using System.Numerics;

namespace Foundations.UnitTests.Functions
{
    [TestClass]
    public class EllipticTests
    {
        [TestMethod]
        public void EllipticFModulusOneHalfTest()
        {
            double f;
            var e = Elliptic.FDouble(Math.Sqrt(0.5));
            f = Elliptic.F(.5, Math.Sqrt(0.5));
            Assert.IsTrue(Math.Abs(f - 0.51516665702885808770) < 1e-12);
            f = e(0);
            Assert.AreEqual(0, f);
            f = e(1);
            Assert.IsTrue(Math.Abs(f - 1.1310061601574670032) < 1e-12);
        }

        [TestMethod]
        public void EllipticFLargeArgumentTest()
        {
            var x = 1436.699529559065124036301546242965863291805609504612854879;
            var f = Elliptic.F(1000, .8);
            Assert.IsTrue(Math.Abs(x - f) < 1e-12);
            f = Elliptic.FDouble(.8)(1000);
            Assert.IsTrue(Math.Abs(x - f) < 1e-12);
        }

        [TestMethod]
        public void EllipticFMatchesDoubleTest()
        {
            var rand = new Random("EllipticFMatchesDoubleTest".GetHashCode());

            for (int i = 0; i < 100; i++)
            {
                var m = rand.NextDouble();
                var efm = Elliptic.FDouble(m);
                var x = Math.Sqrt(-2 * Math.Log(rand.NextDouble())) * Math.Cos(Math.PI * rand.NextDouble());
                var f1 = Elliptic.F(x, m);
                var f2 = efm(x);
                Assert.AreEqual(f1, f2);
            }
        }

        private static Complex RandomZ(Random rand)
        {
            var t = rand.NextDouble();
            var s = Math.Sqrt(-2 * Math.Log(rand.NextDouble()));
            var x = s * Math.Cos(2 * Math.PI * t);
            var y = s * Math.Sin(2 * Math.PI * t);
            return new Complex(x, y);
        }

        [TestMethod]
        public void EllipticFMatchesComplexTest()
        {
            var rand = new Random("EllipticFMatchesComplexTest".GetHashCode());

            for (int i = 0; i < 100; i++)
            {
                var m = rand.NextDouble();
                var efm = Elliptic.FComplex(m);
                var z = RandomZ(rand);
                var f1 = Elliptic.F(z, m);
                var f2 = efm(z);
                Assert.AreEqual(f1, f2);
            }
        }

        [TestMethod]
        public void EllipticFParameterPoint9Test()
        {
            var e = Elliptic.FDouble(0.9);
            var f = e(.5);
            Assert.IsTrue(Math.Abs(f - 0.51976394243782869496) < 1e-12);
        }

        [TestMethod]
        public void EllipticFNegativeParameterTest()
        {
            var e = Elliptic.FDouble(-4.3);
            var f = e(2.718);
            Assert.IsTrue(Math.Abs(f - 1.6006675004822180492) < 1e-12);
        }

        [TestMethod]
        public void EllipticFParameterOverUnityTest()
        {
            var e = Elliptic.FDouble(1.01);
            var f = e(1);
            Assert.IsTrue(Math.Abs(f - 1.23037473300409532633) < 1e-12);
        }

        [TestMethod]
        public void EllipticFTest()
        {
            var z = new[] {
                new Complex(-0.670791254938757, -0.0938159212065481),
                new Complex(0.528053930350357, -1.07956370130938),
                new Complex(-0.557287707188084, -1.00272777341254),
                new Complex(0.320745728918526, 0.287620039090474),
                new Complex(-0.420904792797892, 0.642254696882941),
                new Complex(0.109863468987495, -0.47809418429494),
                new Complex(-0.272688296113657, 0.197822903078282),
                new Complex(10.9863468987495, -0.47809418429494),
                new Complex(-27.2688296113657, 0.197822903078282),
            };

            var expected = new[] {
                new Complex(-0.68620842267039771730, -0.10047616775855015460),
                new Complex(0.42868733998557565682, -1.05995343584072013101),
                new Complex(-0.46942504702869457356, -1.00008738595868835155),
                new Complex(0.31819382130927205251, 0.29124965412479359813),
                new Complex(-0.39580478305810881056, 0.64709397408138872245),
                new Complex(0.10569294199103250250, -0.47295612599211204389),
                new Complex(-0.27204914758137346116, 0.19984298803088524405),
                new Complex(12.12391310952531579389, -0.5977215792388405620),
                new Complex(-30.1535374477487750443, 0.22663693321479046126),
            };

            for (int i = 0; i < expected.Length; i++)
            {
                Complex f;
                f = Elliptic.F(z[i], 0.333);
                var err = (f - expected[i]).Magnitude;
                Assert.IsTrue(err < 1e-14);
            }
        }

        [TestMethod]
        public void EllipticFComplexTest()
        {
            var e = Elliptic.FComplex(0.333);

            var z = new[] {
                new Complex(-0.670791254938757, -0.0938159212065481),
                new Complex(0.528053930350357, -1.07956370130938),
                new Complex(-0.557287707188084, -1.00272777341254),
                new Complex(0.320745728918526, 0.287620039090474),
                new Complex(-0.420904792797892, 0.642254696882941),
                new Complex(0.109863468987495, -0.47809418429494),
                new Complex(-0.272688296113657, 0.197822903078282),
                new Complex(10.9863468987495, -0.47809418429494),
                new Complex(-27.2688296113657, 0.197822903078282),
            };

            var expected = new[] {
                new Complex(-0.68620842267039771730, -0.10047616775855015460),
                new Complex(0.42868733998557565682, -1.05995343584072013101),
                new Complex(-0.46942504702869457356, -1.00008738595868835155),
                new Complex(0.31819382130927205251, 0.29124965412479359813),
                new Complex(-0.39580478305810881056, 0.64709397408138872245),
                new Complex(0.10569294199103250250, -0.47295612599211204389),
                new Complex(-0.27204914758137346116, 0.19984298803088524405),
                new Complex(12.12391310952531579389, -0.5977215792388405620),
                new Complex(-30.1535374477487750443, 0.22663693321479046126),
            };

            for (int i = 0; i < expected.Length; i++)
            {
                Complex f;
                f = e(z[i]);
                var err = (f - expected[i]).Magnitude;
                Assert.IsTrue(err < 1e-14);
            }
        }

        [TestMethod]
        public void EllipticNomeTest()
        {
            double q, x;

            q = Elliptic.Nome(0);
            Assert.AreEqual(0, q);

            q = Elliptic.Nome(1);
            Assert.AreEqual(1, q);

            q = Elliptic.Nome(0.2);
            x = 0.01394285727531827;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);

            q = Elliptic.Nome(0.4);
            x = 0.03188334731336318;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);

            q = Elliptic.Nome(0.6);
            x = 0.05702025781460968;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);

            q = Elliptic.Nome(0.8);
            x = 0.09927369733882490;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);

        }

        [TestMethod]
        public void EllipticNomeWithKTest()
        {
            double q, x, K;

            q = Elliptic.Nome(0, out K);
            Assert.AreEqual(0, q);
            Assert.IsTrue(Math.Abs(K - Math.PI / 2) < 1e-10);

            q = Elliptic.Nome(1, out K);
            Assert.AreEqual(1, q);
            Assert.IsTrue(double.IsPositiveInfinity(K));

            q = Elliptic.Nome(0.2, out K);
            x = 0.01394285727531827;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.659623598610528000851247659235781517398160552079174827295;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);

            q = Elliptic.Nome(0.4, out K);
            x = 0.03188334731336318;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.777519371491253323502990072871915020298234739809831817591;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);

            q = Elliptic.Nome(0.6, out K);
            x = 0.05702025781460968;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.949567749806025882661770790728878407362558400119072023980;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);

            q = Elliptic.Nome(0.8, out K);
            x = 0.09927369733882490;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 2.257205326820853655083256004523387397235419281739999493155;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);
        }

        [TestMethod]
        public void EllipticNomeWithKPrimeTest()
        {
            double q, x, K, KPrime;

            q = Elliptic.Nome(0, out K, out KPrime);
            Assert.AreEqual(0, q);
            Assert.IsTrue(Math.Abs(K - Math.PI / 2) < 1e-10);
            Assert.IsTrue(double.IsPositiveInfinity(KPrime));

            q = Elliptic.Nome(1, out K, out KPrime);
            Assert.AreEqual(1, q);
            Assert.IsTrue(double.IsPositiveInfinity(K));
            Assert.IsTrue(Math.Abs(KPrime - Math.PI / 2) < 1e-10);

            q = Elliptic.Nome(0.2, out K, out KPrime);
            x = 0.01394285727531827;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.659623598610528000851247659235781517398160552079174827295;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);
            x = 2.257205326820853655083256004523387397235419281739999493155;
            Assert.IsTrue(Math.Abs(x - KPrime) < 1e-10);

            q = Elliptic.Nome(0.4, out K, out KPrime);
            x = 0.03188334731336318;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.777519371491253323502990072871915020298234739809831817591;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);
            x = 1.949567749806025882661770790728878407362558400119072023980;
            Assert.IsTrue(Math.Abs(x - KPrime) < 1e-10);

            q = Elliptic.Nome(0.6, out K, out KPrime);
            x = 0.05702025781460968;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 1.949567749806025882661770790728878407362558400119072023980;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);
            x = 1.777519371491253323502990072871915020298234739809831817591;
            Assert.IsTrue(Math.Abs(x - KPrime) < 1e-10);

            q = Elliptic.Nome(0.8, out K, out KPrime);
            x = 0.09927369733882490;
            Assert.IsTrue(Math.Abs(x - q) < 1e-10);
            x = 2.257205326820853655083256004523387397235419281739999493155;
            Assert.IsTrue(Math.Abs(x - K) < 1e-10);
            x = 1.659623598610528000851247659235781517398160552079174827295;
            Assert.IsTrue(Math.Abs(x - KPrime) < 1e-10);
        }
    }
}
