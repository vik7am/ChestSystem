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
        Action unlockChest;

        public bool IsSlotEmpty(){
            return chestController == null;
        }

        public Slot(SlotUI slotUI){
            this.slotUI = slotUI;
            slotUI.SetSlot(this);
            unlockChest += UnlockChest;
        }

        public void SpawnChest(){
            ChestModel chestModel = new ChestModel();
            chestController = new ChestController(chestModel, slotUI.chestView);
            slotUI.statusGUI.text = "Locked";
            slotUI.actionButton.gameObject.SetActive(true);
        }

        public void ShowChestUnlockOption(){
            ChestService.Instance.popupUI.ShowChestUnlockPopup(unlockChest, unlockChest,
                 "20 sec Time", "100 Gems");
        }

        public void UnlockChest(){
            DestroyChest();
        }

        public void DestroyChest(){
            chestController.SetChestActive(false);
            chestController = null;
            slotUI.statusGUI.text = "EMPTY";
            slotUI.actionButton.gameObject.SetActive(false);
        }
    }
}
