using System.Collections.Generic;

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
            IsAI = false;
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
            int size = GameManager.Board.GetSize();

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
                if (IsDiagonal(size, true)) return true;
                else if (IsDiagonal(size, false)) return true;
            }
            return false;
        }

        /// <summary>
        /// Call UI to get play from the user
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 GetPlay()
        {
            return Menu.GetUserPlay();
        }

        public List<Vector2> GetTokenPos() { return tokenPos; }
        public Token GetToken() { return token; }
        public int GetNumber() { return number; }
        public void SetToken(Token newToken) { token = newToken; }
        public void SetNumber(int newNum) { number = newNum; }
        public bool IsAI { get; protected set; }



        /// <summary>
        /// Check diagonals
        /// </summary>
        /// <param name="max">Size of board</param>
        /// <param name="isLTR">Is left to right diagonal</param>
        /// <returns></returns>
        private bool IsDiagonal(int size, bool isLTR)
        {
            for (int i = 0; i < size; ++i)
                if (!tokenPos.Contains(new Vector2(isLTR? i : (size - 1) - i, i))) return false;
            return true;
        }


        private List<Vector2> tokenPos;
        private Token token;
        private int number;
    }
}
