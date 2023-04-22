using UnityEngine;

namespace ChestSystem
{
    public class ChestController
    {
        private ChestView chestView;
        public ChestModel chestModel {get; private set;}
        public ChestUnlocker chestUnlocker {get; private set;}
        private Slot slot;

        public ChestController(Slot slot, ChestModel chestModel, ChestView chestView){
            this.slot = slot;
            this.chestView = chestView;
            this.chestModel = chestModel;
            chestView.SetSprite(chestModel.chestSprite);
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
            else if(chestModel.chestState == ChestState.UNLOCKED){
                OpenChest();
            }
        }

        private void OpenChest(){
            int randomCoins = Random.Range(chestModel.coins.min, chestModel.coins.max);
            int randomGems = Random.Range(chestModel.gems.min, chestModel.gems.max);
            ItemService.Instance.AddCoins(randomCoins);
            ItemService.Instance.AddGems(randomGems);
            string message = "Chest opened with " + randomCoins + " coins and " + randomGems + " gems";
            ChestService.Instance.messagePopupUI.ShowMessagePopup(message);
            slot.RemoveChest();
        }

        public void SetChestActive(bool status){
            chestView.gameObject.SetActive(status);
        }
    }
}
