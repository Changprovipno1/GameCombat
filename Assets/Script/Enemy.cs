using Assets.Script;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _currentHp;
    private bool _isDead;
    private EnemyData _enemyData;

    public bool IsDead => _isDead;
    public int MaxHp => _enemyData.MaxHp;
    public int CurrentHp => _currentHp;

    private const int MinimumDamage = 0;
    private const int MinimumHp = 0;

    public void Initialize(EnemyData data)
    {
        _enemyData = data;
        _currentHp = data.MaxHp;
        _isDead = false;
    }

    public void TakeDamage(int rawDamage)
    {
        
        if (rawDamage <= MinimumDamage) return;
        if (IsDead)
        {
            Debug.Log($"Enemy is already dead");
            return;
        }
        Debug.Log($"Taking Damage : {rawDamage}");
        ApplyDamage(rawDamage);
        if (HasReachedDeathThreshold())
        {
            Die();
        }
        PrintEnemyInfo();
    }
    private bool HasReachedDeathThreshold()
    {
        return _currentHp <= MinimumHp;
    }
    private void ApplyDamage(int incomingDamage)
    {
        _currentHp -= incomingDamage;
        if (_currentHp < MinimumHp)
        {
            _currentHp = 0;
        }
    }
    private void Die()
    {
        if (IsDead) return;
        _isDead = true;
        Debug.Log("Enemy is dead");
    }
   
    private void PrintEnemyInfo()
    {
        string status = IsDead ? "Dead" : "Live";
        Debug.Log($" CurrentHp = {CurrentHp} | MaxHP = {MaxHp} | Status: {status}");
    }

        
}
