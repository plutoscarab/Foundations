
namespace Foundations.RandomNumbers;

/// <summary>
/// An equidistributed sequence of <see cref="Next"/>
/// values. This differs from <see cref="IRandomSource"/> in that the
/// values may not be random, e.g. they may be quasirandom,
/// and hence not necessarily suitable as an IRandomSource.
/// See
/// https://en.wikipedia.org/wiki/Equidistributed_sequence
/// </summary>
public interface IUniformSource
{
    /// <summary>
    /// Gets equidistributed values in [0, 2⁶⁴) when
    /// called repeatedly.
    /// </summary>
    ulong Next();

    /// <summary>
    /// Gets a copy of this <see cref="IUniformSource"/>.
    /// </summary>
    /// <returns>
    /// Returns a copy of this <see cref="IUniformSource"/> with the
    /// same state. Returns null if the future state is not completely
    /// determined from the current state or if the source is
    /// otherwise unclonable.
    /// </returns>
    IUniformSource Clone();
}
