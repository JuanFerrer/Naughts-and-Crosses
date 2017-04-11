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
        Vector2 moveMade;   // Mode that led to this position
        double value;          // How good this position is.Each winning and losing position from children list respectively increases or decreases the value of this node. 
        int depth;          // Take into account how many moves have been take to get here

        public Node()
        {
        }

        /// <summary>
        /// Deep copy. We need an all new position
        /// </summary>
        /// <param name="newPos"></param>
        /// <param name="newParent"></param>
        public Node(Position newPos, Vector2 move, int newDepth, Node newParent)
        {
            pos = new Position(newPos);
            children = new List<Node>();
            parent = newParent;
            moveMade = move;
            value = 0.0;
            depth = newDepth;
        }

        /// <summary>
        /// Recursively, look for empty spaces, copy the passed board and put a piece in that one
        /// </summary>
        public void PopulateChildren(Token turnToken, Token originalToken)
        {
            Position localBoard = new Position(pos);
            // If it's a winning position for the player that requested the search stop
            if (pos.TokenWins(originalToken))
            {
                value = 1.0 / depth;
                return;
            }
            if (pos.TokenWins(originalToken == Token.O ? Token.X : Token.O))
            {
                value = -1.0 / depth;
                return;
            }
            for (int i = 0; i < pos.GetSize(); ++i)
            {
                for (int j = 0; j < pos.GetSize(); ++j)
                {
                    // Look for empty spaces
                    if (pos.GetAt(new Vector2(i, j)) == Token.E)
                    {
                        // When found, temporarily set to token...
                        localBoard.SetAt(turnToken, new Vector2(i, j));
                        // add it to the children list...
                        children.Add(new Node(localBoard, new Vector2(i, j), depth + 1, this));
                        // and change it back to empty, to keep searching
                        localBoard.SetAt(Token.E, new Vector2(i, j));
                    }
                }
            }
            foreach (Node child in children)
                // Now, do the same for each children of this node
                child.PopulateChildren(turnToken == Token.O ? Token.X : Token.O, originalToken);
        }

        /// <summary>
        /// Check if children are winning or losing positions and modify value accordingly
        /// </summary>
        /// <param name="token">The token from the requesting player. The token whose winning position would mean +1</param>
        public void CalculateNodeValue(Token token)
        {
            // If we still have children, do the same for them          
            if (children.Count != 0)
            {
                foreach (Node child in children)
                {
                    child.CalculateNodeValue(token);
                    value += child.value;
                }
            }
            //// Otherwise, we're at the end of a branch. So, calculate this position
            if (pos.TokenWins(token))
                value = 1.0 / depth;
            if (pos.TokenWins(token == Token.O ? Token.X : Token.O))
                value = -1.0 / depth;
            else
                value = 0.5 / depth;
        }

        /// <summary>
        /// Look through children and return move of child with best score
        /// </summary>
        /// <returns></returns>
        public Vector2 GetBestMoveInChildren()
        {
            if (children == null || children.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }

            double maxValue = double.MinValue;
            Vector2 bestMove = new Vector2(-1, -1);
            foreach (Node node in children)
            {
                if (node.value > maxValue)
                {
                    maxValue = node.value;
                    bestMove = node.moveMade;
                }
            }
            return bestMove;
        }
    }
}
