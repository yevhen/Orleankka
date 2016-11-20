module Domain.Game

open Contracts

type Score = {
   Home: int
   Away: int     
}

type Odds = {
   MarketId: int
   Home: double
   Draw: double
   Away: double
}

type Side =
   | Home = 0
   | Draw = 1
   | Away = 2   

type Bet = {
   MarketId: int
   Side: Side
   OddsCount: double
}

type Status = 
   | NotStarted = 0
   | InLive = 1
   | Finished = 2

type Event =
   | GameStatusChanged of Status
   | ScoreChanged of Score   
   | OddsChanged of Odds   

type GameState = {
   Status: Status
   Score: Option<Score>
   Odds: Option<Odds>
   Bets: List<Bet>
} with
  static member Zero() = { 
      Status = Status.NotStarted
      Score = None
      Odds = None
      Bets = []
  }

let handleCommand(state, command) = 
   match command with
   | ChangeGameStatus(statusCode)        -> if state.Status = Status.NotStarted then
                                               let newStatus = enum<Status> statusCode
                                               GameStatusChanged(newStatus)
                                            else invalidOp ""       

   | ChangeScore(home,away)              -> let score = { Score.Home = home; Away = away }
                                            ScoreChanged(score)
   
   | ChangeOdds(marketId,home,draw,away) -> let odds = { 
                                               MarketId = marketId; Home = home
                                               Draw = draw; Away = away                                               
                                            }
                                            OddsChanged(odds)

let apply(state, event) = 
   match event with
   | GameStatusChanged(status) -> { state with Status = status }
   | ScoreChanged(score)       -> { state with Score = Some(score) }
   | OddsChanged(odds)         -> { state with Odds = Some(odds) } 