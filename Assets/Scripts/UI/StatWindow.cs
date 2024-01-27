using System.Collections;
using System.Collections.Generic;
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
        panel.text = "";
        for (int i = 0; i < statsHandler.stats.Count; i++)
        {
            Stat stat = statsHandler.stats[i];
            panel.text += stat.ToString() + "\n";
        }
    }
}
