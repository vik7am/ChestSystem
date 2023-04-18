using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class StatusBarUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsGUI;
        [SerializeField] private TextMeshProUGUI gemsGUI;
        [SerializeField] private Button spawnButton;

        void Start()
        {
            spawnButton.onClick.AddListener(SpawnChest);
        }

        public void SpawnChest(){
            ChestService.Instance.SpawnChest();
        }
    }
}
