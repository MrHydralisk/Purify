using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UICombatCharacter : MonoBehaviour
{
    [SerializeField]
    private Text panelStats;

    public void RedrawStats(StatsHandler statsHandler)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < statsHandler.stats.Count; i++)
        {
            Stat stat = statsHandler.stats[i];
            stringBuilder.AppendLine(stat.ToString());
        }
        panelStats.text = stringBuilder.ToString();
    }
}
