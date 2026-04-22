        using System.Collections.Generic;
        using UnityEngine;

        public class PlayerAttack : MonoBehaviour
        {
            [SerializeField] private Player player;
            private float rangeAttack = 5f;
            private List<Enemy> enemies;
            [SerializeField] private WaveSpawner waveSpawner;

            private void Awake()
            {
                enemies = waveSpawner.Enemies;
            }
            public void AttackEnemy(Enemy enemy)
            {
                if (enemy == null)
                {
                    Debug.Log("Enemy is null, can not attack");
                    return;
                }
                if (player.IsDead)
                {
                    Debug.Log("Player is already dead, can not attack");
                    return;
                }

                if (enemy.IsDead)
                {
                    Debug.Log("Enemy is already dead, can not attack");
                    return;
                }
                Debug.Log($"Player attack {enemy.name}");
                enemy.TakeDamage(player.Damage);
            }

            public void AttackAllEnemyInRange()
            {
                if (player.IsDead)
                {
                    return;
                }
                for (int i = 0; i<  enemies.Count; i++)
                {
                    Enemy enemyTarget = enemies[i];
                    if (enemyTarget == null) continue;
                    if (enemyTarget.IsDead) { continue; }
                    float distance = GetSqrDistanceToPlayer(enemyTarget);
                    if (distance > rangeAttack * rangeAttack)
                    {
                        Debug.Log($"{enemyTarget} is not in range attack");
                        continue;
                    }
                    AttackEnemy(enemyTarget);
                }
            }
            public float GetSqrDistanceToPlayer(Enemy enemy)
            {
                Vector3 offset = enemy.transform.position - transform.position;
                float distance = offset.sqrMagnitude;
                return distance;
            }
            public Enemy AutoNearestEnemy()
            {
                Enemy autoTargetEnemy = null;
                float minDistance = float.MaxValue;
                for (int i = 0; i < enemies.Count; i++)
                {
                    Enemy enemy = enemies[i];
                    if (enemy == null) continue;
                    if (enemy.IsDead) continue;
                    float currentEnemyDistance = GetSqrDistanceToPlayer(enemy);
                    if (currentEnemyDistance < minDistance)
                    {
                        minDistance = currentEnemyDistance;
                        autoTargetEnemy = enemy;
                    }
                }
                return autoTargetEnemy;
            }
            public void PrintEnemyNearest()
            {
                Enemy enemyNearest = AutoNearestEnemy();
                if (enemyNearest == null)
                {
                    Debug.Log("No valid enemy found");
                    return;
                }
                Debug.Log($"Enemy is nearest Player: {enemyNearest.name}");
            }

        }
