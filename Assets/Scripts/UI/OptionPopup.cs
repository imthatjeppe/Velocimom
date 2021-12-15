using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopup : MonoBehaviour
{
    public GameObject optionsPopup;

    public static bool OptionIsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        optionsPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (OptionIsOn)
            {
                OptionsPopupOff();
            }

            else
            {
                OptionsPopupOn();
            }
        }
    }

    void OptionsPopupOff()
    {
        optionsPopup.SetActive(false);
        Time.timeScale = 1f;
        OptionIsOn = false;
    }

    void OptionsPopupOn()
    {
        optionsPopup.SetActive(true);
        Time.timeScale = 0f;
        OptionIsOn = true;
    }
}
