module MultiThreadLazy

open ILazy
open System

/// Lazy calculation with the protection from mulrithreading
type MultiThreadLazy<'a> (supplier : unit -> 'a) =
    [<VolatileField>]
    let mutable calledBefore = false
    let mutable result = None
    let lockObject = new Object()

    /// If called for the first time - calculate and return the result
    /// and be sure, that only one thread will calculate and change calledBefore,
    /// else return already calculated result
    interface ILazy<'a> with
        member this.Get () = 
            if not calledBefore
            then lock lockObject (fun() -> if not calledBefore
                                           then result <- Some(supplier())
                                                calledBefore <- true)
            result.Value
