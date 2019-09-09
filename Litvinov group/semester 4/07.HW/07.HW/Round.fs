module Round

open System

/// Workflow that performs calculations with wanted accuracy
type RoundCounter(accuracy: int) =
    member this.Bind((x: float), f) = 
        f (Math.Round(x, accuracy))
    member this.Return(x: float) = 
        Math.Round(x, accuracy)