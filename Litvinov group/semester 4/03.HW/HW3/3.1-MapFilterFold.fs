module MapFilterFold

/// Count even numbers using map
let mapMethod input =
    input |> Seq.map (fun x -> if x % 2 = 0 then 1 else 0) |> Seq.sum
    
/// Count even numbers using filter
let filterMethod input =
    input |> Seq.filter (fun x -> x % 2 = 0) |> Seq.length
        
/// Count even numbers using fold
let foldMethod input =
    Seq.fold (fun acc x -> if x % 2 = 0 then (acc + 1) else acc) 0 input