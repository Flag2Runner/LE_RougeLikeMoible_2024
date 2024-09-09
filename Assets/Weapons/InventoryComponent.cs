using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private Weapon[] initialWeaponPrefabs;

    List<Weapon> _weapons = new List<Weapon>();

    private int _currentWeaponIndex = -1;

    private void Awake()
    {
        foreach (Weapon weaponPrefab in initialWeaponPrefabs)
        {
            Weapon newWeapon = Instantiate(weaponPrefab);
            newWeapon.Init(gameObject);
            _weapons.Add(newWeapon);
        }

        EquipNextWeapon();
    }

    private void EquipNextWeapon()
    {
        if (_weapons.Count == 0) return;
        
        int nextWeaponIndex = _currentWeaponIndex + 1;
        if (nextWeaponIndex > _weapons.Count)
        {
            nextWeaponIndex = 0;
        }
        
        _weapons[nextWeaponIndex].Equip();
        
        //unequip the old one
        if(_currentWeaponIndex > 0 && _currentWeaponIndex < _weapons.Count)
        {
            _weapons[_currentWeaponIndex].UnEquip();
        }
        _currentWeaponIndex = nextWeaponIndex;
    }
}
