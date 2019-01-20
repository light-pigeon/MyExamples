type Price = double

type CakeOptions =
    { Cream : Price option
      Jam : Price option
      Chocolate : Price option
      // ... and 36 more members ...
    }

type IHavingCreamPrice =
    abstract CreamPrice : Price

type IHavingJamPrice =
    abstract JamPrice : Price

type IHavingChocolatePrice =
    abstract ChocolatePrice : Price

type IHavingCreamJamPrices =
    inherit IHavingCreamPrice
    inherit IHavingJamPrice

type IHavingCreamJamChocolatePrices =
    inherit IHavingCreamJamPrices
    inherit IHavingChocolatePrice

type CakeMaker1() =
    interface IHavingCreamJamPrices with
        member __.CreamPrice = 12.0
        member __.JamPrice = 15.0

type CakeMaker2() =
    interface IHavingCreamJamChocolatePrices with
        member __.CreamPrice = 22.0
        member __.JamPrice = 25.0
        member __.ChocolatePrice = 23.0

module CakeOptions =
    let createWithCreamOnly (maker : IHavingCreamPrice) =
        { Cream = Some maker.CreamPrice
          Jam = None
          Chocolate = None
            // ... and 36 more members ...
        }

    let createFull (maker : IHavingCreamJamChocolatePrices) =
        { Cream = Some maker.CreamPrice
          Jam = Some maker.JamPrice
          Chocolate = Some maker.ChocolatePrice
          // ... and 36 more members ...
        }

[<EntryPoint>]
let main _ =
    let cakeOptions1 = CakeOptions.createWithCreamOnly (CakeMaker1())
    let cakeOptions2 = CakeOptions.createFull (CakeMaker2())
    0
