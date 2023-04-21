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
        [SerializeField] private Image chestImage;
        [field: SerializeField] public int timeReducedPerGem {get; private set;}
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
            chestImage.sprite = chestModel.chestSprite;
            chestTypeGUI.text = "Chest Type : " + chestModel.name;
            coinsDropRangeGUI.text = "Coins : " + chestModel.coins.min + " - " + chestModel.coins.max;
            gemsDropRangeGUI.text = "Gems : " + chestModel.gems.min + " - " + chestModel.gems.max;
            unlockTimeGUI.text = "Time : " + chestModel.unlockTime + " sec";
            unlockCostGUI.text = "Cost : " + chestModel.unlockTime/timeReducedPerGem + " gems";
            slowUnlockStatusGUI.text = "Click Unlock Button to unlock chest at a slower speed";
            instantUnlockStatusGUI.text = "Click Unlock Button to unlock chest instantly with gems";
        }

        public void UpdateChestUnlockOptions(){
            if(chestModel.chestState == ChestState.LOCKED){
                if(ChestService.Instance.inventory.chestUnlocker.IsUnlockQueueFull()){
                    slowUnlockButton.interactable = false;
                    slowUnlockStatusGUI.text = "Chest Unlocking Queue is Full";
                }
                if(ItemService.Instance.gems < chestModel.unlockTime/timeReducedPerGem){
                    instantUnlockButton.interactable = false;
                    instantUnlockStatusGUI.text = "You don't have required no of gems";
                }
            }
            if(chestModel.chestState == ChestState.UNLOCKING){
                slowUnlockButton.interactable = false;
                slowUnlockStatusGUI.text = "Unlock in Progress";
                if(ItemService.Instance.gems < chestModel.unlockTime/timeReducedPerGem){
                    instantUnlockButton.interactable = false;
                    instantUnlockStatusGUI.text = "You don't have required no of gems";
                }
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
            int unlockCost = Mathf.CeilToInt(unlockTime/timeReducedPerGem);
            unlockCostGUI.text = "Cost : " + unlockCost + " gems";
            if(!instantUnlockButton.IsInteractable()){
                if(unlockCost <= ItemService.Instance.gems){
                    instantUnlockButton.interactable = true;
                    instantUnlockStatusGUI.text = "Click Unlock Button to unlock chest instantly with gems";
                }
            }
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
