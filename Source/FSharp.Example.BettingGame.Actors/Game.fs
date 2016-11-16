module Actors.Game

open Orleankka
open Orleankka.CSharp
open Orleankka.FSharp
open Domain.Game
open Contracts

type Event =
   | ScoreChanged of Score
   | GameStatusChanged of Status
   | OddsChanged of Odds

type State = {
   Status: Status
   Score: Score
   Odds: Odds
   Bets: Bet[]
}

let handleCommand state command = 
   match command with
   | ChangeScore(home,away) -> ScoreChanged({ Home = home; Away = away })
   | _                      -> failwith ""


[<ActorType("game")>]
type GameActor() =
   inherit Actor<obj>()

   override this.Receive message = task {      
      match message with
      | :? Command as command -> return nothing
      | :? Query as query     -> return nothing
      | _                     -> return nothing
   }