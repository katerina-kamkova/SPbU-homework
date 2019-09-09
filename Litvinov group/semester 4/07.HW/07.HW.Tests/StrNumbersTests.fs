module StrNumbersTests

open NUnit.Framework
open FsUnit
open StrNumbers

let calculator = new StrCalculation()

[<Test>]
let ``Check on given example``() =
    calculator {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    } |> should equal (Some(3))

[<Test>]
let ``Check on given fail example``() =
    calculator {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    } |> should equal None

[<Test>]
let ``Check on my example``() =
    calculator {
        let! x = "87"
        let! y = "25"
        let z = x + y
        let! a = "2"
        return z / 2
    } |> should equal (Some(56))

[<Test>]
let ``Check on my fail example``() =
    calculator {
        let! x = "31"
        let! y = "3o"
        let z = x + y
        return z
    } |> should equal None