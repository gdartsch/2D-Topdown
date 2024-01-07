using System.Collections.Generic;

namespace MatchaIsSpent.BehaviourTree
{
    /// <summary>
    /// The possible states a node can be in.
    /// </summary>
    public enum NodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    /// <summary>
    /// The base class that all node classes inherit from.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The current state of the node.
        /// </summary>
        protected NodeState state;
        /// <summary>
        /// The parent of the node.
        /// </summary>
        public Node parent;
        /// <summary>
        /// The children of the node.
        /// </summary>
        protected List<Node> children = new List<Node>();

        /// <summary>
        /// The data context of the node.
        /// </summary>
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        /// <summary>
        /// The constructor for the node.
        /// </summary>
        public Node()
        {
            parent = null;
        }

        /// <summary>
        /// The constructor for the node.
        /// <paramref name="children"/> The children of the node.
        /// </summary>
        /// <param name="children"></param>
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        /// <summary>
        /// Attaches a child to the node.
        /// <paramref name="node"/> The child to attach to the node.
        /// </summary>
        /// <param name="node"></param>
        public void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /// <summary>
        /// Detaches a child from the node.
        /// <paramref name="node"/> The child to detach from the node.
        /// </summary>
        /// <param name="node"></param>
        public void Detach(Node node)
        {
            node.parent = null;
            children.Remove(node);
        }

        /// <summary>
        /// This method is called when the node is being evaluated.
        /// </summary>
        /// <returns></returns>
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        /// <summary>
        /// Sets the data of the node.
        /// <paramref name="key"/> The key of the data.
        /// <paramref name="value"/> The value of the data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        /// <summary>
        /// Gets the data of the node.
        /// <paramref name="key"/> The key of the data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = parent;

            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        /// <summary>
        /// Clears the data of the node.
        /// <paramref name="key"/> The key of the data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Node node = parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }
}
