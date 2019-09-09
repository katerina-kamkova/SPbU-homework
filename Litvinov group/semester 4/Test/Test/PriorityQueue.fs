module PriorityQueue

/// Priority queue 
/// Every element has its key - priority
/// Smaller value of the key means higher priority
type PriorityQueue<'T>() =
    /// List to keep pairs (priority, value)
    let mutable list = []

    /// Enqueue new pair (priority, value) according to priority
    member this.Enqueue priority (value: 'T) = 
        let rec findPlaceEnq fst snd =
            match snd with
            | [] -> fst @ [(priority, value)]
            | (hPriority, hvalue) :: tail -> if hPriority > priority 
                                             then fst @ [(priority, value)] @ snd
                                             else findPlaceEnq (fst @ [(hPriority, hvalue)]) tail
        list <- findPlaceEnq [] list

    /// Get the element with highest priority (smallest key)
    member this.Dequeue () = 
        match list.Length with
        | 0 -> raise (System.InvalidOperationException("Priority queue is empty"))
        | _ -> let temp = list.Head
               list <- list.Tail
               temp

    /// Check whether queue is empty
    member this.IsEmpty () = list.IsEmpty

    /// Get queue size
    member this.Count () = list.Length