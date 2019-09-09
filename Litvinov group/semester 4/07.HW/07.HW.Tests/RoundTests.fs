module RoundTests

open NUnit.Framework
open FsUnit
open Round
open System

[<Test>]
let ``Check on given example``() =
    let roundCounter = new RoundCounter(3)
    roundCounter {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should (equalWithin 0.0001) 0.048

[<Test>]
let ``Check on given example with another accuracy``() =
    let roundCounter = new RoundCounter(2)
    roundCounter {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should (equalWithin 0.001) 0.05

[<Test>]
let ``Check on exaample with integers`` () =
    let roundCounter = new RoundCounter(7)
    roundCounter {
        let! a = 14.0 / 7.0
        let! b = 3.0
        return a * b
    } |> should (equalWithin 0.01) 6.0

[<Test>]
let ``Big test``() =
    let roundCounter = new RoundCounter(5)
    roundCounter {
        let! a = 23.0 / 7.5             // 3,06667
        let! b = 13.3 * 0.32            // 4,256
        let! c = 3.552 + 43.2857483     // 46,83775
        let! a = a / c                  // 0,06547
        return a * b
    } |> should (equalWithin 0.000001) 0.27864