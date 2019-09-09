/// You can enter the string, check whether it`s a palindrome and get printed answer
module Palindrome 

open System

/// Check whether the string is a Palindrome
let rec isPalindrome (string: string) = 
    match string.Length with
    | 0 | 1 -> true
    | _ ->  match string.[0] with
            | el when el = string.[(string.Length - 1)] -> isPalindrome (string.Substring (1, (string.Length - 2)))
            | _ -> false

/// Print results according to the answer
let printResult answer =
    if answer then printfn "String is a palindrome" 
    else printfn "String isn`t a palindrome"

printf "Enter your string:  "
let string = Console.ReadLine()
printResult << isPalindrome <| string