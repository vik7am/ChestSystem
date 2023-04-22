using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ChestSystem
{
    public class ChestUnlockPopupUI : MonoBehaviour
    {
        [SerializeField] private Image chestImage;
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
        
        private float timeReducedPerGem;
        private ChestModel chestModel;
        
        private Action<ChestModel> slowUnlockAction;
        private Action<ChestModel> instantUnlockAction;

        private void Awake() {
            timeReducedPerGem = ChestService.Instance.timeReducedPerGem;
        }

        private void Start(){
            closeButton.onClick.AddListener(ClosePopup);
            slowUnlockButton.onClick.AddListener(SlowUnlockButtonClick);
            instantUnlockButton.onClick.AddListener(InstantUnlockButtonClick);
        }

        public void ShowChestUnlockPopup(Action<ChestModel> slowUnlockAction, Action<ChestModel> instantUnlockAction, ChestModel chestModel){
            this.chestModel = chestModel;
            gameObject.SetActive(true);
            instantUnlockButton.interactable = true;
            slowUnlockButton.interactable = true;
            UpdateChestInfo();
            UpdateChestUnlockOptions();
            RegisterForChestEvents();
            this.slowUnlockAction = slowUnlockAction;
            this.instantUnlockAction = instantUnlockAction;
        }

        private void UpdateChestInfo(){
            chestImage.sprite = chestModel.chestSprite;
            chestTypeGUI.text = "Chest Type : " + chestModel.name;
            coinsDropRangeGUI.text = "Coins : " + chestModel.coins.min + " - " + chestModel.coins.max;
            gemsDropRangeGUI.text = "Gems : " + chestModel.gems.min + " - " + chestModel.gems.max;
            unlockTimeGUI.text = "Time : " + chestModel.unlockTime + " sec";
            unlockCostGUI.text = "Cost : " + chestModel.unlockTime/timeReducedPerGem + " gems";
            slowUnlockStatusGUI.text = "Click Unlock Button to unlock chest at a slower speed";
            instantUnlockStatusGUI.text = "Click Unlock Button to unlock chest instantly with gems";
        }

        private void UpdateChestUnlockOptions(){
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

        private void UpdateChestChanges(ChestState chestState){
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

        private void UpdateChestChanges(int unlockTime){
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

        private void RegisterForChestEvents(){
            chestModel.onStateChange += UpdateChestChanges;
            chestModel.onRemaingUnlockTimeChange += UpdateChestChanges;
        }

        private void UnregisterForChestEvents(){
            chestModel.onStateChange -= UpdateChestChanges;
            chestModel.onRemaingUnlockTimeChange -= UpdateChestChanges;
        }

        private void ClosePopup(){
            UnregisterForChestEvents();
            gameObject.SetActive(false);
        }

        private void SlowUnlockButtonClick(){
            slowUnlockAction?.Invoke(chestModel);
            ClosePopup();
        }

        private void InstantUnlockButtonClick(){
            instantUnlockAction?.Invoke(chestModel);
            ClosePopup();
        }
    }
}
