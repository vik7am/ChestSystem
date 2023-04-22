using UnityEngine;

namespace ChestSystem
{
    [CreateAssetMenu(fileName = "ChestConfigSO", menuName = "Chest/ChestConfigSO")]
    public class ChestConfigSO : ScriptableObject
    {
        public new string name;
        public ItemRange coins;
        public ItemRange gems;
        public int unlockTime;
        public ChestType chestType;
        public Sprite chestSprite;
    }

    [System.Serializable]
    public struct ItemRange{
        public int min;
        public int max;
    }
}
