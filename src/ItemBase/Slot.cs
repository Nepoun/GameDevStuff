using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Slot
{
    public int slotId;
    public ItemInstance item;
    public bool isEmpty;
    public int ItemQnt;
    public int AddQuantity(int qnt)
    {
        ItemQnt += qnt;
        if(ItemQnt >= 64){
            int counter = ItemQnt; 
            ItemQnt = 64;
            return counter - 64;
        }else{
            return 0;
        }

        /**

            Returns always the diffence

            result = 64 + 1
            result = 65 -64

            result = 1

            retornar 1


        **/


    }

    
}
