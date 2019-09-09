module NetTests

open NUnit.Framework
open FsUnit

open System
open Net
open Computer

type MyRandom (x) = 
   inherit Random()
   override is.NextDouble() = x

let compResistance = Map["Windows XP", 1.0; "Mac OS", 0.5; "Linux", 0.0]

let computers1: List<Computer> = [new Computer("1", "Windows XP", true)
                                  new Computer("2", "Windows XP", false)
                                  new Computer("3", "Windows XP", false)]

let checkComputers1: List<Computer> = [new Computer("1", "Windows XP", true)
                                       new Computer("2", "Windows XP", true)
                                       new Computer("3", "Windows XP", true)]

let matrix1: List<List<bool>> = [[false; true; true]
                                 [true; false; true]
                                 [true; true; false]]

let computers2: List<Computer> = [new Computer("1", "Linux", false)
                                  new Computer("2", "Linux", true)
                                  new Computer("3", "Linux", false)]

let checkComputers2: List<Computer> = [new Computer("1", "Linux", false)
                                       new Computer("2", "Linux", true)
                                       new Computer("3", "Linux", false)]

let matrix2: List<List<bool>>  = [[false; true; false]
                                  [true; false; true]
                                  [false; true; false]]

let computers3: List<Computer> = [new Computer("1", "Windows XP", true)
                                  new Computer("2", "Windows XP", false)
                                  new Computer("3", "Mac OS", false)
                                  new Computer("4", "Linux", false)]

let checkComputers3: List<Computer> = [new Computer("1", "Windows XP", true)
                                       new Computer("2", "Windows XP", false)
                                       new Computer("3", "Mac OS", false)
                                       new Computer("4", "Linux", false)]

let matrix3: List<List<bool>>  = [[false; false; true; true]
                                  [false; false; false; true]
                                  [true; false; false; false]
                                  [true; true; false; false]]

let computers4: List<Computer> = [new Computer("1", "Windows XP", true)
                                  new Computer("2", "Windows XP", false)
                                  new Computer("3", "Windows XP", false)
                                  new Computer("4", "Windows XP", false)]

let checkComputers4_1: List<Computer> = [new Computer("1", "Windows XP", true)
                                         new Computer("2", "Windows XP", true)
                                         new Computer("3", "Windows XP", false)
                                         new Computer("4", "Windows XP", false)]

let checkComputers4_2: List<Computer> = [new Computer("1", "Windows XP", true)
                                         new Computer("2", "Windows XP", true)
                                         new Computer("3", "Windows XP", true)
                                         new Computer("4", "Windows XP", false)]
       
let checkComputers4_3: List<Computer> = [new Computer("1", "Windows XP", true)
                                         new Computer("2", "Windows XP", true)
                                         new Computer("3", "Windows XP", true)
                                         new Computer("4", "Windows XP", true)]

let matrix4: List<List<bool>>  = [[false; true; false; false]
                                  [true; false; true; false]
                                  [false; true; false; true]
                                  [false; false; true; false]]

[<Test>]
let ``Check when possibility is 1. All computers should be infected in 1 iteration`` () =
    let net = new Net(computers1, matrix1, compResistance)
    net.Start(1, 1, new Random())
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) computers1 checkComputers1 |> should equal 0

[<Test>]
let ``Check when possibility is 0. Uninfected computers shouldn`t be infected in 1000 iterations`` () =
    let net = new Net(computers2, matrix2, compResistance)
    net.Start(1000, 1000, new Random ())
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) computers2 checkComputers2 |> should equal 0

[<Test>]
let ``Check in more complicated case. In 1 iteration 3rd comp should be infected`` () =
    let net = new Net(computers3, matrix3, compResistance)
    net.Start(1, 1000, MyRandom(0.999))
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) computers3 checkComputers3 |> should equal 0

[<Test>]
let ``Computers will be infected one by one. Step 1`` () =
    let net = new Net(computers4, matrix4, compResistance)
    net.Start(1, 1000, new Random())
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) computers4 checkComputers4_1 |> should equal 0

[<Test>]
let ``Computers will be infected one by one. Step 2`` () =
    let net = new Net(checkComputers4_1, matrix4, compResistance)
    net.Start(1, 1000, new Random())
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) checkComputers4_1 checkComputers4_2 |> should equal 0

[<Test>]
let ``Computers will be infected one by one. Step 3`` () =
    let net = new Net(checkComputers4_2, matrix4, compResistance)
    net.Start(1, 1000, new Random())
    List.compareWith (fun (comp1: Computer) (comp2: Computer) -> 
        if (comp1.Name = comp2.Name && comp1.OS = comp2.OS && comp1.Infected = comp2.Infected) 
        then 0 else 1) checkComputers4_2 checkComputers4_3 |> should equal 0