using System.Collections.Generic;

namespace ChestSystem
{
    public class Inventory 
    {
        private InventoryUI inventoryUI;
        private List<Slot> slotList;
        private int slotsAvailable;
        public ChestUnlocker chestUnlocker {get; private set;}

        public Inventory(InventoryUI inventoryUI){
            this.inventoryUI = inventoryUI;
            slotsAvailable = inventoryUI.slotUIArray.Length;
            chestUnlocker = inventoryUI.GetComponent<ChestUnlocker>();
            CreateSlots();
        }

        private void CreateSlots(){
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

        private int GetEmptySlotIndex(){
            for(int i=0; i<slotsAvailable; i++){
                if(slotList[i].IsSlotEmpty()){
                    return i;
                }
            }
            return -1;
        }

        
    }
}
