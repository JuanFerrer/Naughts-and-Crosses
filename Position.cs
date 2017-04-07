using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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

        // Own version of ToString
        public override string ToString()
        {
            // Needs fixing. E should be " "
            string rows = string.Empty;

            for (int i = 0; i < BoardSize; ++i)
            {
                if (i != 0) rows += "\n──┼───┼──\n";
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

        private Token[,] board;
    }
}
