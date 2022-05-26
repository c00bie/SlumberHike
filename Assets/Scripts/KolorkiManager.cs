using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class KolorkiManager : MonoBehaviour
{
    Volume vol;
    [SerializeField] ScriptableRendererFeature glitch;
    [SerializeField] ScriptableRendererFeature glitch_col;
    [SerializeField] ScriptableRendererFeature inverse;
    [SerializeField] ScriptableRendererData renderData;
    void Start()
    {
        vol = GetComponent<Volume>();
        glitch.SetActive(true);
        glitch_col.SetActive(true);
        StartCoroutine(StartKolorki());
    }

    void OnDisable()
    {
        glitch.SetActive(false);
        glitch_col.SetActive(false);
        inverse.SetActive(false);
        renderData.SetDirty();
    }

    IEnumerator StartKolorki()
    {
        vol.enabled = false;
        while (isActiveAndEnabled)
        {
            vol.enabled = !vol.enabled;
            yield return new WaitForSecondsRealtime(Random.Range(.5f, 2f));
        }
    }

    IEnumerator StartInverse()
    {
        while (isActiveAndEnabled)
        {
            inverse.SetActive(!inverse.isActive);
            renderData.SetDirty();
            yield return new WaitForSecondsRealtime(Random.Range(1f, 3f));
        }
        inverse.SetActive(false);
        renderData.SetDirty();
    }
}