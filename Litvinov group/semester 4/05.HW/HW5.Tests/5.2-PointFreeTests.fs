module PointFreeTests

open NUnit.Framework
open FsCheck
open PointFree

[<Test>]
let ``Check func = func'1`` () =
    Check.Quick (fun x l -> (func x l) = (func'1 x l))

[<Test>]
let ``Check func = func'2`` () =
    Check.Quick (fun x l -> (func x l) = (func'2 x l))

[<Test>]
let ``Check func = func'3`` () =
    Check.Quick (fun x l -> (func x l) = (func'3 x l))

[<Test>]
let ``Check func = func'4`` () =
    Check.Quick (fun x l -> (func x l) = (func'4 x l))