using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestUnlocker : MonoBehaviour
    {
        private Queue<ChestModel> chestUnlockQueue;
        private int chestUnlockQueueSize;
        private bool queueActive;
        private int prevRemainingTime;
        private float remainingTime;
        private bool chestUnlockProcessActive;
        private float timeReducedPerGem;

        Action<ChestModel> unlockChestWithTime;
        Action<ChestModel> unlockChestWithGems;

        private void Start() {
            unlockChestWithTime += AddChestToUnlockQueue;
            unlockChestWithGems += UnlockChestWithGems;
            chestUnlockQueue = new Queue<ChestModel>();
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
            chestUnlockProcessActive = true;
            remainingTime = chestModel.unlockTime;
            prevRemainingTime = (int)remainingTime;
        }

        private void UnlockChestWithGems(ChestModel chestModel){
            if(chestModel.chestState == ChestState.UNLOCKING){
                if(chestUnlockQueue.Peek() == chestModel && chestUnlockProcessActive){
                    chestUnlockProcessActive = false;
                    RemoveChestFromQueue();
                }
            }
            ItemService.Instance.RemoveGems(Mathf.CeilToInt(chestModel.remaingUnlockTime/timeReducedPerGem));
            chestModel.SetChestState(ChestState.UNLOCKED);
        }

        private void AddChestToUnlockQueue(ChestModel chestModel){
            chestModel.SetChestState(ChestState.UNLOCKING);
            chestUnlockQueue.Enqueue(chestModel);
            if(!queueActive)
                UnlockChestInQueue();
        }

        private void RemoveChestFromQueue(){
            chestUnlockQueue.Dequeue();
            if(chestUnlockQueue.Count > 0)
                UnlockChestInQueue();
            else
                queueActive = false;
        }

        private void UnlockChestInQueue(){
            queueActive = true;
            UnlockChestWithTime(chestUnlockQueue.Peek());
        }

        public bool IsUnlockQueueFull(){
            return chestUnlockQueue.Count == chestUnlockQueueSize;
        }

        private void Update()
        {
            if(chestUnlockProcessActive){
                UpdateChestUnlockTimer();
            }
        }

        private void UpdateChestUnlockTimer(){
            if(remainingTime<=0){
                chestUnlockProcessActive = false;
                chestUnlockQueue.Peek().SetChestState(ChestState.UNLOCKED);
                RemoveChestFromQueue();
                return;
            }
            remainingTime -= Time.deltaTime;
            //Updating chest remaningUnlock time after 1 sec has passed
            if(Mathf.CeilToInt(remainingTime) < prevRemainingTime){
                prevRemainingTime = Mathf.CeilToInt(remainingTime);
                chestUnlockQueue.Peek().SetRemaingUnlockTime(prevRemainingTime);
            }
        }
    }
}
