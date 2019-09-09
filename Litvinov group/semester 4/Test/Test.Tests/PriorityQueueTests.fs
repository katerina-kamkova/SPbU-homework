module PriorityQueueTests

open NUnit.Framework
open FsUnit
open PriorityQueue

let mutable queue = new PriorityQueue<string>()

[<SetUp>]
let ``Init``() = queue <- new PriorityQueue<string>()

[<Test>]
let ``Check isEmpty on empty queue`` () =
    queue.IsEmpty () |> should equal true

[<Test>]
let ``Check isEmpty on not empty queue`` () =
    queue.Enqueue 1 "1"
    queue.IsEmpty () |> should equal false

[<Test>]
let ``Enqueue some elements in ascending priority order`` () =
    queue.Enqueue 4 "C"
    queue.Enqueue 3 "B"
    queue.Enqueue 2 "A"
    queue.Dequeue () |> should equal (2, "A")
    queue.Dequeue () |> should equal (3, "B")
    queue.Dequeue () |> should equal (4, "C")

[<Test>]
let ``Enqueue some elements in descending priority order`` () =
    queue.Enqueue 2 "A"
    queue.Enqueue 3 "B"
    queue.Enqueue 4 "C"
    queue.Dequeue () |> should equal (2, "A")
    queue.Dequeue () |> should equal (3, "B")
    queue.Dequeue () |> should equal (4, "C")

[<Test>]
let ``Enqueue some elements in ascending priority order with repeating priorities`` () =
    queue.Enqueue 4 "D"
    queue.Enqueue 3 "B"
    queue.Enqueue 3 "C"
    queue.Enqueue 2 "A"
    queue.Dequeue () |> should equal (2, "A")
    queue.Dequeue () |> should equal (3, "B")
    queue.Dequeue () |> should equal (3, "C")
    queue.Dequeue () |> should equal (4, "D")

[<Test>]
let ``Enqueue some elements in descending priority order with repeating priorities`` () =
    queue.Enqueue 2 "A"
    queue.Enqueue 3 "B"
    queue.Enqueue 3 "C"
    queue.Enqueue 4 "D"
    queue.Dequeue () |> should equal (2, "A")
    queue.Dequeue () |> should equal (3, "B")
    queue.Dequeue () |> should equal (3, "C")
    queue.Dequeue () |> should equal (4, "D")

[<Test>]
let ``Big mixed test`` () =
    queue.Enqueue 3 "you"
    queue.Enqueue 43 "speak"
    queue.Enqueue -2 "Google"
    queue.Enqueue 0 "before"
    queue.Enqueue 29 "you"
    queue.Enqueue 3 "tweet"
    queue.Enqueue 13 "think"
    queue.Enqueue 7 "new"
    queue.Enqueue 19 "before"
    queue.Enqueue 3 "is"
    queue.Dequeue () |> should equal (-2, "Google")
    queue.Dequeue () |> should equal (0, "before")
    queue.Dequeue () |> should equal (3, "you")
    queue.Dequeue () |> should equal (3, "tweet")
    queue.Dequeue () |> should equal (3, "is")
    queue.Dequeue () |> should equal (7, "new")
    queue.Dequeue () |> should equal (13, "think")
    queue.Dequeue () |> should equal (19, "before")
    queue.Dequeue () |> should equal (29, "you")
    queue.Dequeue () |> should equal (43, "speak")


[<Test>]
let ``Exception must be thrown if user tries to dequeue empty queue`` () =
    (fun () -> queue.Dequeue() |> ignore) |> should throw (typeof<System.InvalidOperationException>)

[<Test>]
let ``Check on other element types`` () =
    let mutable intQueue = new PriorityQueue<int>() 
    intQueue.Enqueue 3 1
    intQueue.Enqueue 1 3
    intQueue.Enqueue 2 2
    intQueue.Dequeue () |> should equal (1, 3)
    intQueue.Dequeue () |> should equal (2, 2)
    intQueue.Dequeue () |> should equal (3, 1)

[<Test>]
let ``Check count on one of tests written before`` () =
    queue.Count () |> should equal 0
    queue.Enqueue 4 "D"
    queue.Count () |> should equal 1
    queue.Enqueue 3 "B"
    queue.Count () |> should equal 2
    queue.Enqueue 3 "C"
    queue.Count () |> should equal 3
    queue.Enqueue 2 "A"
    queue.Count () |> should equal 4
    queue.Dequeue () |> ignore
    queue.Count () |> should equal 3
    queue.Dequeue () |> ignore
    queue.Count () |> should equal 2
    queue.Dequeue () |> ignore
    queue.Count () |> should equal 1
    queue.Dequeue () |> ignore
    queue.Count () |> should equal 0