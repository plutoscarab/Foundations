using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
	public class ScaleSymbol
	{
		public static ScaleSymbol[] ScaleSymbols = new ScaleSymbol[]
		{
			new ScaleSymbol("", 0),
			new ScaleSymbol("m", -3),	// milli
			new ScaleSymbol("u", -6),	// micro
			new ScaleSymbol("μ", -6),	// micro (obsolete)
			new ScaleSymbol("n", -9),	// nano
			new ScaleSymbol("p", -12),	// pico
			new ScaleSymbol("k", 3),	// kilo
			new ScaleSymbol("M", 6),	// mega
			new ScaleSymbol("G", 9),	// giga
			new ScaleSymbol("T", 12),	// tera
		};

		public string Symbol { get; private set; }
		public int Power { get; private set; }

		public ScaleSymbol(string symbol, int power)
		{
			System.Diagnostics.Debug.Assert(power % 3 == 0);
			Symbol = symbol;
			Power = power;
		}
	}

	public class Units
	{
		public static Units Identity = new Units(0, 0, 0, 0, 0, 0, 0);
        public static Units Meter = new Units(1, 0, 0, 0, 0, 0, 0);
        public static Units Kilogram = new Units(0, 1, 0, 0, 0, 0, 0);
        public static Units Second = new Units(0, 0, 1, 0, 0, 0, 0);
        public static Units Hertz = new Units(0, 0, -1, 0, 0, 0, 0);
        public static Units Ampere = new Units(0, 0, 0, 1, 0, 0, 0);
        public static Units Joule = new Units(2, 1, -2, 0, 0, 0, 0);
        public static Units Watt = new Units(2, 1, -3, 0, 0, 0, 0);
        public static Units Coulomb = new Units(0, 1, 0, 1, 0, 0, 0);
        public static Units Volt = new Units(2, 1, -3, -1, 0, 0, 0);
        public static Units Farad = new Units(-2, -1, 4, 2, 0, 0, 0);
        public static Units Ohm = new Units(2, 1, -3, -2, 0, 0, 0);
        public static Units Siemens = new Units(-2, -1, 3, 2, 0, 0, 0);
        public static Units Henry = new Units(2, 1, -2, -2, 0, 0, 0);
        public static Units Weber = new Units(2, 1, -2, -1, 0, 0, 0);
        public static Units Tesla = new Units(0, 1, -2, -1, 0, 0, 0);
        public static Units Newton = new Units(1, 1, -2, 0, 0, 0, 0);
        public static Units Pascal = new Units(-1, 1, -2, 0, 0, 0, 0);
        public static Units Lux = new Units(-2, 0, 0, 0, 0, 0, 1);

		public static UnitSymbol[] UnitSymbols = new UnitSymbol[]
		{
			new UnitSymbol("", Units.Identity),
			new UnitSymbol("ohm", Units.Ohm),
			new UnitSymbol("kg", Units.Kilogram),
			new UnitSymbol("Hz", Units.Hertz),
			new UnitSymbol("Wb", Units.Weber),
			new UnitSymbol("Pa", Units.Pascal),
			new UnitSymbol("me", Units.Meter),	// m = milli
            new UnitSymbol("lx", Units.Lux),
			new UnitSymbol("s", Units.Second),
			new UnitSymbol("A", Units.Ampere),
			new UnitSymbol("J", Units.Joule),
			new UnitSymbol("W", Units.Watt),
			new UnitSymbol("C", Units.Coulomb),
			new UnitSymbol("V", Units.Volt),
			new UnitSymbol("F", Units.Farad),
			new UnitSymbol("Ω", Units.Ohm),
			new UnitSymbol("H", Units.Henry),
			new UnitSymbol("Te", Units.Tesla),	// T = tera
			new UnitSymbol("N", Units.Newton),
		};

		public int Meters { get; private set; }
		public int Kilograms { get; private set; }
		public int Seconds { get; private set; }
		public int Amperes { get; private set; }
        public int Kelvins { get; private set; }
        public int Moles { get; private set; }
        public int Candelas { get; private set; }

		public Units(int m, int kg, int s, int A, int K, int mol, int cd)
		{
			Meters = m;
			Kilograms = kg;
			Seconds = s;
			Amperes = A;
            Kelvins = K;
            Moles = mol;
            Candelas = cd;
		}

		public override string ToString()
		{
			if (this == Units.Identity)
				return string.Empty;

			if (this == Units.Hertz)
				return "Hz";

			var s = new StringBuilder();

			for (int i = 4; i >= -4; i--)
			{
				foreach (var u in UnitSymbols)
				{
					if (this == u.Units * i)
					{
						if (i < 0)
							s.Append("/");

						s.Append(u.Symbol);

						if (Math.Abs(i) != 1)
						{
							s.Append("^");
							s.Append(Math.Abs(i));
						}

						return s.ToString();
					}
				}
			}

            Action<int, string> pattern = (n, sym) =>
            {
                if (n == 0) return;
                if (s.Length > 0) s.Append("·");
                s.Append(sym);
                if (n == 1) return;
                s.Append("^");
                s.Append(n);
            };

            pattern(Meters, "m");
            pattern(Kilograms, "kg");

            if (Seconds == -1)
            { pattern(-Seconds, "Hz"); }
            else
            { pattern(Seconds, "s"); }

            pattern(Amperes, "A");
            pattern(Kelvins, "K");
            pattern(Moles, "mol");
            pattern(Candelas, "cd");
            return s.ToString();
		}

		public static bool operator ==(Units a, Units b)
		{
			return a.Meters == b.Meters
				&& a.Kilograms == b.Kilograms
				&& a.Seconds == b.Seconds
				&& a.Amperes == b.Amperes
                && a.Kelvins == b.Kelvins
                && a.Moles == b.Moles
                && a.Candelas == b.Candelas;
		}

		public static bool operator !=(Units a, Units b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			var u = obj as Units;

			if (u == null)
				return false;

			return u == this;
		}

		public override int GetHashCode()
		{
            return Meters
                ^ Rotate(Kilograms, 4)
                ^ Rotate(Seconds, 8)
                ^ Rotate(Amperes, 12)
                ^ Rotate(Kelvins, 16)
                ^ Rotate(Moles, 20)
                ^ Rotate(Candelas, 24);
		}

		private static int Rotate(int x, int n)
		{
			return (x >> n) ^ (x << (32 - n));
		}

		public static Units operator -(Units u)
		{
			return new Units(-u.Meters, -u.Kilograms, -u.Seconds, -u.Amperes, -u.Kelvins, -u.Moles, -u.Candelas);
		}

		public static Units operator *(Units a, int b)
		{
			return new Units(
                a.Meters * b, 
                a.Kilograms * b, 
                a.Seconds * b, 
                a.Amperes * b,
                a.Kelvins * b,
                a.Moles * b,
                a.Candelas * b);
		}

        public static Units operator *(int a, Units b)
        {
            return b * a;
        }

		public static Units operator +(Units a, Units b)
		{
			return new Units(
                a.Meters + b.Meters, 
                a.Kilograms + b.Kilograms,
				a.Seconds + b.Seconds, 
                a.Amperes + b.Amperes,
                a.Kelvins + b.Kelvins,
                a.Moles + b.Moles,
                a.Candelas + b.Candelas);
		}

		public static Units operator -(Units a, Units b)
		{
			return new Units(
                a.Meters - b.Meters, 
                a.Kilograms - b.Kilograms,
				a.Seconds - b.Seconds, 
                a.Amperes - b.Amperes,
                a.Kelvins - b.Kelvins,
                a.Moles - b.Moles,
                a.Candelas - b.Candelas);
		}
	}

	public class UnitSymbol
	{
		public string Symbol { get; private set; }
		public Units Units { get; private set; }

		public UnitSymbol(string symbol, Units units)
		{
			Symbol = symbol;
			Units = units;
		}
	}

	public class UnitScale : IEquatable<UnitScale>
	{
		public static UnitScale Identity = new UnitScale(Units.Identity, 0);

		public Units Units { get; private set; }
		public int Scale { get; private set; }

		public UnitScale(Units units, int scale)
		{
			Units = units;
			Scale = scale;
		}

		public override string ToString()
		{
			var s = new StringBuilder();

			if (Scale != 0)
			{
				foreach (var ss in ScaleSymbol.ScaleSymbols)
				{
					if (Scale == ss.Power)
					{
						s.Append(ss.Symbol);
						break;
					}
				}

				if (s.Length == 0)
				{
					s.Append("*10^");
					s.Append(Scale);
				}
			}

			s.Append(Units);
			return s.ToString();
		}

		public static bool TryParse(string s, out UnitScale us)
		{
			string unit = string.Empty;
			Units units = Units.Identity;

			foreach (var u in Units.UnitSymbols)
			{
				if (u.Symbol.Length == 0)
					continue;

				if (s.EndsWith(u.Symbol))
				{
					unit = u.Symbol;
					units = u.Units;
					break;
				}
			}

			string scale = string.Empty;
			int power = 0;

			foreach (var sc in ScaleSymbol.ScaleSymbols)
			{
				if (sc.Symbol.Length == 0)
					continue;

				if (s == sc.Symbol + unit)
				{
					scale = sc.Symbol;
					power = sc.Power;
					break;
				}
			}

			if (s == scale + unit)
			{
				us = new UnitScale(units, power);
				return true;
			}

			us = UnitScale.Identity;
			return false;
		}

		public static UnitScale operator +(UnitScale a, UnitScale b)
		{
			return new UnitScale(a.Units + b.Units, a.Scale + b.Scale);
		}

		public static UnitScale operator -(UnitScale a, UnitScale b)
		{
			return new UnitScale(a.Units - b.Units, a.Scale - b.Scale);
		}

		public bool Equals(UnitScale other)
		{
			if (other == null) return false;
			return this.Scale == other.Scale && this.Units == other.Units;
		}

		public override bool Equals(object obj)
		{
			var us = obj as UnitScale;
			if (us == null) return false;
			return this.Equals(us);
		}

		public static bool operator ==(UnitScale a, UnitScale b)
		{
			if (object.ReferenceEquals(a, null)) 
				return object.ReferenceEquals(b, null);

			return a.Equals(b);
		}

		public static bool operator !=(UnitScale a, UnitScale b)
		{
			if (object.ReferenceEquals(a, null))
				return !object.ReferenceEquals(b, null);

			return !a.Equals(b);
		}

        public override int GetHashCode()
        {
            return Units.GetHashCode() ^ Scale.GetHashCode();
        }
	}

    public class Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        public static Quantity Identity = Units.Identity;
        public static Quantity Meter = Units.Meter;
        public static Quantity Kilogram = Units.Kilogram;
        public static Quantity Second = Units.Second;
        public static Quantity Hertz = Units.Hertz;
        public static Quantity Ampere = Units.Ampere;
        public static Quantity Joule = Units.Joule;
        public static Quantity Watt = Units.Watt;
        public static Quantity Coulomb = Units.Coulomb;
        public static Quantity Volt = Units.Volt;
        public static Quantity Farad = Units.Farad;
        public static Quantity Ohm = Units.Ohm;
        public static Quantity Siemens = Units.Siemens;
        public static Quantity Henry = Units.Henry;
        public static Quantity Weber = Units.Weber;
        public static Quantity Tesla = Units.Tesla;
        public static Quantity Newton = Units.Newton;
        public static Quantity Pascal = Units.Pascal;
        public static Quantity Lux = Units.Lux;

        public double Value { get; private set; }
        public Units Units { get; private set; }

        public Quantity(double value, Units units)
        {
            this.Value = value;
            this.Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Units.GetHashCode();
        }

        public bool Equals(Quantity other)
        {
            if (other == null) return false;
            return Value == other.Value && Units == other.Units;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Quantity);
        }

        private static void EnsureUnits(Quantity a, Quantity b)
        {
            if (a.Units != b.Units) throw new ArgumentException("Quantities must have the same units.");
        }

        public int CompareTo(Quantity other)
        {
            if (other == null) 
                throw new ArgumentNullException();

            EnsureUnits(this, other);
            return Value.CompareTo(other.Value);
        }

        public static Quantity operator +(Quantity a, Quantity b)
        {
            EnsureUnits(a, b);
            return new Quantity(a.Value + b.Value, a.Units);
        }

        public static Quantity operator -(Quantity a, Quantity b)
        {
            EnsureUnits(a, b);
            return new Quantity(a.Value - b.Value, a.Units);
        }

        public static Quantity operator -(Quantity q)
        {
            return new Quantity(-q.Value, q.Units);
        }

        public static Quantity operator *(Quantity a, Quantity b)
        {
            return new Quantity(a.Value * b.Value, a.Units + b.Units);
        }

        public static Quantity operator /(Quantity a, Quantity b)
        {
            return new Quantity(a.Value / b.Value, a.Units - b.Units);
        }

        public static implicit operator Quantity(double d)
        {
            return new Quantity(d, Units.Identity);
        }

        public static implicit operator Quantity(Units u)
        {
            return new Quantity(1.0, u);
        }
    }
}
