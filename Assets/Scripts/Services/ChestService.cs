using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestService : GenericMonoSingleton<ChestService>
    {
        public Inventory inventory {get; private set;}
        [SerializeField] private InventoryUI inventoryUI;
        [field: SerializeField] public ChestUnlockPopupUI chestUnlockPopupUI {get; private set;}
        [field: SerializeField] public MessagePopupUI messagePopupUI {get; private set;}
        [field: SerializeField] public ChestConfigArraySO chestConfigArraySO {get; private set;}
        [field: SerializeField] public float timeReducedPerGem {get; private set;}

        private void Start() {
            inventory = new Inventory(inventoryUI);
        }

        public void SpawnChest(){
            if(inventory.SpawnChest() == false){
                messagePopupUI.ShowMessagePopup("All slots are Full.");
            }
        }
    }
}
