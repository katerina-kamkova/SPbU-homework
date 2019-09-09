let fibonacci n =
    let rec fibonacciCount n smallerElement biggerElement i = 
        if n < 1 then
            -1
        else if n = 1 || n = 2 then
            1
        else if i = n - 3 then
            smallerElement + biggerElement
        else
            fibonacciCount n biggerElement (smallerElement + biggerElement) (i + 1)
    fibonacciCount n 1 1 0

printfn "-7 Fibonacci number = %i" (fibonacci -7)
printfn "0 Fibonacci number = %i" (fibonacci 0)
printfn "1 Fibonacci number = %i" (fibonacci 1)
printfn "1 Fibonacci number = %i" (fibonacci 2)
printfn "3 Fibonacci number = %i" (fibonacci 3)
printfn "10 Fibonacci number = %i" (fibonacci 10)

printfn ""
printf "Try your number: "
let number = System.Console.ReadLine() |> System.Int32.Parse
printfn "%i Fibonacci number = %i" number (fibonacci number)