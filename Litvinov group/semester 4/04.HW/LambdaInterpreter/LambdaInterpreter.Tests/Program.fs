module Tests

open NUnit.Framework
open FsUnit
open Main

[<Test>]
let ``Lambda term as a variable`` () =
    betaReduction (Variable("a")) |> should equal (Variable("a"))

[<Test>]
let ``Lambda term as simple abstraction`` () =
    betaReduction (Abstraction("x", Variable("x"))) |> should equal (Abstraction("x", Variable("x")))

[<Test>]
let ``Lambda term as complicated application with 2rename from the beginning`` () =
    betaReduction (Application(Abstraction("2rename", Abstraction("y", Abstraction("a", Application(Application(Variable("a"), Variable("y")), Variable("2rename"))))),
                        Abstraction("b", Application(Variable("b"), Variable("y"))))) |> 
    should equal (Abstraction("3rename", Abstraction("a", Application(Application(Variable("a"), Variable("3rename")), Abstraction("b", Application(Variable("b"), Variable("y")))))))