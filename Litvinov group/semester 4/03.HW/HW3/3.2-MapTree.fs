module MapTree

/// Tree structure
type Tree<'a> = 
    | Empty
    | Tree of 'a * Tree<'a> * Tree<'a>

/// Func that applies given function to each element in binary tree
let rec mapTree func (tree: Tree<int>) =
    match tree with
    | Empty -> Empty
    | Tree (node, left, right) -> Tree (func node, mapTree func left, mapTree func right)