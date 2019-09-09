module Net

open System
open Computer

/// Type representing computers net
/// compList - list of computers
/// compMatrix - shows connection between computers
/// infoDictionary - list of pairs (os, infectPossibility)
type Net(compList: List<Computer>, compMatrix: List<List<bool>>, infoDictionary: Map<string, float>) =
    /// Number of computers in the net
    let compNumber = compList.Length

    /// One 'step' when computers interact and can get infected
    let oneIteration (rand: Random) =
        for i in 0 .. compNumber - 1 do
            let curComp = compList.Item i
            if not curComp.Infected then
                for j in 0 .. compNumber - 1 do
                    if (((compMatrix.Item i).Item j) && (compList.Item j).Infected && 
                        ((rand.NextDouble()) < (infoDictionary.Item(curComp.OS))) && 
                        (not (compList.Item j).JustInfected))
                    then (compList.Item i).JustInfected <- true
                         (curComp.Infected <- true)

        compList |> Seq.iter (fun x -> if x.JustInfected then x.JustInfected <- false)

    /// Show current situation
    let check () =
        for i in 0 .. compNumber - 1 do
            let comp = compList.Item i
            printfn "%s   %s   %b" comp.Name comp.OS comp.Infected
        printfn ""

    /// For testing
    member this.TestCheck () = compList

    /// Runs the process
    /// iterNum - number of iterations
    /// showFrequency - how often should display situation
    /// random - number that presents possibility of infection
    member this.Start (iterNum, showFrequency, rand) =
        for i in 1 .. iterNum do
            oneIteration(rand)
            if (showFrequency <> 0) && (i % showFrequency = 0) then check()
        check()