using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private int maxEnemyCount = 5;
    public List<Enemy> Enemies => enemies;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Player player;

    public void CleanupDeadEnemy()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null || enemies[i].IsDead)
            {
                enemies.RemoveAt(i);
            }
        }
    }
    public void SpawnWave(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.Log("Enemy is null");
            return;
        }
        if (enemies.Count > 0)
        {
            return;
        }
        int currentSpawnEnemy = 0;
        while (currentSpawnEnemy < maxEnemyCount)
        {
            enemies.Add(Instantiate(enemy, transform.position, Quaternion.identity));
            Debug.Log($"a enemy is created, current Enemy spawn: {currentSpawnEnemy}");
            currentSpawnEnemy++;
        }
    }
}
