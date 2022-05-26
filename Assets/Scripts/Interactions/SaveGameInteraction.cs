using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Interactions
{
    public class SaveGameInteraction : Interaction
    {
        public override bool IsAsync => false;
        public override void DoAction()
        {
            Data.SaveGame.SavePlayer(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetActiveScene(), Camera.main.transform.position);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}