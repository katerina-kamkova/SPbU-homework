module Veb

open System.Net
open System.IO
open System.Text.RegularExpressions

/// Get page by given link, get all links from this page, 
/// return and print number of symbols of every page (from list of links)
let downloadAllPagesByLink (link: string) = 
    
    /// Download page by url
    let fetchAsync (url: string) =
        async {
            try
                let request = WebRequest.Create(url)
                use! response = request.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                let html = reader.ReadToEnd()
                do printfn "%s   -   %d" url html.Length
                return Some html
            with 
                | _ -> do printfn "Can`t reach wanted site :("
                       return None
        }
    
    /// Download pages from start page
    let getLinks link = 
        (new Regex("<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>")).Matches(link) 
        |> Seq.map(fun (x: Match) -> x.Groups.[1].Value |> fetchAsync)
        |> Async.Parallel |> Async.RunSynchronously |> Array.toList

    let pages = link |> fetchAsync |> Async.RunSynchronously 
    match pages with
    | None -> [None]
    | Some value -> pages :: getLinks value

