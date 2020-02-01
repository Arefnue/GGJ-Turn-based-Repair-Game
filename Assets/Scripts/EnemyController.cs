using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : Unit,IPointerDownHandler,IPointerUpHandler
{
    public float moveTime = 0.3f;

    public List<GameObject> targetList;

    public enum State 
    {
        waitForPlayers,
        move,
        attackPlayer,
        back,
        attackSelf,
        attackEnemy

    }
    private State state;

    protected override void Start()
    {
        
        BattleHandler.instance.AddEnemyToList(this);

        base.Start();
        SetStats();
    }

    private void Update()
    {
        if (BattleHandler.instance.playerTurn == true || BattleHandler.instance.unitIsMoving == true) return;

        switch (state)
        {
            case State.waitForPlayers :
                Debug.Log("Beklemede");
                EndTurn();
                break;
            case State.move:
                MoveTo(BattleHandler.instance.attackTrigger.transform.position);
                if (Vector3.Distance(transform.position, BattleHandler.instance.attackTrigger.transform.position)<= Mathf.Epsilon) 
                {
                    state = State.attackPlayer;
                }

                break;
            case State.attackPlayer:
                Attack();

                break;
            case State.back:
                MoveTo(initalPos);
                if(Vector3.Distance(transform.position,initalPos)<= Mathf.Epsilon) 
                {
                    state = State.waitForPlayers;
                }
                break;
            case State.attackSelf:
                
                break;
            case State.attackEnemy:
                break;

        }
    }

    public void AttackSelf() 
    {
        state = State.waitForPlayers;
    }

    public void AttackEnemy() 
    {
        state = State.waitForPlayers;
    }

    public void Attack() 
    {


        state = State.back;
    }

    public void MoveHandler()
    {
        DetermineTargets();
        ChooseRandomTarget();
        if(Vector3.Distance(transform.position,targetPos) <= Mathf.Epsilon) 
        {
            state = State.attackSelf;
        }
        else if(Vector3.Distance(transform.position,targetPos) < 
            Vector3.Distance(transform.position,BattleHandler.instance.attackTrigger.transform.position)) 
        {
            state = State.attackEnemy;
        }
        else 
        {
            state = State.move;
        }
        
        
        //EndTurn();
    }

    public void ChooseRandomTarget() 
    {
        int randomInt = Random.Range(0, targetList.Count - 1);

        targetPos = targetList[randomInt].transform.position;
    }

    public void DetermineTargets() 
    {
        foreach(PlayerController target in FindObjectsOfType<PlayerController>()) 
        {
            targetList.Add(target.gameObject);
        }

    }

    protected override void EndTurn()
    {
        //targetList.Clear();

        base.EndTurn();
    }


    public void OnPointerDown(PointerEventData eventData)
    {


        transform.localScale += Vector3.one * 0.1f;


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale -= Vector3.one * 0.1f;
    }
}
