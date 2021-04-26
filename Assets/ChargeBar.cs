using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private static Image BarImage;

    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public static void SetHealthBarValue(float value)
    {
        BarImage.fillAmount = value;
        if (BarImage.fillAmount > 0.66f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (BarImage.fillAmount > 0.33f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else if (BarImage.fillAmount < 0.05)
        {
            SetHealthBarColor(Color.black);
        } else
        {
            SetHealthBarColor(Color.green);
           
        }
    }

    public static float GetHealthBarValue()
    {
        return BarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetHealthBarColor(Color healthColor)
    {
        BarImage.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        BarImage = GetComponent<Image>();
    }
}
