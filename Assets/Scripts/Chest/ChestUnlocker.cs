using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestUnlocker : MonoBehaviour
    {
        private Queue<ChestModel> unlockQueue;
        private int chestUnlockQueueSize;
        private bool queueActive;
        private int prevRemainingTime;
        private float remainingTime;
        private bool timerActive;
        private float timeReducedPerGem;

        Action<ChestModel> unlockChestWithTime;
        Action<ChestModel> unlockChestWithGems;

        private void Start() {
            unlockChestWithTime += AddChestToUnlockQueue;
            unlockChestWithGems += UnlockChestWithGems;
            unlockQueue = new Queue<ChestModel>();
            timeReducedPerGem = ChestService.Instance.timeReducedPerGem;
            chestUnlockQueueSize = ChestService.Instance.chestUnlockQueueSize;
        }

        public void ShowOptionsToUnlockChest(ChestModel chestModel){
            ChestService.Instance.chestUnlockPopupUI.ShowChestUnlockPopup(unlockChestWithTime, 
                unlockChestWithGems, chestModel);
        }

        private void UnlockChestWithTime(ChestModel chestModel){
            if(chestModel.chestState == ChestState.UNLOCKED){
                RemoveChestFromQueue();
                return;
            }
            timerActive = true;
            remainingTime = chestModel.unlockTime;
            prevRemainingTime = (int)remainingTime;
        }

        private void UnlockChestWithGems(ChestModel chestModel){
            if(chestModel.chestState == ChestState.UNLOCKING){
                if(unlockQueue.Peek() == chestModel && timerActive){
                    timerActive = false;
                    RemoveChestFromQueue();
                }
            }
            ItemService.Instance.RemoveGems(Mathf.CeilToInt(chestModel.remaingUnlockTime/timeReducedPerGem));
            chestModel.SetChestState(ChestState.UNLOCKED);
        }

        private void AddChestToUnlockQueue(ChestModel chestModel){
            chestModel.SetChestState(ChestState.UNLOCKING);
            unlockQueue.Enqueue(chestModel);
            if(!queueActive)
                UnlockChestInQueue();
        }

        private void RemoveChestFromQueue(){
            unlockQueue.Dequeue();
            if(unlockQueue.Count > 0)
                UnlockChestInQueue();
            else
                queueActive = false;
        }

        private void UnlockChestInQueue(){
            queueActive = true;
            UnlockChestWithTime(unlockQueue.Peek());
        }

        public bool IsUnlockQueueFull(){
            return unlockQueue.Count == chestUnlockQueueSize;
        }

        private void Update()
        {
            if(timerActive){
                UpdateChestUnlockTimer();
            }
        }

        private void UpdateChestUnlockTimer(){
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
