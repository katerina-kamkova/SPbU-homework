module PrimeNumbers
    
/// Check whether the number is prime
let isPrime number = 
    let max = number |> double |> sqrt |> int
    let length = seq {2 .. max} |> Seq.filter (fun x -> number % x = 0) |> Seq.length
    if length = 0 && number <> 1 then true else false

/// Create an infinite sequence of prime numbers
let primeNumbers = Seq.initInfinite int |> Seq.filter isPrime