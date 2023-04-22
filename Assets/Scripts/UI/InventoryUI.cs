using UnityEngine;

namespace ChestSystem
{
    public class InventoryUI : MonoBehaviour
    {
        [field: SerializeField] public SlotUI[] slotUIArray {get; private set;}
    }
}
