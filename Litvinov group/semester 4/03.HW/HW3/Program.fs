// Learn more about F# at http://fsharp.org

open MapTree

[<EntryPoint>]
let main argv =
    let temp = mapTree (fun x -> x + 1) (Tree (1, Empty, Empty))
    printfn "%A" temp
    0 // return an integer exit code
