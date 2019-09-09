module ``Check whether the string is a palindrome``

open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit
open Palindrome

[<TestClass>]
type PalindromeTests () = 

    [<TestMethod>]
    member this.``Check empty string`` () =
        isPalindrome "" |> should be True

    [<TestMethod>]
    member this.``Check one char string`` () =
        isPalindrome "k" |> should be True

    [<TestMethod>]
    member this.``Check palindrome`` () =
        isPalindrome "detartrated" |> should be True

    [<TestMethod>]
    member this.``Check not palindrome`` () =
        isPalindrome "123421" |> should be False