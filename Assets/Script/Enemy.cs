using Assets.Script;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyHealthSystem _enemyHealthSystem;

    public bool IsDead
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("EnemyHealthSystem is not initialized");
                return true;
            }
            return _enemyHealthSystem.IsDead;
        }
    }

    public void Initialize(EnemyData data)
    {
        if (data == null)
        {
            Debug.LogError("EnemyData is not initialized");
            return;
        }
        _enemyHealthSystem = new EnemyHealthSystem(data.MaxHp);
    }
    private bool IsReady()
    {
        if (_enemyHealthSystem == null)
        {
            return false;
        }
        return true;
    }
    public void TakeDamage(int rawDamage)
    {
        if (!IsReady()) return;
        _enemyHealthSystem.TakeDamage(rawDamage);
    }
    public void PrintEnemyInfo()
    {
        if (!IsReady()) return;
        DebugOverlay.LogStatus(_enemyHealthSystem.CurrentHp, _enemyHealthSystem.MaxHp, _enemyHealthSystem.IsDead);
    }
}