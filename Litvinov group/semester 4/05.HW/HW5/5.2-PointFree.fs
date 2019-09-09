module PointFree

let mult a b = a * b

let func x l = List.map (fun y -> y * x) l

let func'1 x = List.map (fun y -> y * x)

let func'2 x = List.map (fun y -> mult y x)

let func'3 x = List.map (mult x)

let func'4 = List.map << mult