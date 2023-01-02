using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private AudioMixer mixer = null;
    [SerializeField] private string parameterName = "";
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(parameterName))
        {
            float savedVolume = PlayerPrefs.GetFloat(parameterName);
            volumeSlider.value = savedVolume;
        }
        else
        {
            mixer.GetFloat(parameterName, out float mixerVolume);
            volumeSlider.value = Mathf.Pow(10, mixerVolume / 20);
        }
        
        volumeSlider.minValue = 0.0001f;
        volumeSlider.maxValue = 1f;

        SetVolume(volumeSlider.value);
    }

    public void SetVolume(float normalizedValue)
    {
        mixer.SetFloat(parameterName, Mathf.Log10(normalizedValue) * 20f);
        PlayerPrefs.SetFloat(parameterName, normalizedValue);
    }
    
}
