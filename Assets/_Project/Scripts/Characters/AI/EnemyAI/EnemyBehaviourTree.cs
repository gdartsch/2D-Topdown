using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchaIsSpent.BehaviourTree;

namespace MatchaIsSpent.AI
{
    public class EnemyBehaviourTree : BehaviourTree.Tree
    {
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float fovRadius = 6f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float attackRange = 2f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckPlayerInAttackRange(transform, attackRange),
                    new TaskAttack(transform)
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInFOV(transform, fovRadius),
                    new TaskGoToTarget(transform, speed)
                }),
                new TaskPatrol(transform, waypoints, speed)
            });

            return root;
        }
    }
}