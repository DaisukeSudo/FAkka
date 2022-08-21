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


open Akka.Persistence.FSharp

module HelloAkkaPersistence =
    let system = System.create "hello-akka-persistence" (Configuration.load ())

    type Command = Put of string | Print
    type Event = Event of string
    type State = State of string list

    let update state event =
        match (state, event) with (State(s), Event(e)) -> State(e :: s)
        
    // for OnRecover
    let apply _ = update
        
    // for OnCommand
    let exec (mailbox: Eventsourced<Command, Event, State>) state cmd =
        match cmd with
        | Put s -> mailbox.PersistEvent (update state) [Event(s)]
        | Print -> match state with State(s) -> String.Join('-', s) |> printfn "State: %s"

    let run () =
        printfn "start"
        let actor =
            spawnPersist
                system
                "string-list"
                {
                    state = State([])
                    apply = apply
                    exec = exec
                }
                []
        actor <! Put "abc"
        actor <! Print
        actor <! Put "def"
        actor <! Print
        printfn "end"
