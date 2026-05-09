using Assets.Script;
using UnityEngine;

public class Player : MonoBehaviour
{
    // field
    private bool _isInitialize;
    private PlayerData _playerData;
    private Weapon _weapon;
    private PlayerHealthSystem _playerHealthSystem;

    // Property
    public bool IsDead
    {
        get
        {
            if (_playerHealthSystem == null)
            {
                Debug.LogError("PlayerHealthSystem is not initialized");
                return true;
            }
            return _playerHealthSystem.IsDead;
        }
    }
    public int Damage
    {
        get
        {
            if (_weapon == null)
            {
                Debug.LogError("Weapon is not initialized");
                return 0;
            }
            return _weapon.Damage;
        }
    }
    public float AttackRange
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("PlayerData is not initialized");
                return 0;
            }
            return _playerData.AttackRange;
        }
    }
    public float AttackCooldown
    {
        get
        {
            if (!IsReady())
            {
                Debug.LogError("PlayerData is not initialized");
                return 0;
            }
            return _playerData.AttackCooldown;
        }
    }
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
    public void Initialize(PlayerData dataPlayer, WeaponData dataWeapon, PlayerHealthSystem playerHealthSystem)
    {
        if (dataPlayer == null)
        {
            Debug.LogError("PlayerData is not initialized");
            return;
        }
        if (playerHealthSystem == null)
        {
            Debug.LogError("PlayerHealthSystem is not initialized");
            return;
        }
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
        _playerData = dataPlayer;
        _playerHealthSystem = playerHealthSystem;
        _weapon.Initialize(dataWeapon);
        _isInitialize = true;
    }
    
    private bool IsReady()
    {
        if (!_isInitialize) return false;
        if (_playerData == null) return false;
        if (_weapon == null) return false;
        if (_playerHealthSystem == null) return false;
        return true;
    }
    public void TakeDamage(int rawDamage)
    {
        if (!IsReady()) return;
        _playerHealthSystem.TakeDamage(rawDamage);
    }
    public void Heal(int amount)
    {
        if (!IsReady()) return;
        _playerHealthSystem.Heal(amount);
    }
    public void PrintPlayerInfo()
    {
        if (!IsReady()) return;
        DebugOverlay.LogStatus(_playerHealthSystem.CurrentHp, _playerHealthSystem.MaxHp, _playerHealthSystem.IsDead);
    }
}
