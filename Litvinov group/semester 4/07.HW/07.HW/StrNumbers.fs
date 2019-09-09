module StrNumbers

/// Try convert string to int, if fail then return None
let (|Int|_|) (str : string) =
   match System.Int32.TryParse(str) with
   | (true, int) -> Some(int)
   | _ -> None

/// Workflow that performs calculations
type StrCalculation() =
    member this.Bind((x: string), f) =
        match x with
        | Int newX -> f newX
        | _ -> None
    member this.Return(x) =
        Some(x)