using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class Slot 
    {
        public SlotUI slotUI {get; private set;}
        ChestController chestController;
        public Action unlockChestWithTime;
        public Action unlockChestWithGems;

        public bool IsSlotEmpty(){
            return chestController == null;
        }

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
            slotUI.actionButton.gameObject.SetActive(true);
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
            slotUI.statusGUI.text = "Empty";
            slotUI.actionButton.gameObject.SetActive(false);
        }
    }
}
