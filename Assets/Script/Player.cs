using Assets.Script;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData _playerData;
    public int MaxHp => _playerData.MaxHp;
    private int _currentHp;
    private Weapon _weapon;

    public int Damage => _weapon.Damage;
    private bool _isDead;
    public bool IsDead => _isDead;

    private const int MinimumDamage = 0;
    private const int MinimumHp = 0;

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
        _weapon = GetComponent<Weapon>();
    }

    private bool ValidateDependencies()
    {
        
        return true;
    }
    public void Initialize(PlayerData dataPlayer, WeaponData dataWeapon)
    {
        _playerData = dataPlayer;
        _currentHp = dataPlayer.MaxHp;
        _isDead = false;
        _weapon.Initialize(dataWeapon);
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
        if (_currentHp > MaxHp)
        {
            _currentHp = MaxHp;
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
        Debug.Log($" CurrentHp = {_currentHp} | Max HP = {MaxHp} | Status: {status}");
    }
}
