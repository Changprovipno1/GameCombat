using Assets.Script;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    private IReadOnlyList<Enemy> _enemies;
    [SerializeField] private WaveSpawner _waveSpawner;
    private CalculateDistance _calculate;
    private float AttackRange => _player.AttackRange;

    private void Awake()
    {
        AssignDependencies();
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }
    private void AssignDependencies()
    {
        _player = GetComponent<Player>();
        _calculate = GetComponent<CalculateDistance>();
        if (_waveSpawner != null)
            _enemies = _waveSpawner.Enemies;
    }
    private bool ValidateDependencies()
    {
        if (_player == null)
        {
            Debug.LogError("Player component is missing in PlayerAttack");
            return false;
        }
        if (_waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing in PlayerAttack");
            return false;
        }

        if (_calculate == null)
        {
            Debug.LogError("CalculateDistance is missing in PlayerAttack");
            return false;
        }
        if (_enemies == null)
        {
            Debug.LogError("List Enemy is null");
            return false;
        }
        return true;
    }
    private void AttackEnemy(Enemy enemy)
    {
        // lấy trạng thái trước đó của enemy
        bool wasAliveBeforeHit = !enemy.IsDead;
        Debug.Log($"Player attack {enemy.name}");
        enemy.TakeDamage(_player.Damage);
        CombatStatTracker.RecordDamage(_player.Damage);
        if (wasAliveBeforeHit && enemy.IsDead)
        {
            CombatStatTracker.RecordKillWave();
        }
    }

    public bool AttackAllEnemyInRange()
    {
        float attackRangeSqr = AttackRange * AttackRange;
        bool hasHitEnemy = false;
        if (_player.IsDead)
        {
             return false;
        }
        for (int i = 0; i <  _enemies.Count; i++)
        {
            Enemy enemyTarget = _enemies[i];
            if(!IsValidTarget(enemyTarget, attackRangeSqr)) { continue; }
            AttackEnemy(enemyTarget);
            hasHitEnemy = true;
            enemyTarget.PrintEnemyInfo();
         }
        return hasHitEnemy;
     }


    private bool IsValidTarget(Enemy enemyTarget, float attackRangeSqr)
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
        if (distance > attackRangeSqr)
        {
            return false;
        }
        return true;
    }
}
