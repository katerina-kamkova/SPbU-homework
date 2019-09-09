module ``Merge sort tests`` 

open NUnit.Framework
open FsUnit
open MergeSort

[<Test>]
let ``Check merge with two lists by one element`` () =
    merge [2] [1] |> should equal [1; 2]

[<Test>]
let ``Check merge with long lists`` () =
    merge [1; 5; 6; 7] [2; 3; 3; 4; 5] |> should equal [1; 2; 3; 3; 4; 5; 5; 6; 7]
    
[<Test>]
let ``Try merge sort on empty list`` () =
    mergeSort [] |> should equal []

[<Test>]
let ``Try merge sort on [1]`` () =
    mergeSort [1] |> should equal [1]

[<Test>]
let ``Try merge sort on [1; 2]`` () =
    mergeSort [1; 2] |> should equal [1; 2]

[<Test>]
let ``Try merge sort on [2; 1]`` () =
    mergeSort [2; 1] |> should equal [1; 2]

[<Test>]
let ``Try merge sort on [1; 2; 3]`` () =
    mergeSort [1; 2; 3] |> should equal [1; 2; 3]

[<Test>]
let ``Try merge sort on [1; 3; 2]`` () =
    mergeSort [1; 3; 2] |> should equal [1; 2; 3]

[<Test>]
let ``Try merge sort on [2; 1; 3]`` () =
    mergeSort [2; 1; 3] |> should equal [1; 2; 3]

[<Test>]
let ``Try merge sort on [3; 2; 1]`` () =
    mergeSort [3; 2; 1] |> should equal [1; 2; 3]

[<Test>]
let ``Try merge sort on [1; 2; 3; 4; 5; 6; 7; 8; 9]`` () =
    mergeSort [1; 2; 3; 4; 5; 6; 7; 8; 9] |> should equal [1; 2; 3; 4; 5; 6; 7; 8; 9]

[<Test>]
let ``Try merge sort on [11; 4; 22; 121; 221; 4; 1; 22]`` () =
    mergeSort [11; 4; 22; 121; 221; 4; 1; 22] |> should equal [1; 4; 4; 11; 22; 22; 121; 221]