/// Functions presenting phone book and implementing all necessary operations
module PhoneBook

open System
open System.IO

/// Following 6 funcs used to be inner funcs of those which are called from eventLoop
/// but were taken out to be tested

/// Check whether there is no such number in the phone book yet
let rec checkUnique number (list: List<string * string>) =
    match list with
    | [] -> true
    | (_, headNumber)::_ when headNumber = number -> false
    | _::tail -> checkUnique number tail

/// Find the place to add contact (sort by name) and add it, if there is no such name yet
let rec insideAddContact (name: string) (number: string) (fst: List<string * string>) (snd: List<string * string>) = 
    match snd with
    | [] -> fst @ [(name, number)]
    | (headName, _)::_ when headName = name -> printfn "There is already the contact with such name, contact was`n added"
                                               fst @ snd
    | (headName, _)::_ when headName > name -> printfn "Contact is added"
                                               fst @ [(name, number)] @ snd
    | (headName, headNumber)::tail -> insideAddContact name number (fst @ [(headName, headNumber)]) tail

/// Find number by name
let rec insideFindNumber name (list: List<string * string>) =
    match list with
    | [] -> ""
    | (headName, headNumber)::_ when headName = name -> headNumber
    | _::tail -> insideFindNumber name tail

/// Find name by number
let rec insideFindName number (list: List<string * string>) =
    match list with
    | [] -> ""
    | (headName, headNumber)::_ when headNumber = number -> headName
    | _::tail -> insideFindName number tail

/// Save all contacts into file with given path in format "name number"
let insideSaveContacts path list =
    let newList = List.map (fun (name, number) -> name + " " + number) list
    try 
        File.WriteAllLines(path, List.toArray newList)
        printfn "All contacts are saved"
    with 
        | :? System.IO.FileNotFoundException -> printfn "Wrong path, contacts aren`t saved"
        | :? System.ArgumentException -> printfn "Path can`t be empty, contacts aren`t saved"

/// Get contacts from file with given path
let insideGetContacts path list =
    try
        let temp = Seq.map (fun (str: string) -> (Array.get (str.Split(" ")) 0, Array.get (str.Split(" ")) 1)) (File.ReadAllLines(path))
        printfn "Contacts are uploaded"
        Seq.toList temp
    with 
        | :? System.IO.FileNotFoundException -> printfn "Wrong path, contacts weren`t uploaded"
                                                list
        | :? System.ArgumentException -> printfn "Path can`t be empty, nothing`s uploaded"
                                         list

/// Print all available options
let printMenue = 
    printfn "Phone book"
    printfn ""
    printfn "Available options:"
    printfn "1 - add contact"
    printfn "2 - find number by name"
    printfn "3 - find name by number"
    printfn "4 - print all current contacts"
    printfn "5 - save current data to file"
    printfn "6 - get data from file"
    printfn "0 - exit"

/// Add contact into phone book
/// Won`t add contact if there are coinciding name or number
let addContact (list: List<string * string>) =
    printf "Enter new name: "
    let name = Console.ReadLine()
    printf "Enter new nummber: "
    let number = Console.ReadLine()
    if name = "" || number = "" then printfn "Names and numbers can`t be empty"
                                     list
    elif checkUnique number list then insideAddContact name number [] list
    else printfn "There is already the contact with such number, contact was`n added"
         list

/// Find number by given name
let findNumber (list: List<string * string>) =
    printf "Enter the name: "
    let number = insideFindNumber (Console.ReadLine()) list
    match number with
    | "" -> printfn "There is no contact with this name" 
    | _ -> printfn "Wanted number is %s" number

/// Find name by given number
let findName (list: List<string * string>) =
    printf "Enter the number: "
    let name = insideFindName (Console.ReadLine()) list
    match name with
    | "" -> printfn "There is no contact with this number"
    | _ -> printfn "Wanted name is %s" name

/// Save all current contacts in the file on given path
let saveContacts (list: List<string * string>) =
    printf "Enter the path: "
    insideSaveContacts (Console.ReadLine()) list

/// Get contacts from given file
/// If the path or data format is incorrect return previous list
let getContacts list = 
    printf "Enter the path: "
    insideGetContacts (Console.ReadLine()) list

/// Start the phone book
let main () =
    /// Main eventLoop that reacts user`s commands
    let rec eventLoop list =
        printfn ""
        printf "Choose availale option: "
        match Console.ReadLine() with 
        | "0" -> printfn "You`ve quited the phone book"
        | "1" -> eventLoop (addContact list)
        | "2" -> findNumber list
                 eventLoop list
        | "3" -> findName list
                 eventLoop list
        | "4" -> printfn "Current contacts: "
                 List.map (fun (name, number) -> printfn "%s   %s" name number) list |> ignore
                 eventLoop list
        | "5" -> saveContacts list
                 eventLoop list
        | "6" -> eventLoop (getContacts list)
        | _ -> printfn "Wrong number, try again"
               eventLoop list

    printMenue
    eventLoop []
