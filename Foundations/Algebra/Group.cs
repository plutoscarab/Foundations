
using System;

namespace Foundations.Algebra;

public record Group<T>(T Identity, Func<T, T, T> Op, Func<T, T> Invert);