module SquareTests

open NUnit.Framework
open FsUnit
open Square

[<Test>]
let ``Test on -2`` () =
    createPicture -2 |> should equal []

[<Test>]
let ``Test on 0`` () =
    createPicture 0 |> should equal []

[<Test>]
let ``Test on 1`` () =
    createPicture 1 |> should equal ["*"]

[<Test>]
let ``Test on 2`` () =
    createPicture 2 |> should equal ["**"; "**"]
    
[<Test>]
let ``Test on 3`` () =
    createPicture 3 |> should equal ["***"; "* *"; "***"]

[<Test>]
let ``Test on 4`` () =
    createPicture 4 |> should equal ["****"; "*  *"; "*  *"; "****"]