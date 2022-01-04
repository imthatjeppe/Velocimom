using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{

    public GameObject clockObject;
    public Slider clockSlider;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("DropOff").GetComponent<Timer>();
        clockSlider.maxValue = timer.timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        clockSlider.value = timer.GetTimer();
    }
}
