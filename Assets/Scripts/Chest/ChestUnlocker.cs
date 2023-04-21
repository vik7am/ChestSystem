using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestUnlocker : MonoBehaviour
    {
        Queue<ChestModel> unlockQueue;
        private int queueSize;
        private bool queueActive;
        Action<ChestModel> unlockChestWithTime;
        Action<ChestModel> unlockChestWithGems;
        private int prevRemainingTime;
        private float remainingTime;
        private bool timerActive;

        private void Start() {
            unlockChestWithTime += AddChestToUnlockQueue;
            unlockChestWithGems += UnlockChestWithGems;
            queueSize = 2;
            unlockQueue = new Queue<ChestModel>();
        }

        public void ShowOptionsToUnlockChest(ChestModel chestModel){
            ChestService.Instance.chestUnlockPopupUI.ShowChestUnlockPopup(unlockChestWithTime, 
                unlockChestWithGems, chestModel);
        }

        public void UnlockChestWithGems(ChestModel chestModel){
            ItemService.Instance.RemoveGems(chestModel.gems.min*2);
            chestModel.SetChestState(ChestState.UNLOCKED);
            if(timerActive){
                timerActive = false;
                RemoveChestFromQueue();
            }
        }

        public void UnlockChestWithTime(ChestModel chestModel){
            timerActive = true;
            remainingTime = chestModel.unlockTime;
            prevRemainingTime = (int)remainingTime;
        }

        public void AddChestToUnlockQueue(ChestModel chestModel){
            chestModel.SetChestState(ChestState.UNLOCKING);
            chestModel.SetRemaingUnlockTime(chestModel.unlockTime);
            unlockQueue.Enqueue(chestModel);
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
            UnlockChestWithTime(unlockQueue.Peek());
        }

        public bool IsUnlockQueueFull(){
            return unlockQueue.Count == queueSize;
        }

        void Update()
        {
            if(timerActive){
                if(remainingTime<=0){
                    timerActive = false;
                    unlockQueue.Peek().SetChestState(ChestState.UNLOCKED);
                    RemoveChestFromQueue();
                    return;
                }
                remainingTime -= Time.deltaTime;
                if(Mathf.CeilToInt(remainingTime) < prevRemainingTime){
                    prevRemainingTime = Mathf.CeilToInt(remainingTime);
                    unlockQueue.Peek().SetRemaingUnlockTime(prevRemainingTime);
                }
            }
        }
    }
}