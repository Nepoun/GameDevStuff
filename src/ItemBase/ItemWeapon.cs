using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Item Types/Weapon", order=2)]
[System.Serializable]
public class ItemWeapon : Item
{
    public string desc;
    public int damage;
}
