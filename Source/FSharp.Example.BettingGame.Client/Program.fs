open System
open System.Reflection
open Orleankka
open Orleankka.CSharp
open Orleankka.FSharp
open Orleankka.FSharp.Configuration
open Contracts

[<EntryPoint>]
let main argv = 

   printfn "Running demo...\n"
   
   let assembly = Assembly.GetExecutingAssembly()   

   let config = ClientConfig.loadFromResource assembly "Client.xml"

   use system = [|typeof<IGameActor>.Assembly|]
                |> ActorSystem.createClient config
                |> ActorSystem.conect 
   
   let game = ActorSystem.actorOf<IGameActor>(system, "football")

   let job() = task {
      do! game <! Command.ChangeGameStatus(0) // InLive
      
      do! game <! Command.ChangeScore(home = 1, away = 2)
   }

   Task.run(job) |> ignore

   Console.ReadLine() |> ignore   
   //ActorSystem.disconnect(system)
   printfn "%A" argv
   0 // return an integer exit code
