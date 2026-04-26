using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _currentHp;
    [SerializeField] private int _damage;
    public int Damage => _damage;
    private bool _isDead;
    public bool IsDead => _isDead;
    private const int MinimumDamage = 0;
    private const int MinimumHp = 0;
    private void Start()
    {
        _currentHp = _maxHP;
        _damage = 20;
    }
    public void TakeDamage(int rawDamage)
    {
        if (rawDamage <= MinimumDamage) return;
        if (IsDead)
        {
            Debug.Log($"Player is already dead");
            return;
        }
        ApplyDamage(rawDamage);
        Debug.Log($"Taking damage : {rawDamage}");
        if (HasReachedDeathThreshold())
        {
            Die();
        }
    }
    private bool HasReachedDeathThreshold()
    {
        if (_currentHp <= MinimumHp)
        {
            return true;
        }
        else
            return false;

    }
     private void ApplyDamage(int incomingDamage)
    {
        _currentHp -= incomingDamage;
        if (_currentHp <= MinimumHp)
        {
            _currentHp = 0;
            // return; // dùng để chặn effect mở rộng
        }
    }
    private void Die()
    {
        if (IsDead) return;
        _isDead = true;
        Debug.Log($"Player is dead");
    }
    public void Heal(int amountHp)
    {
        if (IsDead)
        {
            Debug.Log("Heal is error because player is dead");
            return;
        }
        if (amountHp <= 0)
        {
            Debug.Log("Heal is error because amount Hp <= 0");
            return;
        }

        _currentHp += amountHp;
        Debug.Log($"Heal : {amountHp}");
        if (_currentHp > _maxHP)
        {
            _currentHp = _maxHP;
        }

    }

    public void PrintPlayerInfo()
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
        Debug.Log($" CurrentHp = {_currentHp} | Max HP = {_maxHP} | Status: {status}");
    }
}
