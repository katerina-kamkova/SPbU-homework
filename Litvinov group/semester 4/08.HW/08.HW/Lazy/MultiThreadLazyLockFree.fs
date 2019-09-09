module MultiThreadLazyLockFree

open ILazy
open System.Threading

/// Lazy calculation with the protection from mulrithreading without locks
type MultiThreadLazyLockFree<'a>(supplier: unit -> 'a) =
    let mutable calledBefore = false
    let mutable result = None

    /// If called for the first time - calculate and return the result
    /// and be shure, that only one thread will calculate,
    /// else return already calculated result, and without locks
    interface ILazy<'a> with
        member this.Get() = 
            if not calledBefore
            then let startVal = result
                 let desiredVal = Some(supplier())
                 Interlocked.CompareExchange(&result, desiredVal, startVal) |> ignore
                 calledBefore <- true
            result.Value
