using Assets.Script;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private PlayerData _playerData;
    [SerializeField] private Player _player;
    private WeaponData _weaponData;
    private PlayerHealthSystem _playerHealthSystem;

    void Awake()
    {
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }
    void Start()
    {
        
        _playerData = new PlayerData(100, 2f, 2f);
        _weaponData = new WeaponData("HandGun", 20, 2f);
        _playerHealthSystem = new PlayerHealthSystem(_playerData.MaxHp);
        _player.Initialize(_playerData, _weaponData, _playerHealthSystem);
    }
    private bool ValidateDependencies()
    {
        if (_player == null)
        {
            Debug.LogError("Player is missing");
            return false;
        }
        return true;
    }

}
