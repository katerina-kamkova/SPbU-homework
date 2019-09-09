module ILazy

/// Interface for class Lazy that describes lazy calculations
type ILazy<'a> =
    /// If called for the first time - calculate and return the result,
    /// else return already calculated result
    abstract member Get: unit -> 'a