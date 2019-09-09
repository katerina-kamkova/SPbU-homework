module Computer

/// Type representing computer
/// name - computerName
/// os - operating system
/// infected - current state of the computer
type Computer(name: string, os: string, infected: bool) =
    member val Name = name with get
    member val OS = os with get
    member val Infected = infected with get, set
    member val JustInfected = false with get, set