﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxVelocity(float velocity)
    {
        slider.maxValue = velocity;
        slider.value = velocity;
    }

    public void SetVelocity(float velocity)
    {
        if(velocity > slider.maxValue) slider.value = slider.maxValue;
        slider.value = velocity;
    }
}
