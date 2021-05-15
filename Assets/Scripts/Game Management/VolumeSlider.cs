using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public string volumeParameter = "Master";
    public AudioMixer mixer;
    public Slider slider;
    private float multiplier = 30f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderChanged);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void HandleSliderChanged(float value)
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
        Debug.Log(Mathf.Log(value) * multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
