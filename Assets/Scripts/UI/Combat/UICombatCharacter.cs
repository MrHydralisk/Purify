using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UICombatCharacter : MonoBehaviour
{
    [SerializeField]
    private UIStat statHP;

    public void RedrawStats(StatsHandler statsHandler)
    {
        statHP.RedrawStats(statsHandler.stats[0]);
    }
}
