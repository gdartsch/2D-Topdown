using UnityEngine;

namespace MatchaIsSpent.BehaviourTree
{
    /// <summary>
    /// This class is responsible for setting up the behaviour tree.
    /// </summary>
    public abstract class Tree : MonoBehaviour
    {
        /// <summary>
        /// The root node of the tree.
        /// </summary>
        public Node root = null;

        private void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        /// <summary>
        /// Sets up the tree.
        /// </summary>
        /// <returns>
        /// The root node of the tree.
        /// </returns>
        protected abstract Node SetupTree();
    }
}
