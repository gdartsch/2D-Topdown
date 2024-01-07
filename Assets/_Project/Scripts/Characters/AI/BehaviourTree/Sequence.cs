using System.Collections.Generic;

namespace MatchaIsSpent.BehaviourTree
{
    /// <summary>
    /// The sequence node.
    /// </summary>
    public class Sequence : Node
    {
        /// <summary>
        /// The constructor for the sequence node.
        /// </summary>
        public Sequence() : base() { }

        /// <summary>
        /// The constructor for the sequence node.
        /// <paramref name="children"/> The children of the node.
        /// </summary>
        /// <param name="children"></param>
        public Sequence(List<Node> children) : base(children) { }

        override public NodeState Evaluate()
        {
            bool anyChildRunning = false;

            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}