using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UICombat : MonoBehaviour
{
    [SerializeField]
    private List<UICombatEnemiesRow> UIEnemiesRows;
    private List<Transform> cachedUIEnemyPositions;
    public List<Transform> UIEnemyPositions 
    {
        get 
        { 
            return cachedUIEnemyPositions ?? (cachedUIEnemyPositions = UIEnemiesRows.SelectMany((UICombatEnemiesRow row) => row.UIEnemyPositions).ToList()); 
        }
    }
    [SerializeField]
    public Transform UICharacterGrid;
    [SerializeField]
    public UICombatSelectedCharacter UISelectedCharacter;
}
