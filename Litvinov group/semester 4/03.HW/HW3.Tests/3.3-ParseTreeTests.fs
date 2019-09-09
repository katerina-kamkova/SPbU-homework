module ParseTreeTests

open NUnit.Framework
open FsUnit
open ParseTree

[<Test>]
let ``Check on number`` () =
    calculate (Number 22.3) |> should (equalWithin 0.1) 22.3

[<Test>]
let ``Check on small expression`` () =
    calculate (Add (Number 33.12, Number 42.5)) |> should (equalWithin 0.001) 75.62

[<Test>]
let ``Check on big expression`` () =
    calculate (Add (Subtract (Number 3.0, (Multiply (Number 2.0, Number 0.5))), Divide (Number 3.9, Number ((double) 3)))) 
    |> should (equalWithin 0.1) 3.3

[<Test>]
let ``Try divide on 0`` () =
    (fun () -> calculate (Divide (Number 3.0, Number 0.0)) |> ignore) |> should throw typeof<System.DivideByZeroException>