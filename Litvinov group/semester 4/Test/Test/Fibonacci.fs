module Fibonacci

/// Count the sum of all even fibonacci numbers < 1 000 000
let fibonacci (upperBound: bigint) = 
    let rec countFibonacci (smallerEl: bigint) (biggerEl: bigint) (currentSum: bigint) = 
        let newEl = smallerEl + biggerEl
        if newEl > upperBound then currentSum
        else countFibonacci (smallerEl + biggerEl * (bigint 2)) (smallerEl * (bigint 2) + biggerEl * (bigint 3)) (currentSum + newEl)

    countFibonacci (bigint 1) (bigint 1) (bigint 0)

