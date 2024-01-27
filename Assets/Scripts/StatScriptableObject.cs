using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatType", menuName = "ScriptableObjects/StatScriptableObject")]
public class StatScriptableObject : ScriptableObject
{
    public string statName;
    [SerializeField]
    public float minValue = 0;
    [SerializeField]
    public float maxValue = 100;
    public float defaultValue;
    public float minDefaultValue = 0;
    public float maxDefaultValue = 100;

    public override string ToString()
    {
        return $"{statName}|{minValue}|{maxValue}|{defaultValue}|{minDefaultValue}|{maxDefaultValue}";     
    }
}
