module FirstIndex

open System

/// Get the index of the first element in the list equal to the given
let firstIndexOf (list: List<string>) element =
    let rec findFirstIndexOf (list: List<string>) element i = 
        match list.Length with
        | 0 -> None
        | _ -> match list.Head with
               | el when el = element -> Some(i)
               | _ -> findFirstIndexOf list.Tail element (i + 1)

    findFirstIndexOf list element 0

/// Print result according to answer
let printAnswer answer =
    printfn ""
    match answer with
    | None -> printfn "There`s no such element in the given list"
    | Some(answer) -> printfn "The position is %i" answer

printf "Enter list elements using 'Space': "
let elements = Console.ReadLine().Split()
let list = [ for i in 0 .. (elements.Length - 1) -> elements.[i] ]

printf "Enter searched element: "
firstIndexOf list (Console.ReadLine()) |> printAnswer 