using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private PlayerData _playerData;
    [SerializeField] private Player _player;
    private WeaponData _weaponData;

    void Awake()
    {
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
        _playerData = new PlayerData(100, 2f, 2f);
        _weaponData = new WeaponData("HandGun", 20, 2f);
        _player.Initialize(_playerData, _weaponData);
        
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
