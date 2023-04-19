using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestUnlocker : MonoBehaviour
    {
        ChestController chestController;
        ChestModel chestModel;
        Action unlockChestWithTime;
        Action unlockChestWithGems;
        private int prevRemainingTime;
        private float remainingTime;
        private bool timerActive;

        private void Start() {
            unlockChestWithTime += UnlockChestWithTime;
            unlockChestWithGems += UnlockChestWithGems;
        }

        public void SetChestController(ChestController chestController){
            this.chestController = chestController;
            this.chestModel = chestController.chestModel;
        }

        public void ShowOptionsToUnlockChest(){
            ChestService.Instance.popupUI.ShowChestUnlockPopup(unlockChestWithTime, 
                unlockChestWithGems, chestModel);
        }

        public void UnlockChestWithGems(){
            ItemService.Instance.RemoveGems(chestModel.gems*2);
            chestController.chestModel.SetChestState(ChestState.OPEN);
        }

        public void UnlockChestWithTime(){
            timerActive = true;
            remainingTime = chestController.chestModel.unlockTime;
            prevRemainingTime = (int)remainingTime;
            chestController.chestModel.SetChestState(ChestState.UNLOCKING);
            chestController.chestModel.SetRemaingUnlockTime(prevRemainingTime);
        }

        void Update()
        {
            if(timerActive){
                if(remainingTime<=0){
                    timerActive = false;
                    chestController.chestModel.SetChestState(ChestState.OPEN);
                    return;
                }
                remainingTime -= Time.deltaTime;
                if(Mathf.CeilToInt(remainingTime) < prevRemainingTime){
                    prevRemainingTime = Mathf.CeilToInt(remainingTime);
                    chestController.chestModel.SetRemaingUnlockTime(prevRemainingTime);
                }
            }
        }
    }
}
