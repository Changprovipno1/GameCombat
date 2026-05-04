using Assets.Script;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyHealthSystem _enemyHealthSystem;

    public bool IsDead => _enemyHealthSystem.IsDead;

    public void Initialize(EnemyData data)
    {
        _enemyHealthSystem = new EnemyHealthSystem(data.MaxHp);
    }
    public void TakeDamage(int rawDamage) => _enemyHealthSystem.TakeDamage(rawDamage);
}