
# Bijections between natural numbers ℕ ∪ {0} and other countable objects

### Nat struct
A natural number.

### Integer.cs
Implements a bijection between natural numbers and integers.

### List.cs
Implements a bijection between natural numbers and finite lists of natural numbers.
This is actually a family of bijections with a parameter to determine the balance
between the list length and the size of the values.

### Rational.cs
Implements a bijection between natural numbers and non-negative rational numbers.
This differs from the pair bijection in Tuple.cs because pairs such as (2,4) are not
included since 2/4 is equivalent to 1/2.

### Set.cs
Implements a bijection between natural numbers and finite sets of natural numbers.
This is actually a family of bijections with a parameter to determine the balnce
between the set size and the size of the values.

### Tuple.cs
Implements bijections between natural numbers and ordered n-tuples of natural numbers.

### Word.cs
Implements a bijection between natural numbers and finite lists of values from a finite alphabet,
e.g. lists of bytes or lists of characters.