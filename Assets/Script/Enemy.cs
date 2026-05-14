using Assets.Script;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterHealthSystemBase _characterHealthSystemBase;

    public bool IsDead
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("EnemyHealthSystem is not initialized");
                return true;
            }
            return _characterHealthSystemBase.IsDead;
        }
    }

    public void Initialize(EnemyData data)
    {
        if (data == null)
        {
            Debug.LogError("EnemyData is not initialized");
            return;
        }
        //_characterHealthSystemBase = new EnemyHealthSystem(data.MaxHp);
        _characterHealthSystemBase = new EliteEnemyHealthSystem(data.MaxHp, 5);

    }
    private bool IsReady()
    {
        if (_characterHealthSystemBase == null)
        {
            return false;
        }
        if (_characterHealthSystemBase == null)
        {
            return false;
        }
        return true;
    }
    public void TakeDamage(int rawDamage)
    {
        if (!IsReady()) return;
        _characterHealthSystemBase.TakeDamage(rawDamage);
    }
    public void PrintEnemyInfo()
    {
        if (!IsReady()) return;
        DebugOverlay.LogStatus(_characterHealthSystemBase.CurrentHp, _characterHealthSystemBase.MaxHp, _characterHealthSystemBase.IsDead);
    }
}