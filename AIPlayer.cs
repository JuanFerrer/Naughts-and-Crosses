using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaughtsAndCrosses
{
    class AIPlayer : Player
    {
        AIPlayer()
        {
            IsAI = true;
        }

        /// <summary>
        /// Calculate best move
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetPlay()
        {
            Vector2 move;
            Position board = GameManager.Board;
            Node baseNode = new Node(board, new Node());
            baseNode.PopulateChildren();


            return move;
        }

        /// <summary>
        /// Recursively, look for empty spaces, copy the passed board and put a piece in that one
        /// </summary>
        /// <param name="board"></param>
        private void AddChildPosToTree(Position board, Node parentNode)
        {
            Position localBoard = new Position(board);



            positionTree.Add(new Node(localBoard, parentNode));



            for (int i = 0; i < board.GetSize(); ++i)
            {
                for (int j = 0; j < board.GetSize(); ++j)
                {
                    if (board.GetAt(new Vector2(i, j)) == Token.E)
                    {
                        localBoard.SetAt(GetToken(), new Vector2(i, j));
                        isMyTurn = !isMyTurn;   // Toggle isMyTurn to make sure, next simulation is for opponent
                        AddChildPosToTree(localBoard, isMyTurn);
                    }
                }
            }
        }
    }
}
