using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private const int MaxEnemyCount = 5;
    public IReadOnlyList<Enemy> Enemies => _enemies;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Player player;

    public void CleanupDeadEnemy()
    {
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            if (_enemies[i] == null || _enemies[i].IsDead)
            {
                _enemies.RemoveAt(i);
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
        if (_enemies.Count >= MaxEnemyCount)
        {
            Debug.Log("Enemy Count >= 5");
            return;
        }
        int currentSpawnEnemy = 0;
        while (currentSpawnEnemy < MaxEnemyCount)
        {
            _enemies.Add(Instantiate(enemy, transform.position, Quaternion.identity));
            Debug.Log($"a enemy is created, current Enemy spawn: {currentSpawnEnemy}");
            currentSpawnEnemy++;
        }
    }
}
