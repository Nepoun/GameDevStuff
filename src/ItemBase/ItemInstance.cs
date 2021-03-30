using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Item Instance", menuName = "Inventory System/Item Instance", order = 1)]

public class ItemInstance : ScriptableObject
{
    public Item item;
    public bool isNew;
    public string quality;


}
