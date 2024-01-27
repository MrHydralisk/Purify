using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public List<Stat> stats = new List<Stat>();
    [SerializeField]
    public List<StatScriptableObject> statScriptableObjects = new List<StatScriptableObject>();

    private void Awake()
    {
        foreach (StatScriptableObject sso in statScriptableObjects)
        {
            stats.Add(new Stat(sso));
        }
    }

    public Stat GetStat(string statName)
    {
        return stats.FirstOrDefault((Stat s) => s.statName == statName);
    }
}
