
module Natural

[<CustomComparison; CustomEquality>]
type Natural =
    | Nat of uint list

    static member Zero = Nat []

    static member One = Nat [ 1u ]

    static member (+) (Nat x, Nat y) = Nat (Natural.addWithCarry x y 0u)

    static member (+) (Nat x, y) = Nat (Natural.addCarry x y)

    static member (+) (x, Nat y) = Nat (Natural.addCarry y x)

    static member (~+) (Nat n) = Nat n

    static member (-) (Nat x, Nat y) = 
        let diff, b = Natural.subWithBorrow x y 0u
        if b = 0u then Nat diff else Nat []

    static member ( * ) (s, Nat n) = Nat (Natural.mulWithCarry s n 0u)

    static member ( * ) (Nat n, s) = Nat (Natural.mulWithCarry s n 0u)

    static member ( * ) (Nat x, Nat y) = Nat (Natural.mul x y)

    static member (/) (Nat x, Nat y) = Natural.One

    static member (/%) (Nat n, s) =
        let q, r = Natural.div (List.rev n) s 0u
        Nat (List.rev q), r

    static member (/) (Nat n, s) =
        let q, _ = Natural.div (List.rev n) s 0u
        Nat (List.rev q)

    static member Compare (Nat x) (Nat y) =
        let c = (List.length x).CompareTo(List.length y)
        if c <> 0 then c
        else Natural.compare (List.rev x) (List.rev y)

    interface System.IComparable<Natural> with
        member x.CompareTo(y) = Natural.Compare x y

    interface System.IEquatable<Natural> with
        member x.Equals(y) = (Natural.Compare x y) = 0

    static member op_Equality (x, y) = (Natural.Compare x y) = 0
    static member op_NotEqual (x, y) = (Natural.Compare x y) <> 0
    static member op_LessThan (x, y) = (Natural.Compare x y) < 0
    static member op_GreaterThan (x, y) = (Natural.Compare x y) > 0
    static member op_LessThanOrEqual (x, y) = (Natural.Compare x y) <= 0
    static member op_GreaterThanOrEqual (x, y) = (Natural.Compare x y) >= 0

    override x.Equals(other: obj) =
        match other with
        | :? Natural as n -> (Natural.Compare n x) = 0
        | _ -> false

    override x.GetHashCode() =
        match x with
        | Nat [] -> 0
        | Nat [a] -> hash a
        | Nat [a; b] -> hash (a, b)
        | Nat (a::b::c::tail) -> hash (a, b, c)

    override x.ToString() =
        match x with
        | Nat [] -> "0"
        | Nat n -> 
            match Natural.digits (List.rev n) |> List.rev with
            | [] -> "0"
            | [d] -> d.ToString()
            | head::tail -> head.ToString() + System.String.Concat(List.map (fun d -> $"{d:D9}") tail)

    static member private digits n =
        let div, rem = Natural.div n 1_000_000_000u 0u
        if div = [] then [rem]
        else rem :: (Natural.digits div)

    static member private addCarry x c = 
        match x, c with
        | _, 0u -> x
        | [], c -> [c]
        | (xh::xs), c -> 
            let sum = (uint64)c + (uint64)xh
            (uint)sum :: (Natural.addCarry xs ((uint)(sum >>> 32)))

    static member private addWithCarry x y c = 
        match x, y, c with
        | _, [], 0u -> x
        | [], _, 0u -> y
        | [], [], c -> [c]
        | _, [], c -> Natural.addCarry x c
        | [], _, c -> Natural.addCarry y c
        | (xh::xs), (yh::ys), c ->
            let sum = (uint64)c + (uint64)xh + (uint64)yh
            (uint)sum :: (Natural.addWithCarry xs ys ((uint)(sum >>> 32)))

    static member private subWithBorrow x y b =
        match x, y, b with
        | _, [], 0u -> x, 0u
        | [], _, _ -> [], b
        | xh::xs, [], _ ->
            let diff = (uint64)xh - (uint64)b
            let rest, b' = Natural.subWithBorrow xs [] ((uint)(diff >>> 32))
            if (uint)diff = 0u && rest = [] 
                then [], b'
                else ((uint)diff :: rest), b'
        | xh::xs, yh::ys, _ ->
            let diff = (uint64)xh - (uint64)yh - (uint64)b
            let rest, b' = Natural.subWithBorrow xs ys ((uint)(diff >>> 32))
            if (uint)diff = 0u && rest = [] 
                then [], b'
                else ((uint)diff :: rest), b'

    static member private mulWithCarry s n c =
        match s, n, c with
        | 0u, _, 0u -> []
        | 0u, _, c -> [c]
        | _, [], 0u -> []
        | _, [], c -> [c]
        | s, head::tail, c ->
            let prod = (uint64)c + (uint64)head * (uint64)s
            ((uint)prod) :: (Natural.mulWithCarry s tail ((uint)(prod >>> 32)))

    static member private mul x y =
        match x, y with
        | [], _ -> []
        | _, [] -> []
        | head::tail, _ -> Natural.addWithCarry (Natural.mulWithCarry head y 0u) (0u :: (Natural.mul tail y)) 0u

    static member private div n s c =
        match n, s, c with
        | [], _, _ -> [], c
        | _, 0u, _ -> [], c
        | head::tail, s, c ->
            let num = ((uint64)c <<< 32) + (uint64)head
            let q = (uint)(num / (uint64)s)
            let r = (uint)(num % (uint64)s)
            let d, rem = Natural.div tail s r
            if q = 0u then d, rem
            else q :: d, rem

    static member private compare x y =
        match x, y with
        | [], _ -> 0
        | _, [] -> 0
        | xh::xt, yh::yt ->
            let c = xh.CompareTo(yh)
            if c <> 0 then c
            else compare xt yt


let NatI i = if i <= 0 then Natural.Zero else Nat [ (uint)i ]
let NatU u = if u = 0u then Natural.Zero else Nat [ u ]
