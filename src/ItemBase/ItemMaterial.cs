using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[CreateAssetMenu(fileName = "New Material", menuName = "Inventory System/Item Types/Material", order=1)]
[System.Serializable]
public class ItemMaterial : Item
{
    public string desc;
}
