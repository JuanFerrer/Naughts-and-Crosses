using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaughtsAndCrosses
{
    class Node
    {
        Position pos;
        List<Node> children;
        Node parent;

        public Node()
        { }
        public Node(Position newPos, Node newParent)
        {
            pos = newPos;
            parent = newParent;
        }

        /// <summary>
        /// Recursively, look for empty spaces, copy the passed board and put a piece in that one
        /// </summary>
        public void PopulateChildren()
        {
            Position localBoard = new Position(pos);
            for (int i = 0; i < pos.GetSize(); ++i)
            {
                for (int j = 0; j < pos.GetSize(); ++j)
                {
                    if (pos.GetAt(new Vector2(i, j)) == Token.E)
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
