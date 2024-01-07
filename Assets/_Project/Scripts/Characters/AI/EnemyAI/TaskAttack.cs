using System.Collections;
using System.Collections.Generic;
using MatchaIsSpent.BehaviourTree;
using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    public class TaskAttack : Node
    {
        private Transform lastTarget;
        private HealthSystem healthSystem;
        private Animator animator;
        private float attackTime = 1f;
        private float attackCounter = 0f;

        public TaskAttack(Transform transform)
        {
            animator = transform.GetComponent<Animator>();
        }

        override public NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (target != lastTarget)
            {
                healthSystem = target.GetComponent<HealthSystem>();
                lastTarget = target;
            }

            attackCounter += Time.deltaTime;
            if (attackCounter >= attackTime)
            {
                bool isDead = healthSystem.IsDead();

                if (!isDead)
                {
                    animator.Play("EnemyAttack");
                    attackCounter = 0f;
                }
                else
                {
                    ClearData("target");
                    animator.SetFloat("Speed", 1f);
                }

            }


            state = NodeState.RUNNING;
            return state;
        }
    }
}