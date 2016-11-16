namespace Contracts
   
type Command =
   | ChangeScore of home:int * away:int   
   | ChangeGameStatus of status:string
   | ChangeOdds of marketId:int * home:double * draw:double * away:double

type Query =
   | GetCurrentState