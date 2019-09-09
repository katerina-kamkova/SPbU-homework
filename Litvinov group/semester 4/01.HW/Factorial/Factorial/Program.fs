let rec factorialCount x acc = 
    if x < 0 then
        -1
    else if x = 0 || x = 1 then
        acc
    else
        factorialCount
            (x - 1)
            (acc * x)

let factorial x = (factorialCount x 1)

printfn "Factorial of -7 = %i, what means you can`t count (-7)!" (factorial -7)
printfn "Factorial of 0 = %i" (factorial 0)
printfn "Factorial of 1 = %i" (factorial 1)
printfn "Factorial 0f 2 = %i" (factorial 2)
printfn "Factorial of 3 = %i" (factorial 3)
printfn "Factorial of 4 = %i" (factorial 4)
printfn "Factorial of 10 = %i" (factorial 10)

printfn ""
printf "Try your number: "
let number = System.Console.ReadLine() |> System.Int32.Parse
printfn "Factorial of %i = %i" number (factorial number)