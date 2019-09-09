module PrimeNumberTests

open NUnit.Framework
open FsUnit
open PrimeNumbers

[<Test>]
let ``Check several numbers`` () = 
    Seq.item 1 primeNumbers |> should equal 2
    Seq.item 2 primeNumbers |> should equal 3
    Seq.item 3 primeNumbers |> should equal 5
    Seq.item 10 primeNumbers |> should equal 29
    Seq.item 100 primeNumbers |> should equal 541
    Seq.item 168 primeNumbers |> should equal 997