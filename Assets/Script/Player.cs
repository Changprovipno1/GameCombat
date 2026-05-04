using Assets.Script;
using UnityEngine;

public class Player : MonoBehaviour
{
    // field
    private int _currentHp;
    private bool _isInitialize;
    private bool _isDead;
    private PlayerData _playerData;
    private Weapon _weapon;

    // property
    public int MaxHp
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("PlayerData is not initialized");
                return 0;
            }
            return _playerData.MaxHp;
        }
    }
    public int Damage
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("Weapon is not initialized");
                return 0;
            }
            return _weapon.Damage;
        }
        
    }
    public bool IsDead => _isDead;

    // magic number
    private const int MinimumDamage = 0;
    private const int MinimumHp = 0;

    // GetComponent và check null
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
        if (_weapon == null)
        {
            Debug.LogError("Weapon is missing in Player");
            return false;
        }
        return true;
    }
    // hàm khởi tạo Player
    public void Initialize(PlayerData dataPlayer, WeaponData dataWeapon)
    {
        if (dataPlayer == null)
        {
            Debug.LogError("PlayerData is not initialized");
            return;
        }
        _playerData = dataPlayer;
        _currentHp = dataPlayer.MaxHp;

        _isDead = false;


        if (_weapon == null)
        {
            AssignDependencies();
        }
        if (_weapon == null)
        {
            Debug.LogError("Weapon is missing in Player");
            return;
        }
        if (dataWeapon == null)
        {
            Debug.LogError("WeaponData is not initialized");
            return;
        }
        _weapon.Initialize(dataWeapon);
        _isInitialize = true;
    }

    public void TakeDamage(int rawDamage)
    {
        if (!IsReady()) return;
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
        if (!IsReady()) return;
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
        if (!IsReady()) return;
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
    private bool IsReady()
    {
        if (!_isInitialize) return false;
        if (_playerData == null) return false;
        if (_weapon == null) return false;
        return true;
    }
    public void PrintPlayerInfo()
    {
        if (!IsReady()) return;
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
