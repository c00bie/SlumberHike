using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SH.Interactions
{
    public class PostProcessingInteraction : Interaction
    {
        [SerializeField] ScriptableRendererFeature glitch;
        [SerializeField] ScriptableRendererFeature glitchColor;
        [SerializeField] ScriptableRendererFeature invert;
        [SerializeField] ScriptableRendererData data;
        [SerializeField] bool useGlitch;
        [SerializeField] bool useInvert;
        [SerializeField] bool useGlitchColor;
        [SerializeField] bool targetState;

        public override bool IsAsync => false;

        public override void DoAction()
        {
            if (useGlitch)
                glitch.SetActive(targetState);
            if (useInvert)
                invert.SetActive(targetState);
            if (useGlitchColor)
                glitchColor.SetActive(targetState);
            data.SetDirty();
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}