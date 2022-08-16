namespace FAkka.Datasource

open System
open Akka.FSharp

module HelloAkka =
    let system = System.create "hello-akka" (Configuration.load ())

    let run () =
        printfn "start"
        let hello = printfn "Hello %s"
        let helloActor = spawn system "hello" (actorOf hello)
        helloActor <! "Akka.NET F#"
        printfn "end"
