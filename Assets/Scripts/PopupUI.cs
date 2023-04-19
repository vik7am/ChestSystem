using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ChestSystem
{
    public class PopupUI : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Image popupBackground;
        [Header("Message Panel")]
        [SerializeField] private GameObject messagePanel;
        [SerializeField] private TextMeshProUGUI messageGUI;
        [Header("Chest Unlock Panel")]
        [SerializeField] private GameObject chestUnlockPanel;
        [SerializeField] private TextMeshProUGUI leftMessageGUI;
        [SerializeField] private Button leftButton;
        [SerializeField] private TextMeshProUGUI rightMessageGUI;
        [SerializeField] private Button rightButton;
        
        private Action leftAction;
        private Action rightAction;

        void Start(){
            closeButton.onClick.AddListener(ClosePopup);
            leftButton.onClick.AddListener(LeftButtonClick);
            rightButton.onClick.AddListener(RightButtonClick);
        }

        public void ShowMessagePopup(string message){
            gameObject.SetActive(true);
            messagePanel.SetActive(true);
            messageGUI.text = message;
        }

        public void ShowChestUnlockPopup(Action leftAction, Action rightAction, ChestModel chestModel){
            gameObject.SetActive(true);
            chestUnlockPanel.SetActive(true);
            this.leftAction = leftAction;
            this.rightAction = rightAction;
            leftMessageGUI.text = "Unlock for free in " + chestModel.unlockTime + " seconds";
            rightMessageGUI.text = "Unlock using " + chestModel.gems*2 + " gems";
        }

        public void ClosePopup(){
            gameObject.SetActive(false);
            messagePanel.SetActive(false);
            chestUnlockPanel.SetActive(false);
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
