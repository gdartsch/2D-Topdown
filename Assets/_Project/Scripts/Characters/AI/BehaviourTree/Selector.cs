using System.Collections.Generic;

namespace MatchaIsSpent.BehaviourTree
{
    /// <summary>
    /// The selector node.
    /// </summary>
    public class Selector : Node
    {
        /// <summary>
        /// The constructor for the selector node.
        /// </summary>
        public Selector() : base() { }

        /// <summary>
        /// The constructor for the selector node.
        /// <paramref name="children"/> The children of the node.
        /// </summary>
        /// <param name="children"></param>
        public Selector(List<Node> children) : base(children) { }

        override public NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
