
open Natural

[<EntryPoint>]
let main argv =
    printfn "Hello, world!"
    let x = Nat [1u; 2u; 3u; 4u]
    let y = Nat [10u; 10u; 10u]
    let z = x + y
    printfn $"{x} + {y} = {z}"
    let u = 3u * z
    printfn $"3 * {z} = {u}"
    let w = x * x
    printfn $"{x} * {x} = {w}"

    printfn $"3^2^6 = {pown (NatI 3) (pown 2 6)}"

    printfn $"{x - x}"
    printfn $"{NatI 100} - {NatI 33} = {(NatI 100) - (NatI 33)}"
    printfn $"{NatI 33} - {NatI 100} = {(NatI 33) - (NatI 100)}"
    let n1 = Nat [1u]
    let n2 = Nat [1u]
    printfn $"{n1 = n2}"
    4
