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
        
        private Action leftAction;
        private Action rightAction;

        void Start(){
            closeButton.onClick.AddListener(ClosePopup);
            leftButton.onClick.AddListener(LeftButtonClick);
            rightButton.onClick.AddListener(RightButtonClick);
        }

        public void ShowChestUnlockPopup(Action leftAction, Action rightAction, ChestModel chestModel){
            gameObject.SetActive(true);
            if(chestModel.chestState == ChestState.LOCKED){
                leftButton.interactable = true;
                leftMessageGUI.text = "Unlock for free in " + chestModel.unlockTime + " seconds";
            }
            else if(chestModel.chestState == ChestState.UNLOCKING){
                leftButton.interactable = false;
                leftMessageGUI.text = "Unlock in Progress";
            }
            this.leftAction = leftAction;
            this.rightAction = rightAction;
            rightMessageGUI.text = "Unlock using " + chestModel.gems*2 + " gems";
        }

        public void ClosePopup(){
            gameObject.SetActive(false);
        }

        public void LeftButtonClick(){
            leftAction?.Invoke();
            ClosePopup();
        }

        public void RightButtonClick(){
            rightAction?.Invoke();
            ClosePopup();
        }
    }
}
