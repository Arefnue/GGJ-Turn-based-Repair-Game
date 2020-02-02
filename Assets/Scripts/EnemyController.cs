using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EnemyController : Unit,IPointerDownHandler,IPointerUpHandler
{
    
    public float moveTime = 0.3f;

    public List<Unit> targetList;

    public bool enemyIsBusy;

    protected override void Start()
    {
        
        BattleHandler.instance.AddEnemyToList(this);

        base.Start();
        SetStats();
    }


    public void ChooseRandomTarget() 
    {
        int randomInt = UnityEngine.Random.Range(0, targetList.Count - 1);

        target = targetList[randomInt];
    }

    public void DetermineTargets() 
    {
        foreach(Unit target in FindObjectsOfType<Unit>()) 
        {
            if (target.gameObject == null) continue;
            targetList.Add(target);
            
        }
        targetList.Remove(this);

    }

    protected override void PlayAction(Unit unit)
    {
        base.PlayAction(unit);

        if(BattleHandler.instance.playerTurn != true) 
        {

            DetermineTargets();
            ChooseRandomTarget();
            StartCoroutine(BattleScreen(unit, target));
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale += Vector3.one * 0.1f;

        if (BattleHandler.instance.playerTurn == true && BattleHandler.instance.unitIsMoving != true) 
        {
            BattleHandler.instance.thisPlayerPlay.attackCursor.SetActive(false);
            StartCoroutine(BattleScreen(BattleHandler.instance.thisPlayerPlay, this));
        }

        


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale -= Vector3.one * 0.1f;
    }
}
