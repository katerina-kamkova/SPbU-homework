module Brackets

///Check whether bracket sequence in the string is correct
let checkBrackets (str : string) = 
    let rec check (list : List<char>) (stack : List<char>) =
        if list = [] then
            if stack = [] then true
            else false
        else 
            match list.Head with
            | '(' | ')' | '[' | ']' | '{' | '}' ->
                if stack = [] then check list.Tail [list.Head]
                else
                    let dif = (list.Head |> int) - (stack.Head |> int)
                    if dif = 1 || dif = 2
                    then check list.Tail stack.Tail
                    else match list.Head with
                         | ')' | ']' | '}' -> false
                         | _ -> check list.Tail (list.Head :: stack)
            | _ -> check list.Tail stack

    check (Seq.toList str) []
