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

    private Enemy sourceEnemy;

    public CombatCreature selectedEnemy { get; private set; }

    [SerializeField]
    private UICombat UIElements;

    [SerializeField]
    private GameObject UIEnemyPrefab;
    [SerializeField]
    private GameObject UICharacterPrefab;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void InitiateCombat(int EnemyCount = 1/*List<CombatEnemyType> combatEnemyTypes*/)
    {
        enemies = new List<CombatCreature>();

        for (int i = 0; i < EnemyCount; i++)
        {
            CombatCreature cc = new CombatCreature();
            GameObject go = Instantiate(UIEnemyPrefab, UIElements.UIEnemyPositions.First(g => g.childCount == 0));
            cc.UIElements = go.GetComponent<UICombatEnemy>();
            cc.UIElements.combatCreature = cc;
            cc.UIElements.SetHealth(cc.HP);
            enemies.Add(cc);
        }

        ChangeSelectedEnemy();

        playerCharacters = new List<CombatCharacter>();

        for (int i = 0; i < 1; i++)
        {
            CombatCharacter cc = new CombatCharacter();
            GameObject go = Instantiate(UICharacterPrefab, UIElements.UICharacterGrid);
            cc.UIElements = go.GetComponent<UICombatCharacter>();
            playerCharacters.Add(cc);
        }

        UpdatePlayerCharacterStats();

        UIElements.UISelectedCharacter.OpenSidePanel();
    }

    public void CleanCombat()
    {
        UIElements.UISelectedCharacter.CloseSidePanel();

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
            if (selectedEnemy == null || selectedEnemy.UIElements == null)
            {
                ChangeSelectedEnemy();
            }
            CombatCreature cc = selectedEnemy; 
            if (cc != null)
            {
                cc.Damage(50);
                cc.UIElements.SetHealth(cc.HP);
            }
            else
            {
                Debug.LogError("Combat Enemy not found");
            }
        }
        EndTurn();
    }

    public void DestroyEnemy(CombatCreature combatCreature)
    {
        enemies.Remove(combatCreature);
        Destroy(combatCreature.UIElements.gameObject);
        ChangeSelectedEnemy();
    }

    /// <summary>
    /// Will select <c>selectedCreature</c> as target enemy
    /// </summary>
    public void ChangeSelectedEnemy(CombatCreature selectedCreature)
    {
        if (selectedCreature != null && selectedCreature.UIElements != null)
        {
            if (selectedEnemy != null && selectedEnemy.UIElements != null)
            {
                selectedEnemy.UIElements.isSelected = false;
            }
            selectedEnemy = selectedCreature;
            selectedEnemy.UIElements.isSelected = true;
        }
    }

    /// <summary>
    /// Will select new first enemy as target
    /// </summary>
    public void ChangeSelectedEnemy()
    {
        ChangeSelectedEnemy(enemies.FirstOrDefault());
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

    public void StartBattle(int EnemyCount = 1, Enemy SourceEnemy = null/*List<CombatEnemyType> combatEnemyTypes*/)
    {
        sourceEnemy = SourceEnemy;
        UIManager.instance.DisableUI();
        GameManager.instance.PauseGame();
        UIElements.gameObject.SetActive(true);
        InitiateCombat(EnemyCount);
        Debug.Log("Battle start");
    }

    private void EndBattle() //Do in Coroutine for animated end
    {
        Debug.Log("Battle end");
        UIManager.instance.EnableUI(); 
        CleanCombat();
        GameManager.instance.ResumeGame();
        UIElements.gameObject.SetActive(false);
        if (sourceEnemy  != null)
        {
            sourceEnemy.CombatFinished();
        }
    }
}
