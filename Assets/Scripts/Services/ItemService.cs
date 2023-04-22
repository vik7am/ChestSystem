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

        public void AddCoins(int coins){
            this.coins += coins;
            UpdateItemsOnStatusBarUI();
        }

        public void AddGems(int gems){
            this.gems = gems;
            UpdateItemsOnStatusBarUI();
        }

        public void RemoveGems(int gems){
            this.gems -= gems;
            UpdateItemsOnStatusBarUI();
        }

        private void UpdateItemsOnStatusBarUI(){
            statusBarUI.UpdateStatusBar(coins, gems);
        }
    }
}
