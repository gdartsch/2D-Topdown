using System.Collections.Generic;
using UnityEngine;
using MatchaIsSpent.BehaviourTree;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// The enemy behaviour tree.
    /// </summary>
    public class EnemyBehaviourTree : BehaviourTree.Tree
    {
        [Header("Enemy Behaviour Tree")]
        [Tooltip("The waypoints the enemy will patrol between.")]
        [SerializeField] private Transform[] waypoints;
        [Tooltip("The field of view radius of the enemy.")]
        [SerializeField] private float fovRadius = 6f;
        [Tooltip("The speed of the enemy.")]
        [SerializeField] private float speed = 2f;
        [Tooltip("The attack range of the enemy.")]
        [SerializeField] private float attackRange = 2f;

        /// <summary>
        /// Sets up the enemy behaviour tree.
        /// </summary>
        /// <returns></returns>
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