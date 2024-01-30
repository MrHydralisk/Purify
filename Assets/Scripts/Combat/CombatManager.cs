using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    public List<CombatCreature> enemies;

    [SerializeField]
    private UICombatEnemies UIElements;

    [SerializeField]
    private GameObject UIEnemyPrefab;

    [SerializeField]
    private GameObject CombatUI;

    public void InitiateCombat(int EnemyCount = 1/*List<CombatEnemyType> combatEnemyTypes*/)
    {
        StartBattle(); //Temp

        enemies = new List<CombatCreature>();

        for (int i = 0; i < EnemyCount; i++)
        {
            CombatCreature cc = new CombatCreature();
            GameObject go = Instantiate(UIEnemyPrefab, UIElements.UIEnemyPositions.First(g => g.transform.childCount == 0).transform);
            cc.UIElements = go.GetComponent<UICombatEnemy>();
            enemies.Add(cc);
        }
    }

    public void AttackEnemy()
    {
        if (enemies.Count > 0)
        {
            enemies.First()?.Damage(50);
        }
        EndTurn();
    }

    private void EndTurn()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameManager.instance.player.Damage(1);
            }
        }
        else
        {
            EndBattle();
        }
    }

    public void DestroyEnemy(CombatCreature combatCreature)
    {
        enemies.Remove(combatCreature);
        Destroy(combatCreature.UIElements.gameObject);
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void StartBattle()
    {
        UIManager.instance.DisableUI();
        GameManager.instance.PauseGame();
        CombatUI.SetActive(true);
        Debug.Log("Battle start");
    }

    private void EndBattle()
    {
        Debug.Log("Battle end");
        UIManager.instance.EnableUI();
        GameManager.instance.ResumeGame();
        CombatUI.SetActive(false);
    }
}
