using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ChestSystem
{
    public class ChestUnlockPopupUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI chestTypeGUI;
        [SerializeField] private TextMeshProUGUI coinsDropRangeGUI;
        [SerializeField] private TextMeshProUGUI gemsDropRangeGUI;

        [SerializeField] private TextMeshProUGUI unlockTimeGUI;
        [SerializeField] private TextMeshProUGUI unlockCostGUI;
        [SerializeField] private TextMeshProUGUI slowUnlockStatusGUI;
        [SerializeField] private TextMeshProUGUI instantUnlockStatusGUI;
        [SerializeField] private Button slowUnlockButton;
        [SerializeField] private Button instantUnlockButton;
        [SerializeField] private Button closeButton;
        private ChestModel chestModel;
        
        private Action<ChestModel> leftAction;
        private Action<ChestModel> rightAction;

        void Start(){
            closeButton.onClick.AddListener(ClosePopup);
            slowUnlockButton.onClick.AddListener(LeftButtonClick);
            instantUnlockButton.onClick.AddListener(RightButtonClick);
        }

        public void ShowChestUnlockPopup(Action<ChestModel> leftAction, Action<ChestModel> rightAction, ChestModel chestModel){
            this.chestModel = chestModel;
            gameObject.SetActive(true);
            instantUnlockButton.interactable = true;
            slowUnlockButton.interactable = true;
            UpdateChestInfo();
            UpdateChestUnlockOptions();
            RegisterForChestEvents();
            this.leftAction = leftAction;
            this.rightAction = rightAction;
        }

        public void UpdateChestInfo(){
            chestTypeGUI.text = "Chest Type : " + chestModel.name;
            coinsDropRangeGUI.text = "Coins : " + chestModel.coins;
            coinsDropRangeGUI.text = "Gems : " + chestModel.gems;
            unlockTimeGUI.text = "Time : " + chestModel.unlockTime + " sec";
            unlockCostGUI.text = "Cost : " + chestModel.unlockTime*2 + " gems";
        }

        public void UpdateChestUnlockOptions(){
            if(chestModel.chestState == ChestState.LOCKED){
                if(ChestService.Instance.inventory.chestUnlocker.IsUnlockQueueFull()){
                    slowUnlockButton.interactable = false;
                    slowUnlockStatusGUI.text = "Chest Unlocking Queue is Full";
                }
            }
            if(chestModel.chestState == ChestState.UNLOCKING){
                slowUnlockButton.interactable = false;
                slowUnlockStatusGUI.text = "Unlock in Progress";
            }
        }

        public void UpdateChestChanges(ChestState chestState){
            if(chestState == ChestState.UNLOCKING){
                slowUnlockButton.interactable = false;
                slowUnlockStatusGUI.text = "Chest will unlock ater " + chestModel.unlockTime + " sec";
            }
            else if(chestState == ChestState.UNLOCKED){
                slowUnlockButton.interactable = false;
                instantUnlockButton.interactable = false;
                slowUnlockStatusGUI.text = "Chest already Unlocked";
                instantUnlockStatusGUI.text = "Chest already Unlocked";
            }
        }

        public void UpdateChestChanges(int unlockTime){
            slowUnlockStatusGUI.text = "Chest will unlock ater " + unlockTime + " sec";
            int time = Mathf.CeilToInt((unlockTime*2)/10) * 10;
            unlockCostGUI.text = "Cost : " + time + " gems";
        }

        public void RegisterForChestEvents(){
            chestModel.onStateChange += UpdateChestChanges;
            chestModel.onRemaingUnlockTimeChange += UpdateChestChanges;
        }

        public void UnregisterForChestEvents(){
            chestModel.onStateChange -= UpdateChestChanges;
            chestModel.onRemaingUnlockTimeChange -= UpdateChestChanges;
        }

        public void ClosePopup(){
            UnregisterForChestEvents();
            gameObject.SetActive(false);
        }

        public void LeftButtonClick(){
            leftAction?.Invoke(chestModel);
            ClosePopup();
        }

        public void RightButtonClick(){
            rightAction?.Invoke(chestModel);
            ClosePopup();
        }
    }
}