using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class MessagePopupUI : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI messageGUI;

        void Start(){
            closeButton.onClick.AddListener(ClosePopup);
        }

        public void ShowMessagePopup(string message){
            gameObject.SetActive(true);
            messageGUI.text = message;
        }

        public void ClosePopup(){
            gameObject.SetActive(false);
        }
    }
}
