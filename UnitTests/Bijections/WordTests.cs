
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Foundations.Types;

[TestClass]
public class WordTests
{
    private static readonly char[] Alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g'];

    [TestMethod]
    public void NatFromEmptyWord()
    {
        List<int> word = [];
        var n = Nat.FromWord(word, 5);
        Assert.AreEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToEmptyWord()
    {
        var word = Nat.Zero.ToWord(5);
        Assert.AreEqual(0, word.Count);
    }

    [TestMethod]
    public void NatFromWord()
    {
        List<int> word = [1, 2, 3, 4, 5];
        var n = Nat.FromWord(word, 6);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToWord()
    {
        var word = ((Nat)12345).ToWord(5);
        Assert.AreNotEqual(0, word.Count);
    }

    [TestMethod]
    public void WordRoundTripThroughNat()
    {
        List<int> word = [1, 3, 5, 7, 9];
        var n = Nat.FromWord(word, 10);
        var word2 = n.ToWord(10);
        Assert.IsTrue(Enumerable.SequenceEqual(word, word2));
    }

    [TestMethod]
    public void NatRoundTripThroughWord()
    {
        Nat n = 8675309;
        var word = n.ToWord(5);
        var m = Nat.FromWord(word, 5);
        Assert.AreEqual(n, m);
    }

    [TestMethod]
    public void NatToWordInjective()
    {
        Random rand = new("NatToWordInjective".GetHashCode());
        HashSet<Nat> domain = new(Enumerable.Range(0, 10_000).Select(_ => (Nat)rand.Next(1_000_000)));
        HashSet<List<int>> range = new(domain.Select(n => n.ToWord(5)), WordEqualityComparer<int>.Instance);
        Assert.AreEqual(domain.Count, range.Count);
    }

    class WordEqualityComparer<T> : IEqualityComparer<List<T>>
    {
        public static readonly WordEqualityComparer<T> Instance = new();

        public bool Equals(List<T> x, List<T> y) =>
            Enumerable.SequenceEqual(x, y);

        public int GetHashCode([DisallowNull] List<T> obj) =>
            obj.Count == 0 ? 0 : obj.Take(5).Select(o => o.GetHashCode()).Aggregate(HashCode.Combine);
    }

    [TestMethod]
    public void WordToNatInjective()
    {
        Random rand = new("WordToNatInjective".GetHashCode());

        HashSet<List<int>> domain = new(Enumerable.Range(0, 10_000).Select(_ => MakeWord(rand)), 
            WordEqualityComparer<int>.Instance);

        HashSet<Nat> range = new(domain.Select(word => Nat.FromWord(word, 5)));
        Assert.AreEqual(domain.Count, range.Count);
    }

    private static List<int> MakeWord(Random rand)
    {
        List<int> word = [];

        while (rand.Next(5) > 0)
        {
            word.Add(rand.Next(5));
        }

        return word;
    }

    [TestMethod]
    public void AllWordsTest()
    {
        var words = Nat.AllWords(5).Take(100).ToList();
        HashSet<List<int>> set = new(words, WordEqualityComparer<int>.Instance);
        Assert.AreEqual(words.Count, set.Count);
        HashSet<Nat> nats = new(words.Select(word => Nat.FromWord(word, 5)));
        Assert.AreEqual(words.Count, nats.Count);

        for (Nat n = 0; n < words.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }

    [TestMethod]
    public void AllWordsAlphabetTest()
    {
        var words = Nat.AllWords(Alphabet).Take(100).ToList();
        HashSet<List<char>> set = new(words, WordEqualityComparer<char>.Instance);
        Assert.AreEqual(words.Count, set.Count);
        HashSet<Nat> nats = new(words.Select(word => Nat.FromWord(word, Alphabet)));
        Assert.AreEqual(words.Count, nats.Count);

        for (Nat n = 0; n < words.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }
}