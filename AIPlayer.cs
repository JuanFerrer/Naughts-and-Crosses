using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaughtsAndCrosses
{
    class AIPlayer : Player
    {
        public AIPlayer()
        {
            IsAI = true;
        }

        /// <summary>
        /// Calculate best move
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetPlay()
        {
            Position board = GameManager.Board;
            Node baseNode = new Node(board, new Vector2(-1, -1), 0, new Node());
            baseNode.PopulateChildren(GetToken(), GetToken());
            CalculateTreeResults(baseNode);
            return GetBestMove(baseNode);
        }

        /// <summary>
        /// Go through the tree and calculate
        /// </summary>
        private void CalculateTreeResults(Node node)
        {
            node.CalculateNodeValue(GetToken());
        }

        /// <summary>
        /// Analyse results and find move with highest chance to win
        /// </summary>
        /// <returns></returns>
        private Vector2 GetBestMove(Node node)
        {
            return node.GetBestMoveInChildren();
        }
    }
}
