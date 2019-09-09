module FibonacciTests

open NUnit.Framework
open FsUnit
open Fibonacci

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 10`` () =
    fibonacci (bigint 10) |> should equal (bigint 10)

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 20`` () =
    fibonacci (bigint 20) |> should equal (bigint 10)

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 30`` () =
    fibonacci (bigint 30) |> should equal (bigint 10)

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 40`` () =
    fibonacci (bigint 40) |> should equal (bigint 44)

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 200`` () =
    fibonacci (bigint 200) |> should equal (bigint 188)

[<Test>]
let ``Check whether the sum is counted correctly with upperBound = 1000000`` () =
    fibonacci (bigint 1000000) |> should equal (bigint 1089154)