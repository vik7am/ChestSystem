using System;

namespace ChestSystem
{
    public class Slot 
    {
        private ChestController chestController;
        public SlotUI slotUI {get; private set;}
        public Action unlockChestWithTime;
        public Action unlockChestWithGems;

        public Slot(SlotUI slotUI){
            this.slotUI = slotUI;
            slotUI.SetSlot(this);
        }

        public void SpawnChest(){
            ChestConfigArraySO chestConfigs = ChestService.Instance.chestConfigArraySO;
            int chestConfigIndex = GetRandomChestConfigIndex(chestConfigs);
            ChestModel chestModel = new ChestModel(chestConfigs.chestConfigArray[chestConfigIndex]);
            chestController = new ChestController(this, chestModel, slotUI.chestView);
            slotUI.RegisterForChestEvents(chestModel);
            slotUI.OpenChestButtonActive(true);
        }

        public int GetRandomChestConfigIndex(ChestConfigArraySO chestConfigs){
            int chestConfigCount = chestConfigs.chestConfigArray.Length;
            return UnityEngine.Random.Range(0, chestConfigCount);
        }

        public void TryToOpenChest(){
            chestController.TryToOpenChest();
        }

        public void RemoveChest(){
            slotUI.UnregisterForChestEvents(chestController.chestModel);
            chestController.SetChestActive(false);
            chestController = null;
            slotUI.UpdateStatusText("Empty");
            slotUI.OpenChestButtonActive(false);
        }

        public bool IsSlotEmpty(){
            return chestController == null;
        }
    }
}
