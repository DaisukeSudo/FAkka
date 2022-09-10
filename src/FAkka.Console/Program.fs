module Program =

    [<EntryPoint>]
    let main args =
        FAkka.Datasource.HelloAkka.run ()
        FAkka.Datasource.HelloAkka.system.Terminate().Wait()
        // FAkka.Datasource.HelloAkkaPersistence.run ()
        // FAkka.Datasource.HelloAkkaPersistence.system.Terminate().Wait()
        0
