namespace Contracts
   
open Orleankka

type IGameActor =
   inherit IActor

type Command =
   | ChangeGameStatus of statusCode:int
   | ChangeScore of home:int * away:int      
   | ChangeOdds of marketId:int * home:double * draw:double * away:double
   interface IGameActor

type Query =
   | GetCurrentState
   interface IGameActor