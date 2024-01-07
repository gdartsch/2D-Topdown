using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
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

        protected abstract Node SetupTree();
    }
}
