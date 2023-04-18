using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestController
    {
        ChestView chestView;
        ChestModel ChestModel;

        public ChestController(ChestModel chestModel, ChestView chestView){
            this.chestView = chestView;
            this.ChestModel = chestModel;
            SetChestActive(true);
        }

        public void SetChestActive(bool status){
            chestView.gameObject.SetActive(status);
        }
    }
}
