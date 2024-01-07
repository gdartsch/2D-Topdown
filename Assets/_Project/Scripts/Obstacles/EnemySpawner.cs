using System.Collections;
using System.Collections.Generic;
using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.Obstacles
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private int enemiesLimit = 4;
        private int enemiesCount = 0;
        private float spawnTimer = 0f;

        private void Start()
        {
            HealthSystem.OnDied += EnemyDied;
        }

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

        private void SpawnEnemy()
        {
            if (enemiesCount >= enemiesLimit)
                return;

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            enemiesCount++;
        }

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