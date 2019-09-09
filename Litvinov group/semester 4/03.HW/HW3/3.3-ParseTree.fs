module ParseTree

/// Struct for parse tree node
type ParseTree =
    | Add of ParseTree * ParseTree
    | Subtract of ParseTree * ParseTree
    | Multiply of ParseTree * ParseTree
    | Divide of ParseTree * ParseTree
    | Number of double

/// Calculate given ParseTree
let rec calculate (tree: ParseTree) = 
    let delta = 1e-10
    match tree with
    | Add (left, right) -> (calculate left) + (calculate right)
    | Subtract (left, right) -> (calculate left) - (calculate right)
    | Multiply (left, right) -> (calculate left) * (calculate right)
    | Divide (left, right) -> let rightValue = calculate right
                              if rightValue < delta then raise (System.DivideByZeroException "You can`t divide by zero")
                              else (calculate left) / rightValue
    | Number number -> number