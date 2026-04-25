using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private int _currentHp;
    private bool _isDead;
    public bool IsDead => _isDead;
    private const int MinimumDamage = 0;
    private const int MinimumHp = 0;
    void Start()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(int rawDamage)
    {
        if (rawDamage <= MinimumDamage) return;
        if (IsDead)
        {
            Debug.Log($"Enemy is already dead");
            return;
        }
        ApplyDamage(rawDamage);
        Debug.Log($"Taking Damage : {rawDamage}");
        if (HasReachedDeathThreshold())
        {
            Die();
        }
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
        Debug.Log($"Enemy is dead");
    }
   
    public void PrintEnemyInfo()
    {
        string status = "";
        if (IsDead)
        {
            status = "Dead";
        }
        else
        {
            status = "Alive";
        }
        Debug.Log($" CurrentHp = {_currentHp} | MaxHP = {_maxHp} | Status: {status}");
    }
}
