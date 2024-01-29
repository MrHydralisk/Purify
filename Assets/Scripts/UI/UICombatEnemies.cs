using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UICombatEnemies : MonoBehaviour
{
    [SerializeField]
    private List<UICombatEnemiesRow> UIEnemiesRows;
    private List<GameObject> cachedUIEnemyPositions;
    public List<GameObject> UIEnemyPositions 
    {
        get 
        { 
            return cachedUIEnemyPositions ?? (cachedUIEnemyPositions = UIEnemiesRows.SelectMany((UICombatEnemiesRow row) => row.UIEnemyPositions).ToList()); 
        }
    }
}
