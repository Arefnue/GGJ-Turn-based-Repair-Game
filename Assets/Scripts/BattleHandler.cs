using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public static BattleHandler instance = null;

    public bool playerTurn;
    public bool enemyTurn;

    [HideInInspector]public List<PlayerController> players;
    [HideInInspector] public List<EnemyController> enemies;
    [HideInInspector] public int enemyCount = 3;


    [HideInInspector] public List<Unit> combatCharacters;

    [HideInInspector] public PlayerController thisPlayerPlay;

    [HideInInspector] public bool unitIsMoving;

    [Header("Prefabs")]
    public GameObject defenderHacker;
    public GameObject healerHacker;

    public List<GameObject> enemyPrefabs;
    [Space]

    [Header("PrefabPositions")]
    public List<GameObject> prefabPositions;
    public GameObject attackTrigger;


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

    public void PlaceCharacters() 
    {
        Instantiate(defenderHacker, prefabPositions[0].transform.position, Quaternion.identity);
        Instantiate(healerHacker, prefabPositions[1].transform.position, Quaternion.identity);

        for(int i = 0; i < enemyCount; i++) 
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count - 1);
            Instantiate(enemyPrefabs[randomIndex], prefabPositions[i + 2].transform.position,Quaternion.identity);
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            for(int i = 0; i < enemies.Count; i++) 
            {
                enemies[i].MoveTo(enemies[i].targetPos);
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            StartBattle();
          
        }

    }

    public void SortTurnSpeed() 
    {
        foreach(Unit unit in FindObjectsOfType<Unit>()) 
        {
            combatCharacters.Add(unit);
        }
        combatCharacters.Sort((a, b) => a.speed.CompareTo(b.speed));
        combatCharacters.Reverse();
    }

    public void StartBattle() 
    {
        SortTurnSpeed();
        SortPlayers();
        SortEnemies();
        PlayFastThenNext();

    }

    public void PlayFastThenNext() 
    {
        
        Unit currentUnit = combatCharacters[0];

        if(currentUnit.isEnemy != true) 
        {
            int i = 0;
            playerTurn = true;
            enemyTurn = false;


            while (!combatCharacters.Contains(players[i]))
            {
                i++;
                
                if (i >= players.Count) 
                    break;
            }
            thisPlayerPlay = players[i];
            combatCharacters.Remove(currentUnit);
            Debug.Log("Player:"+players[i].speed);
            players[i].MoveHandler();
            
        }
        else
        {
            int i = 0;
            enemyTurn = true;
            playerTurn = false;

            while (!combatCharacters.Contains(enemies[i]))
            {
                i++;
                
                
                if (i >= enemies.Count)
                    break;
            }
            combatCharacters.Remove(currentUnit);
            Debug.Log("Enemies:" + enemies[i].speed);
            enemies[i].MoveHandler();
            
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
