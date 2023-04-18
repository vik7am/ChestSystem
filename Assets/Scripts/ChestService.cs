using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestService : GenericMonoSingleton<ChestService>
    {
        Inventory inventory;
        [SerializeField] private InventoryUI inventoryUI;

        private void Start() {
            inventory = new Inventory(inventoryUI);
        }

        public void SpawnChest(){
            
            inventory.SpawnChest();
        }
    }
}
