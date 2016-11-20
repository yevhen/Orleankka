module Actors.Game

open Orleankka
open Orleankka.FSharp
open Domain.Game
open Contracts
open System

type GameActor() =
   inherit Actor<obj>()  
   
   let mutable _state = GameState.Zero()    

   override this.Receive message = task {      
      match message with
      | :? Command as command -> let event = handleCommand(_state, command)
                                 _state <- apply(_state, event)
                                 return nothing
      
      | :? Query as query     -> return response(_state)
      
      | _                     -> return nothing
   }

   interface IGameActor