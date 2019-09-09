module VebTests

open Veb

open NUnit.Framework
open FsUnit

[<Test>]
let ``Does it work at all`` () = 
    let res = downloadAllPagesByLink "https://www.macalester.edu/~abeverid/thrones.html"
    res.Length |> should equal 33

[<Test>]
let ``What happens when the link is wrong`` () =
    let res = downloadAllPagesByLink "https://Smth"
    res.Length |> should equal 1
    res.[0] |> should equal None
