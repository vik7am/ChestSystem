using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestService : GenericMonoSingleton<ChestService>
    {
        Inventory inventory;
        [SerializeField] private InventoryUI inventoryUI;
        [field: SerializeField] public PopupUI popupUI {get; private set;}

        private void Start() {
            inventory = new Inventory(inventoryUI);
        }

        public void SpawnChest(){
            if(inventory.SpawnChest() == false){
                popupUI.ShowMessagePopup("All slots are Full.");
            }
        }
    }
}
