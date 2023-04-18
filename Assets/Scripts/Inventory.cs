using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class Inventory 
    {
        InventoryUI inventoryUI;
        List<Slot> slotList;
        int slotsAvailable;

        public Inventory(InventoryUI inventoryUI){
            this.inventoryUI = inventoryUI;
            slotsAvailable = inventoryUI.slotUIArray.Length;
            CreateSlots();
        }

        public void CreateSlots(){
            slotList = new List<Slot>();
            for(int i=0; i<slotsAvailable; i++){
                slotList.Add(new Slot(inventoryUI.slotUIArray[i]));
            }
        }

        public bool SpawnChest(){
            int index = GetEmptySlotIndex();
            if(index == -1)
                return false;
            slotList[index].SpawnChest();
            return true;
        }

        public int GetEmptySlotIndex(){
            for(int i=0; i<slotsAvailable; i++){
                if(slotList[i].IsSlotEmpty()){
                    return i;
                }
            }
            return -1;
        }
    }
}
