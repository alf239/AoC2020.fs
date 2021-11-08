open System
open System.Collections.Generic

let readLines filePath = System.IO.File.ReadLines(filePath)

[<EntryPoint>]
let main argv =
    let year = 2020
    let pairFor x = year - x
    let lines = readLines "C:\\Users\\alexe\\Learning\\AoC2020.fs\\inputs\\day_01.txt"
    let ints = lines |> Seq.map int
    let found = (        
        ints
        |> Seq.scan
            (fun state i ->
                let (seen: int Set), _ = state
                let valueIfPaired = if seen.Contains(pairFor i) then Some(i) else None
                seen.Add(i), valueIfPaired)
            (Set.empty, None)
        |> Seq.map (fun (_, valueIfPaired) -> valueIfPaired)
        |> Seq.find Option.isSome
    )
    let number = found.Value
    let other = pairFor number
    printfn "1. %d * %d = %d" number other (number * other)

    let xs = ints |> Seq.toArray
    let n = Array.length xs
    for i in 1 .. n-3 do
        let a = xs.[i]
        let index = new Dictionary<int, int>() // sum -> product
        for j in 0 .. i-1 do
            let b = xs.[j]
            // Go directly to target c
            index.Add(year - a - b, a * b)
        for j in i+1 .. n-1 do
            let c = xs.[j]
            let (found, product) = index.TryGetValue(c)
            if found then printfn "2. %d" (c * product)

    0