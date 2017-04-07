using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NaughtsAndCrosses
{
    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int newX, int newY)
        {
            this.x = newX;
            this.y = newY;
        }
    }

    public class Player
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Player()
        {

        }

        /// <summary>
        /// Initialiser function
        /// </summary>
        public void Init()
        {
            tokenPos = new List<Vector2>();
        }

        /// <summary>
        /// Add new position to list
        /// </summary>
        /// <param name="newPos"></param>
        public void AddTokenPos(Vector2 newPos)
        {
            tokenPos.Add(newPos);
        }

        /// <summary>
        /// Check if the player has won
        /// </summary>
        /// <returns></returns>
        public bool IsWinner()
        {
            int size = GameManager.board.GetSize();

            int[] tokensInRowCount = new int[size];
            int[] tokensInColCount = new int[size];

            // Increase the amount of tokens found in a specific row or column
            foreach (Vector2 t in tokenPos)
            {
                tokensInRowCount[t.x]++;
                tokensInColCount[t.y]++;
                
                // If the amount of tokens in a row or column equals the amount of rows, we have a complete row or column
                if (tokensInRowCount[t.x] == size || tokensInColCount[t.y] == size)
                    return true;
            }
            // Check diagonals
            // Check center piece first. If it's not ours, we don't have a diagonal
            if (tokenPos.Contains(new Vector2(size / 2, size / 2)))
            {
                // Ok, then check all diagonals
                for (int i = 0; i < size; ++i)
                {
                    if (!tokenPos.Contains(new Vector2(i, i))) return false;
                    if (!tokenPos.Contains(new Vector2(size - i, i))) return false;
                }
                return true;
            }
            return false;
        }

        public List<Vector2> GetTokenPos() { return tokenPos; }
        public Token GetToken() { return token; }
        public int GetNumber() { return number; }
        public void SetToken(Token newToken) { token = newToken; }
        public void SetNumber(int newNum) { number = newNum; }

        private List<Vector2> tokenPos;
        private Token token;
        private int number;
    }
}
