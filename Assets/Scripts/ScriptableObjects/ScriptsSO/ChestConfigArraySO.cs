using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    [CreateAssetMenu(fileName = "ChestConfigArraySO", menuName = "Chest/ChestConfigArraySO")]
    public class ChestConfigArraySO : ScriptableObject
    {
        [SerializeField]
        public ChestConfigSO[] chestConfigArray;
    }
}
