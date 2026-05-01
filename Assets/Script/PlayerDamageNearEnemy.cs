using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageNearEnemy : MonoBehaviour
{
    private IReadOnlyList<Enemy> _enemies;
    private CalculateDistance _calculate;
    [SerializeField] private WaveSpawner _waveSpawner;
    void Awake()
    {
        AssignDependencies();
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }

    }
    private bool ValidateDependencies()
    {
        if (_calculate == null)
        {
            Debug.LogError("CalculateDistance is missing in PlayerDamageNearEnemy");
            return false;
        }
        if (_waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing in PlayerDamageNearEnemy");
            return false;
        }

        if (_enemies == null)
        {
            Debug.LogError("Enemy List is null");
            return false;
        }
        return true;
    }
    private void AssignDependencies()
    {
        _calculate = GetComponent<CalculateDistance>();
        if (_waveSpawner != null)
            _enemies = _waveSpawner.Enemies;
    }
    public Enemy GetNearestEnemy()
    {
        Enemy autoTargetEnemy = null;
        float minDistance = float.MaxValue;
        for (int i = 0; i < _enemies.Count; i++)
        {
            Enemy enemy = _enemies[i];
            if (enemy == null) continue;
            if (enemy.IsDead) continue;
            float currentEnemyDistance = _calculate.GetSqrDistanceToPlayer(enemy);
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
        Enemy enemyNearest = GetNearestEnemy();
        if (enemyNearest == null)
        {
            Debug.Log("No valid enemy found");
            return;
        }
        Debug.Log($"Enemy is nearest Player: {enemyNearest.name}");
    }
}
