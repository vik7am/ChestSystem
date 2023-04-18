using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ItemService : GenericMonoSingleton<ItemService>
    {
        [field: SerializeField] public int coins {get; private set;}
        [field: SerializeField] public int gems {get; private set;}
        [SerializeField] private StatusBarUI statusBarUI;

        private void Start() {
            statusBarUI.UpdateStatusBar(coins, gems);
        }

        public void AddCoinsAndGems(int coins, int gems){
            coins += coins;
            gems += gems;
            UpdateItemsOnStatusBarUI();
        }

        void UpdateItemsOnStatusBarUI(){
            statusBarUI.UpdateStatusBar(coins, gems);
        }
    }
}
