using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class SlotUI : MonoBehaviour
    {
        [field: SerializeField] public ChestView chestView {get; private set;}
        [SerializeField] public TextMeshProUGUI statusGUI;
        [SerializeField] public Button openChestButton;
        private Slot slot;

        private void Awake(){
            openChestButton.onClick.AddListener(OpenChestButtonClick);
        }

        private void Start() {
            UpdateStatusText("Empty");
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
                UpdateStatusText("Locked");
            } 
            else if(chestState == ChestState.UNLOCKING){
                UpdateStatusText("Unlocking");
            }
            else if(chestState == ChestState.UNLOCKED){
                UpdateStatusText("Unlocked");
            }
        }

        public void UpdateChestStatus(int remainingUnlockTime){
            statusGUI.text = remainingUnlockTime.ToString();
        }

        public void SetSlot(Slot slot){
            this.slot = slot;
        }

        private void OpenChestButtonClick(){
            slot.TryToOpenChest();
        }

        public void OpenChestButtonActive(bool status){
            openChestButton.gameObject.SetActive(status);
        }

        public void UpdateStatusText(string text){
            statusGUI.text = text;
        }
    }
}
