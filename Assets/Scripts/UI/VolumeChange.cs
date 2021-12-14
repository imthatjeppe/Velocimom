using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public Slider slider;
    public TMPro.TMP_Text volumeText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetVolume()
    {
        Settings.volumeMagnitude = slider.value;
        volumeText.text =  Mathf.RoundToInt(Settings.volumeMagnitude*100) + "%";
    }
}
