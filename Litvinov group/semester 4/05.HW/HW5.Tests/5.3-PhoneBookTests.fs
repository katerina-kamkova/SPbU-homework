module PhoneBookTets

open NUnit.Framework
open FsUnit
open PhoneBook
open NUnit.Framework.Internal
open FsCheck

let checkList = [("Dirk_Gently", "+32-999-888-77-66")
                 ("Doctor_Who", "+8-888-888-88-88")
                 ("Jeck_Sparrow_Captain", "Phones_weren`t_invented_yet")
                 ("Jessica_Jones", "+27-348-435-89-22")
                 ("Peter_Parker", "+87-981-333-22-11")
                 ("Venom", "+43-425-653-34-67")]

[<Test>]
let ``Check insideGetContacts with no path, shouldn`t fall`` () =
    insideGetContacts "" |> ignore

[<Test>]
let ``Check insideGetContacts with wrong path, shouldn`t fall`` () =
    insideGetContacts "hjfbjshncalismx" |> ignore

[<Test>]
let ``Check insideGetContacts with empty file`` () =
    let list = insideGetContacts "Empty.txt" []
    List.isEmpty list |> should equal true

[<Test>]
let ``Check insideGetContacts in normal conditions`` () =
    let list = insideGetContacts "PhoneBook.txt" []
    List.length list |> should equal 6
    List.compareWith (fun (name1, number1) (name2, number2) -> 
        if (name1 = name2 && number1 = number2) then 0 else 1) list checkList |> should equal 0

[<Test>]
let ``Check insideSaveContacts with no path, shouldn`t fall`` () = 
    insideSaveContacts "" []

[<Test>]
let ``Check insideSaveContacts with wrong path, shouldn`t fall`` () = 
    insideSaveContacts "wrgfs" []

[<Test>]
let ``Check insideSaveContacts with empty list`` () = 
    insideSaveContacts "Empty.txt" []
    insideGetContacts "Empty.txt" [] |> should equal (List.Empty)

[<Test>]
let ``Check insideSaveContacts in normal conditions`` () = 
    insideSaveContacts "Empty.txt" checkList
    let list = insideGetContacts "Empty.txt" []
    List.length list |> should equal 6
    List.compareWith (fun (name1, number1) (name2, number2) -> 
        if (name1 = name2 && number1 = number2) then 0 else 1) list checkList |> should equal 0
    insideSaveContacts "Empty.txt" []

[<Test>]
let ``Check checkUnique in empty list should equl true`` () =
    checkUnique "245315" [] |> should equal true

[<Test>]
let ``Check checkUnique on unique number`` () =
    checkUnique "13514" checkList |> should equal true

[<Test>]
let ``Check checkUnique on coinciding number`` () =
    checkUnique "+27-348-435-89-22" checkList |> should equal false

[<Test>]
let ``Check insideAddContact with same name`` () =
    let newList = insideAddContact "Dirk_Gently" "999" [] checkList
    newList.Length |> should equal 6
    List.compareWith (fun (name1, number1) (name2, number2) -> 
        if (name1 = name2 && number1 = number2) then 0 else 1) newList checkList |> should equal 0

[<Test>]
let ``Check insideAddContact in normal conditions`` () =
    let newList = insideAddContact "He_who_must_not_be_named" "+13-666-666-33-33" [] checkList
    newList.Length |> should equal 7
    newList.Item 2 |> should equal ("He_who_must_not_be_named", "+13-666-666-33-33")

[<Test>]
let ``Check insideFindNumber on empty list`` () =
    insideFindNumber "Harry_Potter" [] |> should equal ""

[<Test>]
let ``Check insideFindNumber on not existing name`` () =
    insideFindNumber "Harry_Potter" checkList |> should equal ""

[<Test>]
let ``Check insideFindNumber on existing name`` () =
    insideFindNumber "Venom" checkList |> should equal "+43-425-653-34-67"

[<Test>]
let ``Check insideFindName on empty list`` () =
    insideFindName "+43-425-653-34-67" [] |> should equal ""

[<Test>]
let ``Check insideFindName on not existing number`` () =
    insideFindName "446582756" checkList |> should equal ""

[<Test>]
let ``Check insideFindName on existing number`` () =
    insideFindName "+8-888-888-88-88" checkList |> should equal "Doctor_Who"