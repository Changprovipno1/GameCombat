using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private const int MaxEnemyCount = 5;
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
        ValidateEnemiesList();
    }
    public void SpawnWave(Enemy enemyInput)
    {
        // check enemy thêm vào có trống thì k thêm nữa
        if (enemyInput == null)
        {
            Debug.Log("Enemy is null");
            return;
        }
        CleanupDeadEnemy();
        if (_enemies.Count >= MaxEnemyCount)
        {
            Debug.Log("Enemy Count >= 5");
            return;
        }
        int currentSpawnEnemy = 0;
        int enemiesLeftToSpawn = MaxEnemyCount - Enemies.Count;
        while (currentSpawnEnemy < enemiesLeftToSpawn)
        {
            _enemies.Add(Instantiate(enemyInput, transform.position, Quaternion.identity));
            Debug.Log($"a enemy is created, current Enemy spawn: {currentSpawnEnemy}");
            currentSpawnEnemy++;
        }
        ValidateEnemiesList();
    }
    private void ValidateEnemiesList()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            Debug.Assert(_enemies[i] != null, $"null enemy in index {i}");
            Debug.Assert(!_enemies[i].IsDead, $"dead enemy in index {i}");
        }
    }
}
