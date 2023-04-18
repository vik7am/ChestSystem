using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class SlotUI : MonoBehaviour
    {
        [field: SerializeField] public ChestView chestView {get; private set;}
        [SerializeField] public TextMeshProUGUI statusGUI;
        [SerializeField] public Button actionButton;
        private Slot slot;

        private void Awake(){
            actionButton.onClick.AddListener(ActionButtonClick);
        }

        private void Start() {
            statusGUI.text = "EMPTY";
        }

        public void SetSlot(Slot slot){
            this.slot = slot;
        }

        private void ActionButtonClick(){
            slot.ShowChestUnlockOption();
        }
    }
}
