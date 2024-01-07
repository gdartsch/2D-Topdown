using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.Obstacles
{
    /// <summary>
    /// Spawns enemies at random spawn points.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("The enemy prefab to spawn.")]
        [SerializeField] private GameObject enemyPrefab;
        [Tooltip("The spawn points.")]
        [SerializeField] private Transform[] spawnPoints;
        [Tooltip("The spawn interval.")]
        [SerializeField] private float spawnInterval = 5f;
        [Tooltip("The enemies limit.")]
        [SerializeField] private int enemiesLimit = 4;
        /// <summary>
        /// The current amount of enemies.
        /// </summary>
        private int enemiesCount = 0;
        /// <summary>
        /// The spawn timer.
        /// </summary>
        private float spawnTimer = 0f;

        private void Start()
        {
            HealthSystem.OnDied += EnemyDied;
        }

        /// <summary>
        /// Called when the enemy dies.
        /// </summary>
        /// <param name="system"></param>
        private void EnemyDied(HealthSystem system)
        {
            if (system.HealthHolder == HealthHolder.Enemy)
            {
                EnemyDied();
            }
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }

        /// <summary>
        /// Spawns an enemy.
        /// </summary>
        private void SpawnEnemy()
        {
            if (enemiesCount >= enemiesLimit)
                return;

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            enemiesCount++;
        }

        /// <summary>
        /// Called when the enemy dies.
        /// </summary>
        private void EnemyDied()
        {
            enemiesCount--;
        }

        private void OnDestroy()
        {
            HealthSystem.OnDied -= EnemyDied;
        }
    }
}