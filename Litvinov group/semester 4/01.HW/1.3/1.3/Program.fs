open System

let reverse list =
    let rec reverseFunc list acc =
        match list with
            | [] -> acc
            | head :: tail -> reverseFunc tail (head :: acc)
    reverseFunc list []

printfn "%A reversed %A" [] (reverse [])
printfn "%A reversed %A" [1 .. 13] (reverse [1 .. 13])
printfn "%A reversed %A" [3; 9; -2; 111; 34] (reverse [3; 9; -2; 111; 34])

printfn ""
printf "Enter list elements using 'Space': "
let elements = Console.ReadLine()

let length = elements.Length / 2 + elements.Length % 2
let list = [for i in 0 .. length - 1 -> elements.Chars(i * 2)]

printfn ""
printfn "Your list is %A" list
printfn "Reversed list: %A" (reverse list)