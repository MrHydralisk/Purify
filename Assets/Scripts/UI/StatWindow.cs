using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatWindow : MonoBehaviour
{
    [SerializeField]
    private Text panel;
    public StatsHandler statsHandler;

    private void Update()
    {
        Redraw();
    }

    private void Redraw()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < statsHandler.stats.Count; i++)
        {
            Stat stat = statsHandler.stats[i];
            stringBuilder.AppendLine(stat.ToString());
        }
        panel.text = stringBuilder.ToString();
    }
}
