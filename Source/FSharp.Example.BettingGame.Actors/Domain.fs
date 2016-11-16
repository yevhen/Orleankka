module Domain.Game

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
   | Live = 1
   | Finished = 2