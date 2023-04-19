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
        Queue<ChestController> unlockQueue;
        private int queueSize;
        private bool queueActive;

        public Inventory(InventoryUI inventoryUI){
            this.inventoryUI = inventoryUI;
            queueSize = 2;
            slotsAvailable = inventoryUI.slotUIArray.Length;
            unlockQueue = new Queue<ChestController>();
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

        public void AddChestToUnlockQueue(ChestController chestController){
            unlockQueue.Enqueue(chestController);
            if(!queueActive)
                UnlockChestInQueue();
        }

        public void RemoveChestFromQueue(){
            unlockQueue.Dequeue();
            if(unlockQueue.Count > 0)
                UnlockChestInQueue();
            else
                queueActive = false;
        }

        public void UnlockChestInQueue(){
            queueActive = true;
            unlockQueue.Peek().chestUnlocker.UnlockChestWithTime();
        }

        public bool IsUnlockQueueFull(){
            return unlockQueue.Count == queueSize;
        }
    }
}
