using Unity.VisualScripting;
using UnityEngine;


public class Stat
{
    public string statName => statScriptableObject.statName;
    public StatScriptableObject statScriptableObject;
    public float currentValue;
    public float minCurrentValue;
    public float maxCurrentValue;

    public bool isMin => currentValue <= minCurrentValue;
    public bool isMax => currentValue >= maxCurrentValue;

    public Stat(StatScriptableObject statScriptableObject)
    {
        this.statScriptableObject = statScriptableObject;
        currentValue = statScriptableObject.defaultValue;
        minCurrentValue = statScriptableObject.minDefaultValue;
        maxCurrentValue = statScriptableObject.maxDefaultValue;
    }

    public void ChangeValue(float value)
    {
        currentValue = Mathf.Max(Mathf.Min(currentValue + value, maxCurrentValue), minCurrentValue);
    }

    public override string ToString()
    {
        return $"{statName} {currentValue} [{minCurrentValue}/{maxCurrentValue}]";
    }
}
