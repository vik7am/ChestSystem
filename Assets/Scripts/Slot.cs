using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class Slot 
    {
        public SlotUI slotUI {get; private set;}
        ChestController chestController;

        public bool IsSlotEmpty(){
            return chestController == null;
        }

        public Slot(SlotUI slotUI){
            this.slotUI = slotUI;
            slotUI.SetSlot(this);
        }

        public void SpawnChest(){
            ChestModel chestModel = new ChestModel();
            chestController = new ChestController(chestModel, slotUI.chestView);
            slotUI.statusGUI.text = "Locked";
            slotUI.actionButton.gameObject.SetActive(true);
            
        }

        public void DestroyChest(){
            chestController.SetChestActive(false);
            chestController = null;
            slotUI.statusGUI.text = "EMPTY";
            slotUI.actionButton.gameObject.SetActive(false);
        }
    }
}
