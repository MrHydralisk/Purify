using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStat : MonoBehaviour
{
    [SerializeField]
    private Text statText;
    [SerializeField]
    private Slider statSlider;

    public void RedrawStats(Stat stat)
    {
        statSlider.value = stat.currentValue;
        statText.text = $"{stat.currentValue}/{stat.maxCurrentValue}";
    }
}
