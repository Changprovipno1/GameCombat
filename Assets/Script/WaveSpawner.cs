using Assets.Script;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private const int MaxEnemyCount = 5;
    public IReadOnlyList<Enemy> Enemies => _enemies;

    
    public void CleanupDeadEnemy()
    {
        
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
            else if (_enemies[i].IsDead)
            {
                Enemy enemyToDestroy = _enemies[i];
                _enemies.RemoveAt(i);
                Destroy(enemyToDestroy.gameObject);
                CombatStatTracker.RecordKillThisSession();
            }
        }
        ValidateEnemiesList();
    }
    
    public int CountEnemiesAlive()
    {
        int CountAllEnemiesAlive = 0;
        foreach (var enemy in Enemies)
        {
            if (enemy.IsDead) continue;
            CountAllEnemiesAlive++;
        }
        return CountAllEnemiesAlive;
    }
    public int CountEnemiesDead()
    {
        int CountAllEnemiesDead = 0;
        foreach (var enemy in Enemies)
        {
            if (!enemy.IsDead) continue;
            CountAllEnemiesDead++;
        }
        return CountAllEnemiesDead;
    }

    public void SpawnWave(EnemyData enemyData, Enemy enemyInput)
    {
        // check model enemy trống thì k thêm nữa
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
            Enemy enemySpawn = Instantiate(enemyInput, transform.position, Quaternion.identity);
            Debug.Log(enemySpawn.GetHashCode());
            enemySpawn.Initialize(enemyData);
            _enemies.Add(enemySpawn);
            Debug.Log($"a enemy is created, current Enemy spawn: {currentSpawnEnemy}");
            currentSpawnEnemy++;
        }
        ValidateEnemiesList();
    }
    public void PrintList()
    {
        if (!HasEnemyInList())
        {
            Debug.Log("No enemy in list");
            return;
        }
        for (int i = 0; i < _enemies.Count; i++)
        {
            Enemy enemy = _enemies[i];
            Debug.Log($"{enemy.name} in list");
        }
    }
    bool HasEnemyInList()
    {
        return _enemies.Count > 0;
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
