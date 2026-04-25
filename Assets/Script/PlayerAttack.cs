using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    private const float AttackRange = 5f;
    private IReadOnlyList<Enemy> _enemies;
    [SerializeField] private WaveSpawner _waveSpawner;
    private CalculateDistance _calculate;
    private const float AttackRangeSqr = AttackRange * AttackRange;

    private void Awake()
    {
        _player = GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player component is missing");
            enabled = false;
            return;
        }
        
        if (_waveSpawner == null)
        {
            Debug.LogWarning("WaveSpawner is missing");
            enabled = false;
            return;
        }
        _enemies = _waveSpawner.Enemies;
        if (_enemies == null)
        {
            Debug.LogWarning("List Enemy is null");
            enabled = false;
            return;
        }
        _calculate = GetComponent<CalculateDistance>();
        if (_calculate == null)
        {
            Debug.LogError("CalculateDistance is missing");
            enabled = false;
            return;
        }
        
    }
    private void AttackEnemy(Enemy enemy)
    {
        Debug.Log($"Player attack {enemy.name}");
        enemy.TakeDamage(_player.Damage);
    }
    public bool AttackAllEnemyInRange()
    {
        bool hasHitEnemy = false;
        if (_player.IsDead)
        {
             return false;
        }
        for (int i = 0; i<  _enemies.Count; i++)
        {
            Enemy enemyTarget = _enemies[i];
            if(!IsValidTarget(enemyTarget)) { continue; }
            AttackEnemy(enemyTarget);
            hasHitEnemy = true;

         }
        return hasHitEnemy;
     }
    private bool IsValidTarget(Enemy enemyTarget)
    { 
        if (enemyTarget == null) 
        {
            return false;
            
        }
        if (enemyTarget.IsDead) 
        {
            return false;
        }
        float distance = _calculate.GetSqrDistanceToPlayer(enemyTarget);
        if (distance > AttackRangeSqr)
        {
            return false;
        }
        return true;
    }

}
