namespace NaughtsAndCrosses
{
    public enum Token
    {
        [EnumDescription(" ")]
        E = -1,
        [EnumDescription("X")]
        X = 0,
        [EnumDescription("O")]
        O
    }

    public class Position
    {
        static int BoardSize = 3;

        /// <summary>
        /// Constructor
        /// </summary>
        public Position()
        {
            board = new Token[BoardSize, BoardSize];
        }

        public Position(Position other)
        {
            board = new Token[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; ++i)
                for (int j = 0; j < BoardSize; ++j)
                    this.SetAt(other.GetAt(new Vector2(i, j)), new Vector2(i, j));
        }

        public int GetSize() { return BoardSize; }

        /// <summary>
        /// Accessor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Token GetAt(Vector2 pos)
        {
            return board[pos.x, pos.y];
        }

        /// <summary>
        /// Mutator
        /// </summary>
        /// <param name="t"></param>
        /// <param name="pos"></param>
        public void SetAt(Token t, Vector2 pos)
        {
            board[pos.x, pos.y] = t;
        }

        /// <summary>
        /// Set all board to empty
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < BoardSize; ++i)
                for (int j = 0; j < BoardSize; ++j)
                    board[i, j] = Token.E;
        }

        /// <summary>
        /// Own version of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Needs fixing. E should be " "
            string rows = string.Empty;

            for (int i = 0; i < BoardSize; ++i)
            {
                if (i != 0)
                {
                    rows += "\n";
                    for (int j = 0; j < BoardSize - 1; ++j)
                        rows += "──┼─";
                    rows += "─\n";
                }
                rows += board[i, 0].ToDescription() + " │ ";
                for (int j = 1; j < BoardSize - 1; ++j)
                {
                    rows += board[i, j].ToDescription() + " │ ";
                }
                rows += board[i, BoardSize - 1].ToDescription();
            }
            return rows;
            //return "  │   │  \n──┼───┼──\n  │   │  \n──┼───┼──\n  │   │  ";
        }


        /// <summary>
        /// Check if the requested token has won this position
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TokenWins(Token token)
        {
            int[] tokensInRowCount = new int[BoardSize];
            int[] tokensInColCount = new int[BoardSize];

            for (int i = 0; i < BoardSize; ++i)
                for (int j = 0; j < BoardSize; ++j)
                    if (board[j, i] == token)
                    {
                        tokensInRowCount[i]++;
                        tokensInColCount[j]++;
                        if (tokensInRowCount[i] == BoardSize || tokensInColCount[j] == BoardSize)
                            return true;
                    }
            // Check diagonals
            if (board[BoardSize / 2, BoardSize / 2] == token)
            {
                // Ok, then check all diagonals
                if (IsDiagonal(token, true)) return true;
                else if (IsDiagonal(token, false)) return true;
            }
            return false;
        }

        /// <summary>
        /// Check diagonals
        /// </summary>
        /// <param name="isLTR">Is left to right diagonal</param>
        /// <returns></returns>
        private bool IsDiagonal(Token token, bool isLTR)
        {
            for (int i = 0; i < BoardSize; ++i)
                if (board[(isLTR ? i : (BoardSize - 1) - i), i] != token) return false;
            return true;
        }

        /// <summary>
        /// Check if there are moves left
        /// </summary>
        /// <returns></returns>
        public bool AnyMovesLeft()
        {
            foreach (Token t in board)
                if (t == Token.E) return true;
            return false;
        }
        

        private Token[,] board;
    }
}
