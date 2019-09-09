module LazyFactory

open ILazy
open SingleThreadLazy
open MultiThreadLazy
open MultiThreadLazyLockFree

/// Class that creates Lazy classes based on function supplier
type LazyFactory =

    /// Create Lazy class without multithreading
    static member CreateSingleThreadLazy supplier =
        new SingleThreadLazy<'a>(supplier) :> ILazy<'a>

    /// Create MultiThreadLazy class with semithreading
    static member CreateMultiThreadLazy supplier = 
        new MultiThreadLazy<'a>(supplier) :> ILazy<'a>

    /// Create MultiThreadLazy class with semithreading without locks
    static member CreateMultiThreadLazyLockFree supplier =
        new MultiThreadLazyLockFree<'a>(supplier) :> ILazy<'a>
