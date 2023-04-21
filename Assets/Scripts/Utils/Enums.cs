using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public enum ChestType{
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public enum ChestState{
        LOCKED,
        UNLOCKING,
        UNLOCKED
    }
}
