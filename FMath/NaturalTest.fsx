#r "bin/debug/net8.0/fmath.dll"

open FMath

let a = nat 1_234_567_890
printfn "a = %A" a

let b = unat 4_000_000_000u
printfn "b = %A" b

let c = a + b
printfn "c = a + b = %A" c

let five = nat 5
let d = a * five
printfn "d = a * 5 = %A" d

let e = d * d
printfn "e = d * d = %A" e

let three = nat 3
let mutable p3 = three

for i in 1 .. 12 do
    p3 <- p3 * p3
    printfn "%A %A" i p3

printfn "%A / 10 = %A" a (a / 10u)

printfn "%A" (Natural.Parse "0118_999_81199_9119_725____3")