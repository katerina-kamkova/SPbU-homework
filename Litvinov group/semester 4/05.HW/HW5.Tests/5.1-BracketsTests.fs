module BracketsTests

open NUnit.Framework
open FsUnit
open Brackets

[<Test>]
let ``Check on empty string`` () =
    checkBrackets "" |> should equal true

[<Test>]
let ``Check on small correct string only from brackets`` () =
    checkBrackets "()" |> should equal true

[<Test>]
let ``Check on small incorrect string only from brackets`` () =
    checkBrackets "(]" |> should equal false

[<Test>]
let ``Check on big correct string from brackets`` () =
    checkBrackets "(([{{[]}}]))" |> should equal true

[<Test>]
let ``Check on big incorrect string from brackets`` () =
    checkBrackets "(([{[]}}]))" |> should equal false

[<Test>]
let ``Check on string withouy brackets`` () =
    checkBrackets "Haven`t broken means you never tried hard enough" |> should equal true

[<Test>]
let ``Check on correct string`` () = 
    checkBrackets "(Always code [as if {}the guy] who( ends up maintaining[ ]your code) will )be a violent psychopath who knows where you live"
    |> should equal true

[<Test>]
let ``Check on incorrect string`` () =
    checkBrackets "((Give a man a program,[ frustrate {him for a day. Teach a man] to program,} frustrate him for )a lifetime.)"
    |> should equal false