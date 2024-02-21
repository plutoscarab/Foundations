
using System.Diagnostics.CodeAnalysis;

namespace Foundations;

/// <summary>
/// Units of measure of physical quantities.
/// </summary>
public readonly struct SI : IEquatable<SI>
{
    public int KilogramExp { get; init; }
    public int MetreExp { get; init; }
    public int SecondExp { get; init; }
    public int MoleExp { get; init; }
    public int AmpereExp { get; init; }
    public int KelvinExp { get; init; }
    public int CandelaExp { get; init; }

    public static readonly SI Dimensionless = new();

    public static readonly SI Kilogram = new() { KilogramExp = 1 };
    public static readonly SI Metre = new() { MetreExp = 1 };
    public static readonly SI Second = new() { SecondExp = 1 };
    public static readonly SI Mole = new() { MoleExp = 1 };
    public static readonly SI Ampere = new() { AmpereExp = 1 };
    public static readonly SI Kelvin = new() { KelvinExp = 1 };
    public static readonly SI Candela = new() { CandelaExp = 1 };
    public static readonly SI Coulomb = Ampere * Second;
    public static readonly SI Newton = Kilogram * Metre / (Second ^ 2);
    public static readonly SI Joule = Newton * Metre;
    public static readonly SI Watt = Joule / Second;
    public static readonly SI Volt = Joule / Coulomb;
    public static readonly SI Hertz = Second ^ -1;
    public static readonly SI Farad = Coulomb / Volt;
    public static readonly SI Ohm = Volt / Ampere;
    public static readonly SI Siemens = Ohm ^ -1;
    public static readonly SI Weber = Volt * Second;
    public static readonly SI SquareMetre = Metre ^ 2;
    public static readonly SI CubicMetre = Metre ^ 3;
    public static readonly SI Pascal = Newton / SquareMetre;
    public static readonly SI Tesla = Weber / SquareMetre;
    public static readonly SI Henry = Weber / Ampere;

    public static SI operator *(SI a, SI b) => new()
    {
        KilogramExp = a.KilogramExp + b.KilogramExp,
        MetreExp = a.MetreExp + b.MetreExp,
        SecondExp = a.SecondExp + b.SecondExp,
        MoleExp = a.MoleExp + b.MoleExp,
        AmpereExp = a.AmpereExp + b.AmpereExp,
        KelvinExp = a.KelvinExp + b.KelvinExp,
        CandelaExp = a.CandelaExp + b.CandelaExp,
    };

    public static SI operator /(SI a, SI b) => new()
    {
        KilogramExp = a.KilogramExp - b.KilogramExp,
        MetreExp = a.MetreExp - b.MetreExp,
        SecondExp = a.SecondExp - b.SecondExp,
        MoleExp = a.MoleExp - b.MoleExp,
        AmpereExp = a.AmpereExp - b.AmpereExp,
        KelvinExp = a.KelvinExp - b.KelvinExp,
        CandelaExp = a.CandelaExp - b.CandelaExp,
    };

    public static SI operator ^(SI unit, int power) =>
        new()
        {
            KilogramExp = unit.KilogramExp * power,
            MetreExp = unit.MetreExp * power,
            SecondExp = unit.SecondExp * power,
            MoleExp = unit.MoleExp * power,
            AmpereExp = unit.AmpereExp * power,
            KelvinExp = unit.KelvinExp * power,
            CandelaExp = unit.CandelaExp * power,
        };

    public SI PerSecond => this / Second;

    public static Quantity operator *(double value, SI units) => new(value, units);

    public static Quantity operator /(double value, SI units) => new(value, units ^ -1);

    public bool Equals(SI other) =>
        KilogramExp == other.KilogramExp &&
        MetreExp == other.MetreExp &&
        SecondExp == other.SecondExp &&
        MoleExp == other.MoleExp &&
        AmpereExp == other.AmpereExp &&
        KelvinExp == other.KelvinExp &&
        CandelaExp == other.CandelaExp;

    public override bool Equals([NotNullWhen(true)] object obj) =>
        obj is SI si && Equals(si);

    public static bool operator ==(SI a, SI b) => a.Equals(b);

    public static bool operator !=(SI a, SI b) => !a.Equals(b);

    public override int GetHashCode() => HashCode.Combine(KilogramExp, MetreExp, SecondExp, MoleExp, AmpereExp, KelvinExp, CandelaExp);

    private static void Append(StringBuilder s, int exp, string abbv)
    {
        if (exp == 0)
            return;

        if (s.Length > 0)
            s.Append('·');

        s.Append(abbv);
        if (exp < 0) s.Append('⁻');
        s.Append(Math.Abs(exp).ToString().Select(c => "⁰¹²³⁴⁵⁶⁷⁸⁹"[c - '0']));
    }

    public override string ToString()
    {
        var s = new StringBuilder();
        if (KilogramExp > 0) Append(s, KilogramExp, "kg");
        if (MetreExp > 0) Append(s, MetreExp, "m");
        if (SecondExp > 0) Append(s, SecondExp, "s");
        if (MoleExp > 0) Append(s, MoleExp, "mol");
        if (AmpereExp > 0) Append(s, AmpereExp, "A");
        if (KelvinExp > 0) Append(s, KelvinExp, "K");
        if (CandelaExp > 0) Append(s, CandelaExp, "cd");
        if (KilogramExp < 0) Append(s, KilogramExp, "kg");
        if (MetreExp < 0) Append(s, MetreExp, "m");
        if (SecondExp < 0) Append(s, SecondExp, "s");
        if (MoleExp < 0) Append(s, MoleExp, "mol");
        if (AmpereExp < 0) Append(s, AmpereExp, "A");
        if (KelvinExp < 0) Append(s, KelvinExp, "K");
        if (CandelaExp < 0) Append(s, CandelaExp, "cd");
        return s.ToString();
    }
}
