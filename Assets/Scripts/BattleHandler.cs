using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public static BattleHandler instance = null;
    
    public bool playerTurn;
    

    [HideInInspector]public List<PlayerController> players;
    [HideInInspector] public List<EnemyController> enemies;
    [HideInInspector] public int enemyCount = 3;
    [HideInInspector] public int playID = 0;
    public List<Unit> deathList = new List<Unit>();

    [HideInInspector] public List<Unit> combatCharacters;

    [HideInInspector] public PlayerController thisPlayerPlay;

    [HideInInspector] public bool unitIsMoving;

    [Header("Prefabs")]
    public PlayerController defenderHacker;
    public PlayerController healerHacker;

    public List<EnemyController> enemyPrefabs;
    [Space]

    [Header("PrefabPositions")]
    public List<GameObject> prefabPositions;
    public GameObject attackTrigger;

    [Header("BattleScreenPositions")]
    public GameObject leftPos;
    public GameObject rightPos;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        players = new List<PlayerController>();
        enemies = new List<EnemyController>();
        combatCharacters = new List<Unit>();

        
    }

    private void Start()
    {
        PlaceCharacters();
    }

    //Combat Yerleri
    public void PlaceCharacters() 
    {
        PlayerController defender=Instantiate(defenderHacker, prefabPositions[0].transform.position, Quaternion.identity);
        defender.initalPos = prefabPositions[0].transform.position;
        PlayerController healer =Instantiate(healerHacker, prefabPositions[1].transform.position, Quaternion.identity);
        healer.initalPos = prefabPositions[1].transform.position;
        for(int i = 0; i < enemyCount; i++) 
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count - 1);
            EnemyController enemyPre=Instantiate(enemyPrefabs[randomIndex], prefabPositions[i + 2].transform.position,Quaternion.identity);
            enemyPre.initalPos = prefabPositions[i+2].transform.position;
        }
    }

    private void Update()
    {
        
       
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            StartBattle();
        }

        Debug.Log(playID);
        

    }

    //Sort all battling chars
    public void SortTurnSpeed() 
    {
        combatCharacters.Clear();
        foreach (Unit unit in FindObjectsOfType<Unit>()) 
        {
            combatCharacters.Add(unit);
        }
        combatCharacters.Sort((a, b) => a.speed.CompareTo(b.speed));
        combatCharacters.Reverse();
    }

    public void StartBattle() 
    {
        Debug.Log("StartBattle");
        foreach(Unit unit in FindObjectsOfType<Unit>()) 
        {
            unit.DetermineInitiative();
            
        }
        playID = 0;
        SortTurnSpeed();

        PlayCharacter();
    }

    public void PlayCharacter() 
    {
        for(int i = 0; i < deathList.Count-1; i++) 
        {
            if (combatCharacters.Contains(deathList[i]))
            {
                combatCharacters.Remove(deathList[i]);
            }
        }
        
        if(combatCharacters.Count > 0) 
        {
            combatCharacters[0].PlayNow(combatCharacters[0]);
            combatCharacters.Remove(combatCharacters[0]);
        }
        else 
        {
            StartBattle();
        }
        
    }


    public void SortPlayers() 
    {
        players.Sort((a, b) => a.speed.CompareTo(b.speed));
        players.Reverse();
    }

    public void SortEnemies() 
    {
        enemies.Sort((a, b) => a.speed.CompareTo(b.speed));
        enemies.Reverse();
    }

    public void AddEnemyToList(EnemyController script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }

    public void AddPlayerToList(PlayerController script)
    {
        //Add Player to List enemies.
        players.Add(script);
    }

    public void AddCharacterToList(Unit script) 
    {
        combatCharacters.Add(script);
    }

}
