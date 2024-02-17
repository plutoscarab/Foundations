
# Polynomials

### Indeterminate struct
Represents an indeterminate variable in a polynomial.

### Monomial class
Represents a single term of a polynomial, with a coefficient and zero or more indeterminates and their nonnegative
integer powers.

### Polynomial class
Represents a univariate or multivariate polynomial. An element of a PolynomialRing. Use the ring to create them.


# Groups

### Group abstract class
Defines the required behaviors of any group.


# Rings

### Ring abstract class
Defines the required behaviors of any ring.

### RingInt32 class
Implements the ring of System.Int32 values.

### RingZ class
Implements the ring of System.Numerics.BigInteger values.

### PolynomialRing class
Implements the ring of polynomials with coefficients over another ring.

### FieldPolynomialRing class
Implements the ring of univariate polynomials over finite fields,
including polynomial factorization.


# Fields

### Field abstract class
Defines the required behaviors of any field.

### FFValue struct
An element of a finite field. Use the field instance to create them.

### FiniteField class
Implements a finite field of given order and degree.

### BinaryGF class
Implements finite fields of order 2. Coefficients of polynomials are represented as bits in an integer.

### PrimeGF class
Implements finite fields of prime order (degree 1) using simple modular arithmetic.

### ExtensionGF class
Implements finite fields of orders that are powers of primes. Can automatically determine polynomial ideal.


# Miscellaneous

### Primes class
Enumeration of powers of primes, and rudimentary primality testing.
