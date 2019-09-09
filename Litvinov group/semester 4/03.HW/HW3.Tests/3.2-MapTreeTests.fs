module MapTreeTests

open NUnit.Framework
open FsUnit
open MapTree

[<Test>]
let ``Try on empty tree`` () =
    let tree = mapTree (fun x -> x * 2) Empty
    let check =
        match tree with
        | Empty -> true
        | _ -> false

    check |> should equal true

[<Test>]
let ``Try on big tree`` () =
    mapTree (fun x -> x * 2) (Tree (5, Tree (3, Tree (2, Tree (1, Empty, Empty), Empty), Tree (4, Empty, Empty)), Tree (7, Tree (6, Empty, Empty), Tree (8, Empty, Tree (9, Empty, Empty))))) 
    |> should equal (Tree (10, Tree (6, Tree (4, Tree (2, Empty, Empty), Empty), Tree (8, Empty, Empty)), Tree (14, Tree (12, Empty, Empty), Tree (16, Empty, Tree (18, Empty, Empty)))))