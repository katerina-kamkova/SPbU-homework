open System

printf "Enter n: "
let n = Console.ReadLine() |> Int32.Parse
printfn ""

printf "Enter m: "
let m = Console.ReadLine() |> Int32.Parse
printfn ""

let pow number degree =
    let rec powRec number degree acc = 
        if degree = 1 then
            acc * number
        else if (degree % 2) = 0 then
            powRec (number * number) (degree / 2) acc
        else
            powRec (number * number) ((degree - 1) / 2) (acc * number)
    powRec number degree 1

let change n m =
    let rec changeRec m list =
        match m with
        | 0 -> list
        | _ -> changeRec (m - 1) ((list.Head / 2) :: list)
    if n < 0 || m < 0 then
        []
    else
        changeRec m [(pow 2 (n + m))]

printfn "The array = %A" (change n m)