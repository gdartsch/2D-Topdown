using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchaIsSpent.BehaviourTree;

namespace MatchaIsSpent.AI
{
    public class EnemyBehaviourTree : BehaviourTree.Tree
    {
        public Transform[] waypoints;

        public static float fovRadius = 6f;
        public static float speed = 2f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckPlayerInFOV(transform),
                    new TaskGoToTarget(transform)
                }),
                new TaskPatrol(transform, waypoints)
            });

            return root;
        }
    }
}