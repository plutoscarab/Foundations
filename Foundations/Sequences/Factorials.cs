﻿
/*
Factorials.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Foundations
{
    public static partial class Sequences 
    {
        private static long[] flong = new[]
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800,
            39916800, 479001600, 6227020800, 87178291200,
            1307674368000, 20922789888000, 355687428096000,
            6402373705728000, 121645100408832000,
            2432902008176640000,
        };

        private static double[] fdbl = new[]
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600,
            6227020800, 87178291200, 1307674368000, 20922789888000, 355687428096000,
            6.402373705728E+15, 1.21645100408832E+17, 2.43290200817664E+18,
            5.109094217170944E+19, 1.1240007277776077E+21, 2.5852016738884974E+22,
            6.2044840173323941E+23, 1.5511210043330984E+25, 4.0329146112660558E+26,
            1.0888869450418352E+28, 3.0488834461171384E+29, 8.8417619937397019E+30,
            2.6525285981219103E+32, 8.2228386541779224E+33, 2.6313083693369352E+35,
            8.6833176188118859E+36, 2.9523279903960412E+38, 1.0333147966386144E+40,
            3.7199332678990118E+41, 1.3763753091226343E+43, 5.23022617466601E+44,
            2.0397882081197442E+46, 8.1591528324789768E+47, 3.3452526613163803E+49,
            1.4050061177528798E+51, 6.0415263063373834E+52, 2.6582715747884485E+54,
            1.1962222086548019E+56, 5.5026221598120885E+57, 2.5862324151116818E+59,
            1.2413915592536073E+61, 6.0828186403426752E+62, 3.0414093201713376E+64,
            1.5511187532873822E+66, 8.0658175170943877E+67, 4.2748832840600255E+69,
            2.3084369733924138E+71, 1.2696403353658275E+73, 7.1099858780486338E+74,
            4.0526919504877214E+76, 2.3505613312828785E+78, 1.3868311854568981E+80,
            8.32098711274139E+81, 5.0758021387722473E+83, 3.1469973260387932E+85,
            1.9826083154044399E+87, 1.2688693218588415E+89, 8.24765059208247E+90,
            5.44344939077443E+92, 3.6471110918188683E+94, 2.4800355424368305E+96,
            1.711224524281413E+98, 1.197857166996989E+100, 8.504785885678623E+101,
            6.1234458376886085E+103, 4.4701154615126839E+105, 3.3078854415193862E+107,
            2.4809140811395396E+109, 1.88549470166605E+111, 1.4518309202828586E+113,
            1.1324281178206297E+115, 8.9461821307829743E+116, 7.15694570462638E+118,
            5.7971260207473678E+120, 4.7536433370128413E+122, 3.9455239697206583E+124,
            3.3142401345653532E+126, 2.81710411438055E+128, 2.4227095383672729E+130,
            2.1077572983795275E+132, 1.8548264225739844E+134, 1.650795516090846E+136,
            1.4857159644817614E+138, 1.3520015276784029E+140, 1.2438414054641306E+142,
            1.1567725070816416E+144, 1.0873661566567431E+146, 1.0329978488239058E+148,
            9.9167793487094965E+149, 9.6192759682482109E+151, 9.4268904488832465E+153,
            9.3326215443944134E+155, 9.3326215443944151E+157, 9.4259477598383583E+159,
            9.6144667150351251E+161, 9.9029007164861792E+163, 1.0299016745145627E+166,
            1.0813967582402908E+168, 1.1462805637347082E+170, 1.2265202031961378E+172,
            1.3246418194518288E+174, 1.4438595832024934E+176, 1.5882455415227429E+178,
            1.7629525510902446E+180, 1.974506857221074E+182, 2.2311927486598134E+184,
            2.5435597334721872E+186, 2.9250936934930154E+188, 3.3931086844518981E+190,
            3.9699371608087206E+192, 4.68452584975429E+194, 5.5745857612076058E+196,
            6.6895029134491261E+198, 8.0942985252734427E+200, 9.8750442008336011E+202,
            1.2146304367025329E+205, 1.5061417415111407E+207, 1.8826771768889257E+209,
            2.3721732428800469E+211, 3.0126600184576594E+213, 3.8562048236258041E+215,
            4.9745042224772865E+217, 6.4668554892204729E+219, 8.471580690878819E+221,
            1.1182486511960041E+224, 1.4872707060906857E+226, 1.9929427461615188E+228,
            2.6904727073180504E+230, 3.6590428819525483E+232, 5.0128887482749913E+234,
            6.9177864726194877E+236, 9.6157231969410882E+238, 1.3462012475717523E+241,
            1.8981437590761709E+243, 2.6953641378881624E+245, 3.8543707171800725E+247,
            5.5502938327393044E+249, 8.0479260574719917E+251, 1.1749972043909107E+254,
            1.7272458904546386E+256, 2.5563239178728654E+258, 3.8089226376305692E+260,
            5.713383956445854E+262, 8.62720977423324E+264, 1.3113358856834525E+267,
            2.0063439050956823E+269, 3.0897696138473508E+271, 4.7891429014633931E+273,
            7.4710629262828942E+275, 1.1729568794264143E+278, 1.8532718694937346E+280,
            2.9467022724950379E+282, 4.7147236359920609E+284, 7.5907050539472181E+286,
            1.2296942187394494E+289, 2.0044015765453024E+291, 3.2872185855342959E+293,
            5.4239106661315887E+295, 9.0036917057784364E+297, 1.5036165148649989E+300,
            2.526075744973198E+302, 4.2690680090047051E+304, 7.2574156153079978E+306,
        };

        private static double[] overf = new[]
        {
            1, 1, 0.5, 0.16666666666666666, 0.041666666666666664, 0.0083333333333333332,
            0.0013888888888888889, 0.00019841269841269841, 2.48015873015873E-05,
            2.7557319223985893E-06, 2.7557319223985888E-07, 2.505210838544172E-08,
            2.08767569878681E-09, 1.6059043836821613E-10, 1.1470745597729725E-11,
            7.6471637318198164E-13, 4.7794773323873853E-14, 2.8114572543455206E-15,
            1.5619206968586225E-16, 8.22063524662433E-18, 4.1103176233121648E-19,
            1.9572941063391263E-20, 8.8967913924505741E-22, 3.8681701706306841E-23,
            1.6117375710961184E-24, 6.4469502843844736E-26, 2.4795962632247976E-27,
            9.183689863795546E-29, 3.2798892370698378E-30, 1.1309962886447716E-31,
            3.7699876288159054E-33, 1.2161250415535179E-34, 3.8003907548547434E-36,
            1.1516335620771951E-37, 3.3871575355211618E-39, 9.67759295863189E-41,
            2.6882202662866363E-42, 7.2654601791530714E-44, 1.911963205040282E-45,
            4.9024697565135435E-47, 1.2256174391283859E-48, 2.9893108271424046E-50,
            7.117406731291439E-52, 1.6552108677421952E-53, 3.7618428812322616E-55,
            8.3596508471828045E-57, 1.817315401561479E-58, 3.866628513960594E-60,
            8.0554760707512364E-62, 1.6439747083165791E-63, 3.287949416633158E-65,
            6.4469596404571724E-67, 1.2397999308571486E-68, 2.3392451525606576E-70,
            4.3319354677049218E-72, 7.8762463049180392E-74, 1.4064725544496498E-75,
            2.4674957095607893E-77, 4.2543029475186022E-79, 7.2106829618959356E-81,
            1.2017804936493226E-82, 1.9701319568021682E-84, 3.1776321883905942E-86,
            5.0438606164930067E-88, 7.881032213270323E-90, 1.2124664943492804E-91,
            1.8370704459837581E-93, 2.74189618803546E-95, 4.0322002765227353E-97,
            5.8437685166996161E-99, 8.34824073814231E-101, 1.1758085546679308E-102,
            1.6330674370387929E-104, 2.2370786808750587E-106, 3.023079298479809E-108,
            4.0307723979730788E-110, 5.30364789206984E-112, 6.8878544052855063E-114,
            8.8305825708788554E-116, 1.1177952621365639E-117, 1.3972440776707049E-119,
            1.7249926884823517E-121, 2.103649620100429E-123, 2.53451761457883E-125,
            3.0172828744986073E-127, 3.5497445582336554E-129, 4.1276099514344832E-131,
            4.7443792545223946E-133, 5.3913400619572666E-135, 6.0576854628733334E-137,
            6.7307616254148149E-139, 7.3964413466096865E-141, 8.0396101593583548E-143,
            8.64474210683694E-145, 9.1965341562095112E-147, 9.680562269694223E-149,
            1.0083919030931481E-150, 1.0395792815393281E-152, 1.0607951852442122E-154,
            1.0715102881254669E-156, 1.0715102881254669E-158, 1.0609012753717494E-160,
            1.0400992895801465E-162, 1.0098051355147053E-164, 9.7096647645644744E-167,
            9.24729977577569E-169, 8.7238677129959346E-171, 8.1531473953233032E-173,
            7.54921055122528E-175, 6.92588123965622E-177, 6.2962556724147454E-179,
            5.6723024075808519E-181, 5.0645557210543323E-183, 4.4819077177471965E-185,
            3.9314979980238567E-187, 3.4186939113250927E-189, 2.9471499235561144E-191,
            2.5189315585949697E-193, 2.1346877615211607E-195, 1.7938552617824879E-197,
            1.49487938481874E-199, 1.2354375081146612E-201, 1.0126536951759517E-203,
            8.232956871349201E-206, 6.6394813478622588E-208, 5.3115850782898071E-210,
            4.2155437129284188E-212, 3.319325758211353E-214, 2.5932232486026196E-216,
            2.0102505803121082E-218, 1.5463466002400833E-220, 1.1804172520916666E-222,
            8.9425549400883838E-225, 6.7237255188634458E-227, 5.017705611092124E-229,
            3.7168189711793509E-231, 2.73295512586717E-233, 1.9948577561074232E-235,
            1.4455490986285676E-237, 1.0399633803083221E-239, 7.4283098593451572E-242,
            5.2683048647838E-244, 3.7100738484392957E-246, 2.5944572366708359E-248,
            1.8017064143547471E-250, 1.2425561478308602E-252, 8.5106585467867136E-255,
            5.7895636372698732E-257, 3.9118673224796439E-259, 2.6254143103890226E-261,
            1.7502762069260151E-263, 1.1591233158450432E-265, 7.6258112884542312E-268,
            4.9841903846106088E-270, 3.2364872627341619E-272, 2.0880562985381687E-274,
            1.338497627268057E-276, 8.5254625940640562E-279, 5.3958624013063652E-281,
            3.3936241517650096E-283, 2.1210150948531308E-285, 1.3174006800330006E-287,
            8.13210296316667E-290, 4.989020222801638E-292, 3.0420855017083157E-294,
            1.8436881828535247E-296, 1.1106555318394727E-298, 6.6506319271824719E-301,
            3.9587094804657568E-303, 2.34243164524601E-305, 1.3779009677917706E-307,
        };

        /// <summary>
        /// Factorials, 1, 1, 1*2, 1*2*3, 1*2*3*4, etc.
        /// </summary>
        public static IEnumerable<long> Factorials()
        {
            foreach (var f in flong) yield return f;
        }

        /// <summary>
        /// Factorials, 1, 1, 1*2, 1*2*3, 1*2*3*4, etc.
        /// </summary>
        public static IEnumerable<double> FactorialsD()
        {
            foreach (var f in fdbl) yield return f;
        }

        /// <summary>
        /// Factorials, 1, 1, 1*2, 1*2*3, 1*2*3*4, etc.
        /// </summary>
        public static IEnumerable<BigInteger> FactorialsB()
        {
            yield return 1; // 0!
            yield return 1; // 1!
            BigInteger p = 1, f = 1;

            while (true)
            {
                f++;
                p *= f;
                yield return p;
            }
        }

        /// <summary>
        /// Reciprocals of factorials.
        /// </summary>
        public static IEnumerable<double> OverFactorials()
        {
            foreach (var f in overf) yield return f;
        }
    }
}