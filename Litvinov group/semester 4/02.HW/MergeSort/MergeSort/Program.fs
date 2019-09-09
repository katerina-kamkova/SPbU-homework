module MergeSort

/// Merge 2 lists, acc - is the result
let rec merge (left: List<int>) (right: List<int>) =
    match (left, right) with
    | ([], list) -> list
    | (list, []) -> list
    | (lHead :: lTail, rHead :: rTail) -> if lHead <= rHead then lHead :: (merge lTail right)
                                          else rHead :: (merge left rTail)

/// Get list and sort it
let rec mergeSort (list: List<int>) =
    let length = list.Length
    match length with 
    | 0 | 1 -> list
    | _ -> let list1, list2 = List.splitAt (list.Length / 2) list
           merge (mergeSort list1) (mergeSort list2)
