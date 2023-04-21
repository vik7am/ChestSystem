using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestController
    {
        ChestView chestView;
        public ChestModel chestModel {get; private set;}
        public ChestUnlocker chestUnlocker {get; private set;}
        public Slot slot;

        public ChestController(Slot slot, ChestModel chestModel, ChestView chestView){
            this.slot = slot;
            this.chestView = chestView;
            this.chestModel = chestModel;
            chestUnlocker = ChestService.Instance.inventory.chestUnlocker;
            SetChestActive(true);
        }
        
        public void TryToOpenChest(){
            if(chestModel.chestState == ChestState.LOCKED){
                chestUnlocker.ShowOptionsToUnlockChest(chestModel);
            }
            else if(chestModel.chestState == ChestState.UNLOCKING){
                chestUnlocker.ShowOptionsToUnlockChest(chestModel);
            }
            else{
                OpenChest();
            }
        }

        public void OpenChest(){
            ItemService.Instance.AddCoinsAndGems(chestModel.coins, chestModel.gems);
            string message = "Chest opened with " + chestModel.coins + " coins and " + chestModel.gems + " gems";
            ChestService.Instance.messagePopupUI.ShowMessagePopup(message);
            slot.RemoveChest();
        }

        public void SetChestActive(bool status){
            chestView.gameObject.SetActive(status);
        }
    }
}
