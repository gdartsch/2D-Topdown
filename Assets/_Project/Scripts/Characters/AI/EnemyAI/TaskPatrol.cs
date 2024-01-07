using System.Collections;
using System.Collections.Generic;
using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    public class TaskPatrol : Node
    {
        private Transform transform;
        private Animator animator;
        private Rigidbody2D rigidbody2D;
        private Transform[] waypoints;
        private int currentWaypointIndex = 0;
        private float waitTime = 1f;
        private float waitCounter = 0f;
        private bool waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            this.transform = transform;
            animator = transform.GetComponent<Animator>();
            rigidbody2D = transform.GetComponent<Rigidbody2D>();
            this.waypoints = waypoints;
        }

        public override NodeState Evaluate()
        {
            if (waiting)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                {
                    waiting = false;
                    animator.SetFloat("Speed", waiting ? 0f : 1f);
                }
            }
            else
            {
                Transform waypoint = waypoints[currentWaypointIndex];
                if (Vector3.Distance(transform.position, waypoint.position) < 0.1f)
                {
                    transform.position = waypoint.position;
                    waitCounter = 0f;
                    waiting = true;

                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoint.position, EnemyBehaviourTree.speed * Time.deltaTime);
                    float x = transform.position.x - waypoint.position.x;
                    float y = transform.position.y - waypoint.position.y;
                    animator.SetFloat("AnimationMoveX", x < 0 ? 1f : -1f);
                    animator.SetFloat("AnimationMoveY", y < 0 ? 1f : -1f);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }

        /// <summary>
        /// Animate the enemy.
        /// </summary>
        private void Animate()
        {

        }
    }
}