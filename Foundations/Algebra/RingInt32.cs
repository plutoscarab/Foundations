
namespace Foundations.Algebra;

public class RingInt32 : Ring<int>
{
    public static readonly RingInt32 Instance = new();

    private RingInt32()
    {}

    public override int Zero => 0;

    public override bool ElementsHaveSign => true;

    public override int Sign(int x) => Math.Sign(x);

    public override int One => 1;

    public override int Add(int a, int b) => checked(a + b);

    public override bool Equals(Ring<int> other) => other is RingInt32;

    public override int Multiply(int a, int b) => checked(a * b);

    public override int Negate(int x) => checked(-x);

    public override int RandomSmallNonzeroElement(Random rand)
    {
        while (true)
        {
            var x = 1 - 2 * Math.Log(rand.NextDouble());
            if (x <= int.MaxValue) return (int)x * (rand.Next(2) * 2 - 1);
        }
    }

    public override bool HasIntegerRepresentation => true;

    public override int FromInteger(BigInteger n) => (int)n;
}