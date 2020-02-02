using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public Healthbar hpBar;

    public Vector3 initalPos;

    public Unit target;
    [SerializeField]private int moveTimeSpeed = 5;
    public bool isEnemy;
    public float remainingDistance = 1f;
    public bool isFaceRight;
    

    public UnitStats profile;
    

    #region Stats
    [HideInInspector] public int speed;
     public int health;
    [HideInInspector] public int damage;

    #endregion

    protected virtual void Start()
    {
        
    }

    protected virtual void SetStats()
    {
        DetermineInitiative();
        health = profile.health;
        damage = profile.damage;
    }

    public void DetermineInitiative()
    {
        int d6 = UnityEngine.Random.Range(1, 6);

        speed = d6 + profile.speed;
       
    }

    public void PlayNow(Unit unit) 
    {
        PlayAction(unit);
        Debug.Log(GetType());
    }

   

    protected virtual void PlayAction(Unit unit) 
    {
        
        if(unit.isEnemy != true) 
        {
            BattleHandler.instance.playerTurn = true;
            
        }
        else 
        {
            BattleHandler.instance.playerTurn = false;
        }
        
    }

    public IEnumerator BattleScreen(Unit attacker,Unit blocker) 
    {
        if (attacker == null || blocker == null) yield return null;
        Unit rightUnit;
        Unit leftUnit;
        BattleHandler.instance.unitIsMoving = true;
        if(attacker.transform.position.x <= blocker.transform.position.x) 
        {
            rightUnit = blocker;
            leftUnit = attacker;

            SpriteRenderer leftRend = leftUnit.GetComponent<SpriteRenderer>();
            SpriteRenderer rightRend = rightUnit.GetComponent<SpriteRenderer>();



            leftRend.sortingLayerName = "Battle";
            rightRend.sortingLayerName = "Battle";

            leftUnit.transform.position = BattleHandler.instance.leftPos.transform.position;
            rightUnit.transform.position = BattleHandler.instance.rightPos.transform.position;

            leftUnit.transform.localScale += Vector3.one * 2f;
            rightUnit.transform.localScale += Vector3.one * 2f;

            leftRend.sprite = leftUnit.profile.attackSprite;
            rightRend.sprite = rightUnit.profile.blockSprite;


            if (leftUnit.isFaceRight != true)
            {
                leftRend.flipX = true;
            }
            if (rightUnit.isFaceRight == true)
            {
                rightRend.flipX = true;
            }

            Attack(attacker, blocker);
            yield return new WaitForSeconds(1.5f);

            leftRend.sprite = leftUnit.profile.normalSprite;
            rightRend.sprite = rightUnit.profile.normalSprite;

            if (leftUnit.isFaceRight != true)
            {
                leftRend.flipX = false;
            }
            if (rightUnit.isFaceRight == true)
            {
                rightRend.flipX = false;
            }


            leftUnit.transform.localScale -= Vector3.one * 2f;
            rightUnit.transform.localScale -= Vector3.one * 2f;


            leftUnit.transform.position = leftUnit.initalPos;
            rightUnit.transform.position = rightUnit.initalPos;


            leftRend.sortingLayerName = "Unit";
            rightRend.sortingLayerName = "Unit";


            yield return new WaitForSeconds(1f);
            CheckBlockerState(blocker);
            yield return new WaitForSeconds(1f);

            BattleHandler.instance.unitIsMoving = false;

            GoNext();
        }
        else 
        {
            rightUnit = attacker;
            leftUnit = blocker;

            SpriteRenderer leftRend = leftUnit.GetComponent<SpriteRenderer>();
            SpriteRenderer rightRend = rightUnit.GetComponent<SpriteRenderer>();



            leftRend.sortingLayerName = "Battle";
            rightRend.sortingLayerName = "Battle";

            leftUnit.transform.position = BattleHandler.instance.leftPos.transform.position;
            rightUnit.transform.position = BattleHandler.instance.rightPos.transform.position;

            leftUnit.transform.localScale += Vector3.one * 2f;
            rightUnit.transform.localScale += Vector3.one * 2f;

            leftRend.sprite = leftUnit.profile.blockSprite;
            rightRend.sprite = rightUnit.profile.attackSprite;

            if (leftUnit.isFaceRight != true)
            {
                leftRend.flipX = true;
            }
            if (rightUnit.isFaceRight == true)
            {
                rightRend.flipX = true;
            }

            Attack(attacker, blocker);
            yield return new WaitForSeconds(1.5f);

            leftRend.sprite = leftUnit.profile.normalSprite;
            rightRend.sprite = rightUnit.profile.normalSprite;

            if (leftUnit.isFaceRight != true)
            {
                leftRend.flipX = false;
            }
            if (rightUnit.isFaceRight == true)
            {
                rightRend.flipX = false;
            }


            leftUnit.transform.localScale -= Vector3.one * 2f;
            rightUnit.transform.localScale -= Vector3.one * 2f;


            leftUnit.transform.position = leftUnit.initalPos;
            rightUnit.transform.position = rightUnit.initalPos;


            leftRend.sortingLayerName = "Unit";
            rightRend.sortingLayerName = "Unit";


            yield return new WaitForSeconds(1f);
            CheckBlockerState(blocker);
            yield return new WaitForSeconds(1f);

            BattleHandler.instance.unitIsMoving = false;

            GoNext();
        }

        
    }

    public void GoNext() 
    {
        if(0 < BattleHandler.instance.combatCharacters.Count) 
        {
            BattleHandler.instance.PlayCharacter();
        }
        else 
        {
            BattleHandler.instance.StartBattle();
        }
    }

    public void CheckBlockerState(Unit blocker) 
    {
        if(blocker.health<= 0) 
        {
            BattleHandler.instance.deathList.Add(blocker);
            blocker.gameObject.SetActive(false);
        }
        if(blocker.isEnemy == true) 
        {
            if(blocker.health >= 100) 
            {
                BattleHandler.instance.deathList.Add(blocker);
                blocker.gameObject.SetActive(false);
                RepairRobots();
                
            }
        }


    }

    public void RepairRobots() 
    {
        Debug.Log("Robot is repaired");
        GoNext();
        
    }


    void Attack(Unit attacker,Unit blocker) 
    {
        blocker.health -= attacker.damage;

        Instantiate(blocker.profile.defFX, blocker.transform.position, Quaternion.identity);
        Instantiate(attacker.profile.hitFX, attacker.transform.position, Quaternion.identity);

        
        
    }
    
    

}
