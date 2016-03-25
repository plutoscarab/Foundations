
/*
Jacobi.cs

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
    [TestClass]
    public class JacobiTests
    {
        private Complex Parse(string complex)
        {
            complex = complex.TrimEnd('i');
            int i = complex.IndexOf("+", 1);
            if (i == -1) i = complex.IndexOf("-", 1);
            if (i == -1) throw new FormatException();
            return new Complex(double.Parse(complex.Substring(0, i)), double.Parse(complex.Substring(i)));
        }

        [TestMethod]
        public void JacobiCnComplexArgTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(2.12623597863526, 0.480192562215063),
                new Complex(-1.8128943757431, 0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .8287247467,
               .9546547975,
               .4158803949,
               .7991371682,
               .2226334592,
            };

            var ans = new[] {
                "0.9999353004739177-0.0000249497100713 i",
                "0.9998120824513089+0.0004018967006233 i",
                "-0.2620696287961345-0.3671429875904256 i",
                "0.2003469806496059+0.0828329851756354 i",
                "0.6050679400137476-0.2419659452739375 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var cn = Jacobi.cn(z[i], m[i]);
                var ex = Parse(ans[i]);
                Assert.IsTrue(Complex.Abs(cn - ex) < 1e-15);
                var cc = Jacobi.cnComplex(m[i]);
                cn = cc(z[i]);
                Assert.IsTrue(Complex.Abs(cn - ex) < 1e-15);
            }
        }

        [TestMethod]
        public void JacobiArccnComplexArgTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(2.12623597863526, 0.480192562215063),
                new Complex(1.8128943757431, -0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .8287247467,
               .9546547975,
               .4158803949,
               .7991371682,
               .2226334592,
            };

            var ans = new[] {
                "0.9999353004739177-0.0000249497100713 i",
                "0.9998120824513089+0.0004018967006233 i",
                "-0.2620696287961345-0.3671429875904256 i",
                "0.2003469806496059+0.0828329851756354 i",
                "0.6050679400137476-0.2419659452739375 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var cn = Parse(ans[i]);
                var arccn = Jacobi.arccn(cn, m[i]);
                var ex = z[i];
                Assert.IsTrue(Complex.Abs(arccn - ex) < 1e-13);
            }
        }

        [TestMethod]
        public void JacobiSnComplexArgTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(2.12623597863526, 0.480192562215063),
                new Complex(-1.8128943757431, 0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .82872474670,
               .95465479750,
               .41588039490,
               .79913716820,
               .22263345920,
            };

            var ans = new[] {
                "0.011577520035845973+0.002154873907338315 i",
                "0.02513162775967239-0.01598866500104887 i",
                "1.0366906460437097-0.0928117050540744 i",
                "-0.9833652385779392+0.0168760678403997 i",
                "0.8497782949947773+0.1722870976144199 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var sn = Jacobi.sn(z[i], m[i]);
                var ex = Parse(ans[i]);
                Assert.IsTrue(Complex.Abs(sn - ex) < 1e-15);
                var f = Jacobi.snComplex(m[i]);
                sn = f(z[i]);
                Assert.IsTrue(Complex.Abs(sn - ex) < 1e-15);
            }
        }

        [TestMethod]
        public void JacobiArcsnComplexArgTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(1.4511560737138, -0.480192562215063),
                new Complex(-1.8128943757431, 0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .82872474670,
               .95465479750,
               .41588039490,
               .79913716820,
               .22263345920,
            };

            var ans = new[] {
                "0.011577520035845973+0.002154873907338315 i",
                "0.02513162775967239-0.01598866500104887 i",
                "1.0366906460437097-0.0928117050540744 i",
                "-0.9833652385779392+0.0168760678403997 i",
                "0.8497782949947773+0.1722870976144199 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var sn = Parse(ans[i]);
                var arcsn = Jacobi.arcsn(sn, m[i]);
                var ex = z[i];
                Assert.IsTrue(Complex.Abs(arcsn - ex) < 1e-14);
            }
        }

        [TestMethod]
        public void JacobiDnComplexArgTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(2.12623597863526, 0.480192562215063),
                new Complex(-1.8128943757431, 0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .82872474670,
               .95465479750,
               .41588039490,
               .79913716820,
               .22263345920,
            };

            var ans = new[] {
                "0.9999463821545492-0.0000206762130171 i",
                "0.9998206008771541+0.0003836693444763 i",
                "0.7479880904783000+0.0534965402190790 i",
                "0.4777309286723279+0.0277602955990134 i",
                "0.9203769973939489-0.0354146592336434 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var dn = Jacobi.dn(z[i], m[i]);
                var ex = Parse(ans[i]);
                Assert.IsTrue(Complex.Abs(dn - ex) < 1e-15);
                var f = Jacobi.dnComplex(m[i]);
                dn = f(z[i]);
                Assert.IsTrue(Complex.Abs(dn - ex) < 1e-15);
            }
        }

        [TestMethod]
        public void JacobiMultiComplexTest()
        {
            var z = new[]
            {
                new Complex(0.0115779438837883, 0.00215513498962569),
                new Complex(0.0251305156397967, -0.0159972042786677),
                new Complex(2.12623597863526, 0.480192562215063),
                new Complex(-1.8128943757431, 0.175402508876567),
                new Complex(0.974889810203627, 0.317370944403016),
            };

            var m = new[]
            {
               .82872474670,
               .95465479750,
               .41588039490,
               .79913716820,
               .22263345920,
            };

            var cn_ans = new[] {
                "0.9999353004739177-0.0000249497100713 i",
                "0.9998120824513089+0.0004018967006233 i",
                "-0.2620696287961345-0.3671429875904256 i",
                "0.2003469806496059+0.0828329851756354 i",
                "0.6050679400137476-0.2419659452739375 i",
            };

            var sn_ans = new[] {
                "0.011577520035845973+0.002154873907338315 i",
                "0.02513162775967239-0.01598866500104887 i",
                "1.0366906460437097-0.0928117050540744 i",
                "-0.9833652385779392+0.0168760678403997 i",
                "0.8497782949947773+0.1722870976144199 i",
            };

            var dn_ans = new[] {
                "0.9999463821545492-0.0000206762130171 i",
                "0.9998206008771541+0.0003836693444763 i",
                "0.7479880904783000+0.0534965402190790 i",
                "0.4777309286723279+0.0277602955990134 i",
                "0.9203769973939489-0.0354146592336434 i",
            };

            for (int i = 0; i < z.Length; i++)
            {
                var mt = Jacobi.Multi(z[i], m[i]);
                var ex = Parse(cn_ans[i]);
                Assert.IsTrue(Complex.Abs(mt.cn - ex) < 1e-15);
                ex = Parse(sn_ans[i]);
                Assert.IsTrue(Complex.Abs(mt.sn - ex) < 1e-15);
                ex = Parse(dn_ans[i]);
                Assert.IsTrue(Complex.Abs(mt.dn - ex) < 1e-15);
                var f = Jacobi.MultiComplex(m[i]);
                var mtf = f(z[i]);
                Assert.AreEqual(mt.sn, mtf.sn);
                Assert.AreEqual(mt.cn, mtf.cn);
                Assert.AreEqual(mt.dn, mtf.dn);
            }
        }
    }
}
