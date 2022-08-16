[<EntryPoint>]
let main args =
    FAkka.Datasource.HelloAkka.run ()

    System.Console.ReadLine() |> ignore

    0
