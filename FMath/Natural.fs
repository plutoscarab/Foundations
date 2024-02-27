
module FMath

    open System.Linq

    type Natural =
        private { 
            Data: uint array
            Offset: int
            Count: int
        }

        static member Create (n: uint) = { Data = [| n |]; Offset = 0; Count = 1 }

        static member Create (n: uint64) = { Data = [| (uint)n; (uint)(n >>> 32) |]; Offset = 0; Count = 2 }

        static member Create (data: uint array) = { Data = data; Offset = 0; Count = Natural.GetCount data }

        static member private GetCount (data: uint array) =
            data.Length - data.Reverse().TakeWhile(fun x -> x = 0u).Count()

        static member Zero = Natural.Create 0u

        static member One = Natural.Create 1u

        static member (+) (x: Natural, y: Natural) =
            let mutable accum = 0UL
            let size = max x.Count y.Count + 1
            let result = Array.create size 0u
            let len = min x.Count y.Count

            for i in 0 .. len - 1 do
                accum <- accum + (uint64)x.Data[i + x.Offset] + (uint64)y.Data[i + y.Offset]
                result[i] <- (uint)accum
                accum <- accum >>> 32

            if x.Count > y.Count then

                for i in len .. x.Count - 1 do
                    accum <- accum + (uint64)x.Data[i + x.Offset] 
                    result[i] <- (uint)accum
                    accum <- accum >>> 32

            else if x.Count < y.Count then

                for i in len .. y.Count - 1 do
                    accum <- accum + (uint64)y.Data[i + y.Offset]
                    result[i] <- (uint)accum
                    accum <- accum >>> 32

            result[size - 1] <- (uint)accum
            Natural.Create result

        member x.Head =
            if x.Count = 0 then 0u
            else x.Data[x.Offset]

        member x.Tail =
            if x.Count = 0 then x
            else { Data = x.Data; Offset = x.Offset + 1; Count = x.Count - 1 }

        member x.Shift1 =
            let result = Array.create (x.Count + 1) 0u
            Array.blit x.Data x.Offset result 1 x.Count
            { Data = result; Offset = 0; Count = x.Count + 1 }

        member x.Shift2 =
            let result = Array.create (x.Count + 2) 0u
            Array.blit x.Data x.Offset result 2 x.Count
            { Data = result; Offset = 0; Count = x.Count + 2 }

        static member (*) (x: Natural, y: Natural) =
            match x.Count, y.Count with
            | 0, _ -> Natural.Zero
            | _, 0 -> Natural.Zero
            | otherwise -> 
                let result = Array.create (x.Count + y.Count + 1) 0u

                for i in 0 .. x.Count - 1 do
                    for j in 0 .. y.Count - 1 do
                        let mutable product = (uint64)x.Data[i + x.Offset] * (uint64)y.Data[j + y.Offset]
                        let mutable k = i + j

                        while product <> 0UL do
                            product <- product + (uint64)result[k]
                            result[k] <- (uint)product
                            product <- product >>> 32
                            k <- k + 1

                Natural.Create result

        static member (/) (x: Natural, c: uint) =
            match x.Count with
            | 0 -> Natural.Zero, 0u
            | otherwise ->
                let result = Array.create x.Count 0u
                let mutable accum = 0UL

                for i in (x.Count - 1) .. -1 .. 0 do
                    accum <- (accum <<< 32) + (uint64)x.Data[i + x.Offset]
                    result[i] <- (uint)(accum / (uint64)c)
                    accum <- accum % (uint64)c

                (Natural.Create result), (uint)accum

        override x.ToString() = 
            if x.Count = 0 then
                "0"
            else
                let sb = new System.Text.StringBuilder()
                let mutable n = x
                let mutable count = 0

                while n.Count > 0 do
                    let div, rem = n / 10u
                    n <- div
                    if count = 6 then 
                        count <- 0
                        sb.Insert(0, ' ') |> ignore
                    count <- count + 1
                    sb.Insert(0, rem) |> ignore

                sb.ToString()

        static member Parse (s: string) =
            let result = Array.create ((s.Length + 8) / 9) 0u
            
            for ch in s do
                match ch with
                | '_' -> ch
                | d when System.Char.IsDigit(d) ->
                    let mutable accum = (uint64)(d - '0')
                    for i in 0 .. result.Length - 1 do
                        accum <- accum + 10UL * (uint64)result[i]
                        result[i] <- (uint)accum
                        accum <- accum >>> 32
                    ch
                | otherwise -> invalidArg "s" "Only digits and underscores allowed."
                |> ignore

            Natural.Create result

    let inline nat (n: int) = if n >= 0 then Natural.Create ((uint)n) else Natural.Zero

    let inline unat (n: uint) = Natural.Create n
