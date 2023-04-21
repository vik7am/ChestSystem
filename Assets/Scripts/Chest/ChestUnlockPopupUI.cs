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
        [SerializeField] private TextMeshProUGUI leftMessageGUI;
        [SerializeField] private Button leftButton;
        [SerializeField] private TextMeshProUGUI rightMessageGUI;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button closeButton;
        private ChestModel chestModel;
        
        private Action<ChestModel> leftAction;
        private Action<ChestModel> rightAction;

        void Start(){
            closeButton.onClick.AddListener(ClosePopup);
            leftButton.onClick.AddListener(LeftButtonClick);
            rightButton.onClick.AddListener(RightButtonClick);
        }

        public void ShowChestUnlockPopup(Action<ChestModel> leftAction, Action<ChestModel> rightAction, ChestModel chestModel){
            this.chestModel = chestModel;
            gameObject.SetActive(true);
            if(chestModel.chestState == ChestState.LOCKED){
                if(ChestService.Instance.inventory.chestUnlocker.IsUnlockQueueFull()){
                    leftButton.interactable = false;
                    leftMessageGUI.text = "Chest Unlocking Queue is Full";
                }
                else{
                    leftButton.interactable = true;
                    leftMessageGUI.text = "Unlock for free in " + chestModel.unlockTime + " seconds";
                }
            }
            else if(chestModel.chestState == ChestState.UNLOCKING){
                leftButton.interactable = false;
                leftMessageGUI.text = "Unlock in Progress";
            }
            this.leftAction = leftAction;
            this.rightAction = rightAction;
            rightMessageGUI.text = "Unlock using " + chestModel.gems.min*2 + " gems";
        }

        public void ClosePopup(){
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
