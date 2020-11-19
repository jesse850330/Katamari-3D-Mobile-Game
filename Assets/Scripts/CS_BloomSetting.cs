using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CS_BloomSetting : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    public float LowIntensity;
    public float HighIntensity;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        InvokeRepeating("ChangeIntensity", 1f, 1f);
    }

    void ChangeIntensity()
    {
        m_Vignette.intensity.Override(Random.Range(LowIntensity, HighIntensity));
    }
}
