using System.Collections.Generic;

namespace NaughtsAndCrosses
{
    static class GameManager
    {
        //*******************************************************
        // Manager properties
        //*******************************************************
        static public bool InGame { get; private set; }
        static public bool Player1Wins { get; private set; }
        static public bool Player2Wins { get; private set; }
        static public bool IsTie { get; private set; }
        static public bool IsSinglePlayer { get; private set; }
        const int PlayerAmount = 2;
        static public int CurrentPlayer { get; private set; }
        static public int Winner { get; set; }

        //*******************************************************
        // Logic
        //*******************************************************
        static Player[] players;

        static public Position Board;

        /// <summary>
        /// Manager setup
        /// </summary>
        /// <returns></returns>
        static public bool Init()
        {
            InGame = false;
            Player1Wins = false;
            Player2Wins = false;
            IsTie = false;
            IsSinglePlayer = true;
            CurrentPlayer = 0;
            Winner = -1;

            players = new Player[PlayerAmount];

            return true;
        }

        /// <summary>
        /// Start new game
        /// </summary>
        static public void StartGame()
        {
            InGame = true;
            Board = new Position();
            Board.Reset();

            for (int i = 0; i < PlayerAmount; ++i)
            {
                if (i == 0)
                    players[i] = new Player();
                else
                    players[i] = new AIPlayer();
                players[i].Init();
                players[i].SetToken((Token)i);
                players[i].SetNumber(i);
            }
        }

        /// <summary>
        /// Run one turn. Needs to happen in a loop
        /// </summary>
        static public void TurnCycle()
        {
            Menu.ShowProgress();
            DoPlayerMovement(players[CurrentPlayer].GetPlay());

            //else   //AI
            //{
            //    Console.WriteLine("The computer will make its movement");
            //    Console.ReadLine();

            //    ApplyPlayerMovement(FindMovement(), isPlayer1, ref turn);
            //}

            //Win conditions
            CheckIfWon();
            if (!Board.AnyMovesLeft())
                InGame = false;
        }

        /// <summary>
        /// Receive player input and decide whether the movement can be made
        /// </summary>
        static public void DoPlayerMovement(Vector2 move)
        {
            if (Board.GetAt(move) == Token.E)
            {
                Board.SetAt(players[CurrentPlayer].GetToken(), move);
                players[CurrentPlayer].AddTokenPos(move);
                IncreasePlayer();
            }
            else Menu.RequestValidMove();
        }

        /// <summary>
        /// Increase player turn, but enforce to stay under player amount
        /// </summary>
        static public void IncreasePlayer()
        {
            CurrentPlayer++;
            if (CurrentPlayer == PlayerAmount) CurrentPlayer = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //static int FindMovement()
        //{                        //Fix and simplify (use a method)

        //    //Monte Carlo Tree Search
        //    //Check if player1 is about to win
        //    List<int> combinationToBreak = new List<int>();
        //    int selectedMove = 0;
        //    for (int i = 0; i < winningCombinations.Count; i++)
        //    {
        //        bool movementFound = false;
        //        int counterPlayer1 = 0;
        //        combinationToBreak = new List<int>(winningCombinations[i]);

        //        //Make winning move before blocking player


        //        foreach (int tokenPos in player1TokenPos)
        //        {
        //            if (combinationToBreak.Contains(tokenPos) && !player2TokenPos.Contains(tokenPos))
        //            {
        //                combinationToBreak.Remove(tokenPos);
        //                counterPlayer1++;
        //                if (counterPlayer1 == 2 && player2TokenPos.Contains(combinationToBreak[0])) //The movement won't end the game
        //                {
        //                    winningCombinations.Remove(winningCombinations[i]);
        //                }
        //                else if (counterPlayer1 == 2 && !player2TokenPos.Contains(combinationToBreak[0]))   //The movement will end the game
        //                {
        //                    movementFound = true;
        //                    winningCombinations.Remove(winningCombinations[i]);
        //                    break;
        //                }
        //            }
        //        }
        //        if (movementFound)
        //        {
        //            selectedMove = combinationToBreak[0];
        //            break;
        //        }

        //        else
        //        {
        //            List<int> possibleMovesLeft = new List<int>();
        //            for (int j = 0; j < validInputArray.Length; j++)
        //            {
        //                if (!player1TokenPos.Contains(validInputArray[j]) && !player2TokenPos.Contains(validInputArray[j]))
        //                {
        //                    possibleMovesLeft.Add(validInputArray[j]);
        //                }
        //            }
        //            Random rnd = new Random();
        //            selectedMove = possibleMovesLeft[rnd.Next(1, possibleMovesLeft.Count)];
        //        }
        //    }
        //    return selectedMove;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        static void MakePlayerWinner(Player p)
        {
            InGame = false;
            Winner = p.GetNumber();
        }

        static void CheckIfWon()
        {
            foreach (Player p in players)
            {
                if (p.IsWinner())
                {
                    MakePlayerWinner(p);
                    return;
                }
            }

            //for (int i = 0; i < winningCombinations.Count; i++)
            //{
            //    int counterPlayer1 = 0;
            //    foreach (int tokenPos in player1TokenPos)
            //    {
            //        if (winningCombinations[i].Contains(tokenPos))
            //        {
            //            counterPlayer1++;
            //        }
            //    }
            //    int counterPlayer2 = 0;
            //    foreach (int tokenPos in player2TokenPos)
            //    {
            //        if (winningCombinations[i].Contains(tokenPos))
            //        {
            //            counterPlayer2++;
            //        }
            //    }
            //    if (counterPlayer1 == 3)
            //    {
            //        player1Wins = true;
            //        player2Wins = false;
            //        inGame = false;
            //        break;
            //    }
            //    else if (counterPlayer2 == 3)
            //    {
            //        player1Wins = false;
            //        player2Wins = true;
            //        inGame = false;
            //        break;
            //    }
            //}
        }

        static public void ChangeGameMode() { IsSinglePlayer = !IsSinglePlayer; }
    }
}
