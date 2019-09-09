module LazyTests

open LazyFactory

open NUnit.Framework
open FsUnit
open System
open System.Threading

[<Test>]
let ``Does it work at all (Lazy)`` () = 
    LazyFactory.CreateSingleThreadLazy(fun() -> 42).Get() |> should equal 42

[<Test>]
let ``Check whether supplier`s called once (Lazy)`` () =
    let mutable counter = ref 0L
    let lazyInstance = LazyFactory.CreateSingleThreadLazy(fun() -> Interlocked.Increment counter)
    for i in 1..100500 do
        lazyInstance.Get() |> should lessThan 2

[<Test>]
let ``Check whether it`s all right with null (Lazy)`` () = 
    LazyFactory.CreateSingleThreadLazy(fun() -> None).Get() |> should equal None

[<Test>]
let ``Does it work at all (MultiLazy)`` () = 
    LazyFactory.CreateMultiThreadLazy(fun() -> 42).Get() |> should equal 42

[<Test>]
let ``Check whether supplier`s called once (MultiLazy)`` () =
    let mutable counter = ref 0L
    let lazyInstance = LazyFactory.CreateMultiThreadLazy(fun() -> Interlocked.Increment counter)
    for i in 1..100500 do
        lazyInstance.Get() |> should lessThan 2

[<Test>]
let ``Check whether it`s all right with null (MultiLazy)`` () = 
    LazyFactory.CreateMultiThreadLazy(fun() -> None).Get() |> should equal None

[<Test>]
let ``Does it work at all (MultiLazyLockFree)`` () = 
    LazyFactory.CreateMultiThreadLazyLockFree(fun() -> 42).Get() |> should equal 42

[<Test>]
let ``Check whether supplier`s called once (MultiLazyLockFree)`` () =
    let mutable counter = ref 0L
    let lazyInstance = LazyFactory.CreateMultiThreadLazyLockFree(fun() -> Interlocked.Increment counter)
    for i in 1..100500 do
        lazyInstance.Get() |> should lessThan 2

[<Test>]
let ``Check whether it`s all right with null (MultiLazyLockFree)`` () = 
    LazyFactory.CreateMultiThreadLazyLockFree(fun() -> None).Get() |> should equal None

[<Test>]
let ``Check race (MultiLazy)`` () =
    let mutable counter = ref 0L
    let lazyInstance = LazyFactory.CreateMultiThreadLazy(fun() -> Interlocked.Increment counter)
    let result = lazyInstance.Get()
    for i in 1..100500 do
        ThreadPool.QueueUserWorkItem (fun obj -> result = lazyInstance.Get() |> should be True) |> ignore

[<Test>]
let ``Check race (MultiLazyLockFree)`` () =
    let mutable counter = ref 0L
    let lazyInstance = LazyFactory.CreateMultiThreadLazyLockFree(fun() -> Interlocked.Increment counter)
    let result = lazyInstance.Get()
    for i in 1..100500 do
        ThreadPool.QueueUserWorkItem (fun obj -> result = lazyInstance.Get() |> should be True) |> ignore
