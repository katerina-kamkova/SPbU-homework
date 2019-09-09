module SingleThreadLazy

open ILazy

/// Lazy calculations
type SingleThreadLazy<'a> (supplier : unit -> 'a) =
    let mutable calledBefore = false
    let mutable result = None

    /// If called for the first time - calculate and return the result,
    /// else return already calculated result
    interface ILazy<'a> with
        member this.Get () = 
            if not calledBefore
            then calledBefore <- true
                 result <- Some(supplier ())
            result.Value
