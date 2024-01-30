using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    public List<CombatCreature> enemies;
    public List<CombatCharacter> playerCharacters;

    [SerializeField]
    private UICombat UIElements;

    [SerializeField]
    private GameObject UIEnemyPrefab;
    [SerializeField]
    private GameObject UICharacterPrefab;

    private void InitiateCombat(int EnemyCount = 1/*List<CombatEnemyType> combatEnemyTypes*/)
    {
        enemies = new List<CombatCreature>();

        for (int i = 0; i < EnemyCount; i++)
        {
            CombatCreature cc = new CombatCreature();
            GameObject go = Instantiate(UIEnemyPrefab, UIElements.UIEnemyPositions.First(g => g.childCount == 0));
            cc.UIElements = go.GetComponent<UICombatEnemy>();
            enemies.Add(cc);
        }

        playerCharacters = new List<CombatCharacter>();

        for (int i = 0; i < 1; i++)
        {
            CombatCharacter cc = new CombatCharacter();
            GameObject go = Instantiate(UICharacterPrefab, UIElements.UICharacterGrid);
            cc.UIElements = go.GetComponent<UICombatCharacter>();
            playerCharacters.Add(cc);
        }

        UpdatePlayerCharacterStats();
    }

    public void CleanCombat()
    {
        for (int i = UIElements.UICharacterGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(UIElements.UICharacterGrid.GetChild(i).gameObject);
        }
        for (int i = enemies.Count() - 1; i >= 0; i--)
        {
            Destroy(enemies[i].UIElements.gameObject);
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

    public void DestroyEnemy(CombatCreature combatCreature)
    {
        enemies.Remove(combatCreature);
        Destroy(combatCreature.UIElements.gameObject);
    }

    public void AttackPlayerCharacter()
    {
        GameManager.instance.player.Damage(1);
        UpdatePlayerCharacterStats();
    }

    public void UpdatePlayerCharacterStats()
    {
        playerCharacters.First().UIElements.RedrawStats(GameManager.instance.player.statsHandler);
    }

    private void EndTurn()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                AttackPlayerCharacter();
            }
        }
        else
        {
            EndBattle();
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartBattle(int EnemyCount = 1/*List<CombatEnemyType> combatEnemyTypes*/)
    {
        UIManager.instance.DisableUI();
        GameManager.instance.PauseGame();
        InitiateCombat(EnemyCount);
        UIElements.gameObject.SetActive(true);
        Debug.Log("Battle start");
    }

    private void EndBattle()
    {
        Debug.Log("Battle end");
        UIManager.instance.EnableUI(); 
        CleanCombat();
        GameManager.instance.ResumeGame();
        UIElements.gameObject.SetActive(false);
    }
}
