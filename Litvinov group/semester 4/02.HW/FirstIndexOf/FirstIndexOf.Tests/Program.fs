module ``First index of then element in the list``

open NUnit.Framework
open FsUnit
open FirstIndex

[<Test>]
let ``Try find first index of given element in the empty array`` () =
    firstIndexOf [] "1" |> should equal None

[<Test>]
let ``Try find firstIndexOf existing element when it`s first`` () =
    firstIndexOf ["ss"; "3"; "dk"] "ss" |> should equal (Some(0))

[<Test>]
let ``Try find firstIndexOf existing element when it`s last`` () =
    firstIndexOf ["ss"; "3"; "dk"] "dk" |> should equal (Some(2))

[<Test>]
let ``Try find firstIndexOf existing element`` () =
    firstIndexOf ["ss"; "3"; "dk"] "3" |> should equal (Some(1))

[<Test>]
let ``Try find firstIndexOf existing element in bigger list with doubles`` () =
    firstIndexOf ["ss"; "ss"; "3"; "dk"; "ss"; "3"; "dk"; "3"; "dk"] "dk" |> should equal (Some(3))

[<Test>]
let ``Try find nonexistent element`` () =
    firstIndexOf ["2"; "kkk"; "qlj3"; "fh"] "!" |> should equal None