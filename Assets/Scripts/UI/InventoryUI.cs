using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class InventoryUI : MonoBehaviour
    {
        [field: SerializeField] public SlotUI[] slotUIArray {get; private set;}
    }
}