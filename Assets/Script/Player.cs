using Assets.Script;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _maxHP = 100;
    private int _currentHp;
    private Weapon _weapon;
    public int Damage => _weapon.Damage;
    private bool _isDead;
    public bool IsDead => _isDead;
    private WeaponData _weaponData;

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
        _weaponData = new WeaponData("HandGun", 20, 2f);
        _weapon.Initialize(_weaponData);
    }
    private void AssignDependencies()
    {
        _weapon = GetComponent<Weapon>();
    }
    private bool ValidateDependencies()
    {
        if (_weapon == null)
        {
            Debug.LogError("Weapon is missing in Player");
            return false;
        }
        return true;
    }
    private void Start()
    {
        _currentHp = _maxHP;
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
