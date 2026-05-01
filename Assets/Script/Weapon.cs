using Assets.Script;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponData _weaponData;

    public int Damage => _weaponData.Damage;
    public float Range => _weaponData.Range;

    public void Initialize(WeaponData data)
    {
        _weaponData = data;
    }
}
