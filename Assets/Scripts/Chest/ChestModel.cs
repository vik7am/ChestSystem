using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChestSystem
{
    public class ChestModel
    {
        public int coins {get; private set;}
        public int gems {get; private set;}
        public ChestState chestState {get; private set;}
        public int unlockTime {get; private set;}
        public int remaingUnlockTime {get; private set;}
        public event Action<ChestState> onStateChange;
        public event Action<int> onRemaingUnlockTimeChange;
        
        public ChestModel(){
            chestState = ChestState.LOCKED;
            unlockTime = 7;
            coins = 10;
            gems= 5;
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

    public enum ChestState{
        LOCKED,
        UNLOCKING,
        OPEN
    }
}
