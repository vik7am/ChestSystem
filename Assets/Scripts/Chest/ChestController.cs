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
            chestUnlocker = chestView.GetComponent<ChestUnlocker>();
            chestUnlocker.SetChestController(this);
            SetChestActive(true);
        }
        
        public void TryToOpenChest(){
            if(chestModel.chestState == ChestState.LOCKED){
                chestUnlocker.ShowOptionsToUnlockChest();
            }
            else if(chestModel.chestState == ChestState.UNLOCKING){
                chestUnlocker.ShowOptionsToUnlockChest();
            }
            else{
                OpenChest();
            }
        }

        public void OpenChest(){
            ItemService.Instance.AddCoinsAndGems(chestModel.coins, chestModel.gems);
            ChestService.Instance.popupUI.ShowMessagePopup("Chest Unlocked with 10 coins and 5 gems");
            slot.RemoveChest();
        }

        public void SetChestActive(bool status){
            chestView.gameObject.SetActive(status);
        }
    }
}
