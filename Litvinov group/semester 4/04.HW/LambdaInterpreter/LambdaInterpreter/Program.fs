module Main

    /// Definition of lambda-term
    type Term = 
        | Variable of string
        | Abstraction of string * Term
        | Application of Term * Term

    /// Func implementing beta reduction with normal strategy
    let betaReduction (expression: Term) = 
    
        /// Get minimum available rename number for term
        let rec getMinRenameNumber term renameNumber = 
            match term with
            | Variable(var) -> if var.Length >= 7 &&
                                  var.[(var.Length - 6)..(var.Length - 1)] = "rename" && 
                                  (var.[0..(var.Length - 7)] |> int) >= renameNumber
                               then ((var.[0..(var.Length - 7)] |> int) + 1)
                               else renameNumber
            | Abstraction(var, term) -> getMinRenameNumber term (getMinRenameNumber (Variable(var)) renameNumber)
            | Application(left, right) -> getMinRenameNumber right (getMinRenameNumber left renameNumber)

        /// Check whether variable isn`t free in term
        let rec notFree var term = 
            match term with
            | Variable(str) when str = var -> false
            | Variable(_) -> true
            | Abstraction(str, _) when str = var -> true
            | Abstraction(_, insideTerm) -> notFree var insideTerm
            | Application(left, right) -> (notFree var left) && (notFree var right)

        /// Change old variable on new variable
        let rec substitution oldVar term newTerm renameNumber =
            match term with
            | Variable(str) when str = oldVar -> (newTerm, renameNumber)
            | Variable(_) -> (term, renameNumber)
            | Application(left, right) -> let (newLeft, newNumber) = substitution oldVar left newTerm renameNumber
                                          let (newRight, newNewNumber) = substitution oldVar right newTerm newNumber
                                          (Application(newLeft, newRight), newNewNumber)
            | Abstraction(str, _) when str = oldVar -> (term, renameNumber)
            | Abstraction(str, deepTerm) ->
                if (notFree oldVar deepTerm) || (notFree str newTerm)
                then (Abstraction(str, Application(Abstraction(oldVar, deepTerm), newTerm)), renameNumber)
                else let newVar = (renameNumber |> string) + "rename"
                     let (subTerm, newNumber) = substitution str deepTerm (Variable(newVar)) (renameNumber + 1)
                     let (subSubTerm, newNewNumber) = substitution oldVar subTerm newTerm newNumber
                     (Abstraction(newVar, subSubTerm), newNewNumber)

        /// Implement beta reduction
        let rec betaReductionRecursive (term: Term) (renameNumber: int) = 
            match term with 
            | Variable(_) -> (term, renameNumber)
            | Abstraction(var, insideTerm) -> let (newTerm, newNumber) = betaReductionRecursive insideTerm renameNumber
                                              (Abstraction(var, newTerm), newNumber)
            | Application(Abstraction(var, deepTerm), insideTerm) -> let (newTerm, newNumber) = substitution var deepTerm insideTerm renameNumber
                                                                     betaReductionRecursive newTerm newNumber
            | Application(left, right) -> let (newLeft, newNumber) = betaReductionRecursive left renameNumber
                                          match newLeft with
                                          | Abstraction(_, _) -> betaReductionRecursive (Application(newLeft, right)) newNumber
                                          | _ -> let (newRight, newNewNumber) = betaReductionRecursive right newNumber
                                                 (Application(newLeft, newRight), newNewNumber)

        let (answer, _) = betaReductionRecursive expression (getMinRenameNumber expression 0)
        answer