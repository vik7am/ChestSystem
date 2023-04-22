using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestModel
    {
        public string name {get; private set;}
        public ItemRange coins {get; private set;}
        public ItemRange gems {get; private set;}
        public int unlockTime {get; private set;}
        public ChestType chestType {get; private set;}
        public Sprite chestSprite {get; private set;}
        public int remaingUnlockTime {get; private set;}
        public ChestState chestState {get; private set;}

        public event Action<ChestState> onStateChange;
        public event Action<int> onRemaingUnlockTimeChange;
        
        public ChestModel(ChestConfigSO chestConfigSO){
            name = chestConfigSO.name;
            coins = chestConfigSO.coins;
            gems = chestConfigSO.gems;
            unlockTime = chestConfigSO.unlockTime;
            chestType = chestConfigSO.chestType;
            chestSprite = chestConfigSO.chestSprite;

            chestState = ChestState.LOCKED;
            remaingUnlockTime = unlockTime;
        }

        public void SetChestState(ChestState chestState){
            this.chestState = chestState;
            onStateChange?.Invoke(chestState);
        }

        public void SetRemaingUnlockTime(int remaingUnlockTime){
            this.remaingUnlockTime = remaingUnlockTime;
            onRemaingUnlockTimeChange?.Invoke(remaingUnlockTime);
        }
    }
}
