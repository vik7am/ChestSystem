using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;

        public void SetSprite(Sprite sprite){
            chestImage.sprite = sprite;
        }
    }
}
