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
            actionButton.onClick.AddListener(OnButtonClick);
        }

        private void Start() {
            statusGUI.text = "EMPTY";
        }

        public void RegisterForChestEvents(ChestModel chestModel){
            chestModel.onStateChange += UpdateChestStatus;
            chestModel.onRemaingUnlockTimeChange += UpdateChestStatus;
            UpdateChestStatus(chestModel.chestState);
        }

        public void UnregisterForChestEvents(ChestModel chestModel){
            chestModel.onStateChange -= UpdateChestStatus;
            chestModel.onRemaingUnlockTimeChange -= UpdateChestStatus;
        }

        public void UpdateChestStatus(ChestState chestState){
            if(chestState == ChestState.LOCKED){
                statusGUI.text = "Locked";
            } 
            else if(chestState == ChestState.UNLOCKING){
                statusGUI.text = "Unlocking";
            }
            else if(chestState == ChestState.UNLOCKED){
                statusGUI.text = "Unlocked";
            }
        }

        public void UpdateChestStatus(int remainingUnlockTime){
            statusGUI.text = remainingUnlockTime.ToString();
        }

        public void SetSlot(Slot slot){
            this.slot = slot;
        }

        private void OnButtonClick(){
            slot.TryToOpenChest();
        }
    }
}
