using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider staminaSlider;
    public Image background;
    public Image fillArea;

    private void Awake()
    {
        staminaSlider.enabled = false;
        background.enabled = false;
        fillArea.enabled = false;
    }

    void Update()
    {
        if (GameManager.GM.gameStateStarted == true)
        {
            // Display only when game has started.
            staminaSlider.enabled = true;
            background.enabled = true;
            fillArea.enabled = true;
        }

        //Match stamina value
        staminaSlider.value = GameManager.GM.stamina;
    }
}
