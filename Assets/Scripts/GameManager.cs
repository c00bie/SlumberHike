using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SH.Managers {
    public class GameManager : MonoBehaviour
    {

        [SerializeField] ScriptableRendererFeature glitch;
        [SerializeField] ScriptableRendererData renderData;
        private void Start()
        {
            glitch.SetActive(false);
            renderData.SetDirty();
        }

        void Update()
        {
        }
    }
}
