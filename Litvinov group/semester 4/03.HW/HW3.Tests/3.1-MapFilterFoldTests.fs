module MapFilterFoldTests

open NUnit.Framework
open FsUnit
open FsCheck
open MapFilterFold

[<Test>]
let ``Map method on empty seq`` () =
    mapMethod Seq.empty |> should equal 0

[<Test>]
let ``Filter method on empty seq`` () =
    filterMethod Seq.empty |> should equal 0

[<Test>]
let ``Fold method on empty seq`` () =
    foldMethod Seq.empty |> should equal 0

[<Test>]
let ``Map method on {1 3 5 7 9 11}`` () =
    mapMethod [1; 3; 5; 7; 9; 11] |> should equal 0

[<Test>]
let ``Filter method on {1 3 5 7 9 11}`` () =
    filterMethod [1; 3; 5; 7; 9; 11] |> should equal 0

[<Test>]
let ``Fold method on {1 3 5 7 9 11}`` () =
    foldMethod [1; 3; 5; 7; 9; 11] |> should equal 0

[<Test>]
let ``Map method on {1 2 3 4 5 22 32 33}`` () =
    mapMethod [1; 2; 3; 4; 5; 22; 32; 33] |> should equal 4

[<Test>]
let ``Filter method on {1 2 3 4 5 22 32 33}`` () =
    filterMethod [1; 2; 3; 4; 5; 22; 32; 33] |> should equal 4

[<Test>]
let ``Fold method on {1 2 3 4 5 22 32 33}`` () =
    foldMethod [1; 2; 3; 4; 5; 22; 32; 33] |> should equal 4

[<Test>]
let ``Check equality mapMethod & filterMethod`` () =
    Check.QuickThrowOnFailure (fun (list: List<int>) -> (mapMethod list) = (filterMethod list))

[<Test>]
let ``Check equality mapMethod & foldMethod`` () =
    Check.QuickThrowOnFailure (fun (list: List<int>) -> (mapMethod list) = (foldMethod list))