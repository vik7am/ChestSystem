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
            ChestModel chestModel = new ChestModel();
            chestController = new ChestController(this, chestModel, slotUI.chestView);
            slotUI.RegisterForChestEvents(chestModel);
            slotUI.actionButton.gameObject.SetActive(true);
        }

        public void TryToOpenChest(){
            chestController.TryToOpenChest();
        }

        public void RemoveChest(){
            slotUI.UnregisterForChestEvents(chestController.chestModel);
            chestController.SetChestActive(false);
            chestController = null;
            slotUI.statusGUI.text = "EMPTY";
            slotUI.actionButton.gameObject.SetActive(false);
        }
    }
}
