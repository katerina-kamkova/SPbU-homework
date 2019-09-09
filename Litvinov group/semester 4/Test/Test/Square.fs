module Square

/// Create List<string> that contains wanted picture
let createPicture (n: int) =
    /// Create string from only stars
    let rec starLine acc (str: string) =
        if acc > 0 then starLine (acc - 1) (str + "*")
        else str

    /// Create string from spaces and stars on the ends
    let rec line acc (str: string) =
        match acc with
        | 1 -> (str + "*")
        | number when number = n -> line (acc - 1) "*"
        | _ -> line (acc - 1) (str + " ")

    /// Create necessary amount of proper strings
    let rec fillList acc (list: List<string>) =
        match acc with
        | 1 -> (list @ [starLine n ""])
        | number when number = n -> fillList (acc - 1) [(starLine n "")]
        | number when number < 1 -> printfn "%s" "You printed the wrong number, it should be natural"
                                    []
        | _ -> fillList (acc - 1) (list @ [line n ""])
    
    fillList n []

/// Print picture
let rec printPicture (list: List<string>) =
    match list.Length with 
    | 0 -> printfn ""
    | 1 -> printfn "%s" list.Head
    | _ -> printfn "%s" list.Head
           printPicture list.Tail