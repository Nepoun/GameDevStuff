using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory/Inventory Instance", order = 1)]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public List<Slot> SlotsInBag;
    private int defaultSlotQnt = 32;

    
    /// <summary> Initialize Inventory
    ///<para> Should be used before interacting with the inventory </para>
    /// </summary>
    public void _PrepareSlots(int slotQnt = 0)
    {
        if (slotQnt == 0)
        {
            slotQnt = defaultSlotQnt;
        }
        SlotsInBag = new List<Slot>(slotQnt);
        //#TODO json save/load
        for (int i = 0; i < SlotsInBag.Count; i++)
        {
            SlotsInBag[i].slotId = i;
        }
    }


    /// <summary> Reset slot
    ///<para> Returns cleared slot</para>
    /// </summary>    
    public Slot ClearSlot(Slot slot)
    {
        slot.ItemQnt = 0;
        slot.item = null;
        slot.isEmpty = true;
        return slot;
    }


    /// <summary> Returns slot
    ///<para> Search by id </para>
    /// </summary>
    public Slot GetSlotById(int id)
    {
        return SlotsInBag[id];
    }




    /// <summary>Returns slot ID.
    /// <para>Search by Item instance. </para>
    /// </summary>
    public int GetSlotByItem(ItemInstance itemInstance)
    {
        foreach (Slot slot in SlotsInBag)
        {
            if (itemInstance == slot.item)
            {
                //Found something
                return slot.slotId;
            }
        }
        //If nothing is found
        return -1;
    }


    /// <summary> Insert item in inventory
    ///<para> OBS: if you dont wanna select a slot, use slotID = -1  </para>
    ///<para> Returns new slot</para>
    /// </summary>    
    public Slot InsertItem(ItemInstance item, int qntAdded, int slotId = -1)
    {
        /*         var SelectedSlot = GetSlotById(id);
                if (SelectedSlot == null) SelectedSlot = SlotsInBag[GetSlotByItem(item)]; */
        bool hasItem = false;
        Slot SelectedSlot = null;
        if (slotId >= 0)
        {
            SelectedSlot = GetSlotById(slotId);
        }
        else
        {
            hasItem = true;
            SelectedSlot = SlotsInBag[GetSlotByItem(item)];
        }

        if (hasItem)
        {
            if (qntAdded > 64)
            {
                int diff = SelectedSlot.AddQuantity(qntAdded);
                InsertItem(item, diff);
            }
            else
            {
                SelectedSlot.AddQuantity(qntAdded);
                return SelectedSlot;
            }
        }
        else
        {
            //#TODO
        }
        return SelectedSlot;
    }


    public Slot GetFirstEmptySlot()
    {
        if (BagIsFull())
        {
            Debug.Log("Bag is full!");

            return null;
        }

        foreach (Slot slot in SlotsInBag)
        {
            if (slot.item == null || slot.ItemQnt <= 0 || slot.isEmpty)
            {
                //Found empty slot
                return slot;
            }
        }
        //bag is full
        Debug.Log("Bag is full!");
        return null;
    }


    public bool TransferItemToSlot(int sourceSlotId, int targetSlotId)
    {
        Slot source = GetSlotById(sourceSlotId);
        Slot target = GetSlotById(targetSlotId);
        if (isSlotEmpty_ById(targetSlotId))
        {
            target.item = source.item;
            target.ItemQnt = source.ItemQnt;
            ClearSlot(source);
            return true;
        }
        else if (source.item == target.item && (source.ItemQnt + target.ItemQnt) > 64)
        {
            int diff = target.AddQuantity(source.ItemQnt);
            source.ItemQnt = diff;
            return true;
        }
        else if (source.item == target.item && (source.ItemQnt + target.ItemQnt) <= 64)
        {
            target.AddQuantity(source.ItemQnt);
            ClearSlot(source);
            return true;
        }
        return false;
    }


    /// <summary> Check if slot is empty
    ///<para> Search by slot instance  </para>
    /// </summary> 
    public bool isSlotEmpty_BySlot(Slot slot)
    {
        if (slot.item == null || slot.isEmpty == true || slot.ItemQnt <= 0)
        {
            return true;
        }
        return false;
    }


    /// <summary> Check if slot is empty
    ///<para> Search by slot ID  </para>
    /// </summary> 
    public bool isSlotEmpty_ById(int id)
    {
        Slot slot = SlotsInBag[id];
        if (slot.item == null || slot.isEmpty == true || slot.ItemQnt <= 0)
        {
            return true;
        }
        return false;
    }


    public bool BagIsFull()
    {
        bool FoundEmptySlot = false;
        foreach (Slot slot in SlotsInBag)
        {
            if (slot.item == null || slot.isEmpty == true || slot.ItemQnt <= 0)

            {
                FoundEmptySlot = true;
            }
        }
        return FoundEmptySlot;
    }
    [Header("Testing stuff")]
    public int sourceSlotId, targetSlotId;
    public ItemInstance ItemToAdd;
    public int qntAdded;



}

[CustomEditor(typeof(Inventory))]
public class InventoryInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (Inventory)target;

        if (GUILayout.Button("Prepare Slots"))
        {
            script._PrepareSlots();
        }
        if (GUILayout.Button("Transfer")) script.TransferItemToSlot(script.sourceSlotId, script.targetSlotId);
        if (GUILayout.Button("Insert")) script.InsertItem(script.ItemToAdd, script.qntAdded);


    }
}
