using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Unit,IPointerUpHandler,IPointerDownHandler
{
    
    public GameObject attackCursor;


    protected override void Start()
    {

        BattleHandler.instance.AddPlayerToList(this);

        base.Start();
        SetStats();
        
    }

    

    protected override void PlayAction(Unit unit)
    {
        base.PlayAction(unit);

        if (BattleHandler.instance.playerTurn == true)
        {
            
            attackCursor.SetActive(true);
            BattleHandler.instance.thisPlayerPlay = this;
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (BattleHandler.instance.playerTurn == true && BattleHandler.instance.unitIsMoving != true && BattleHandler.instance.thisPlayerPlay != this)
        {
            StartCoroutine(BattleScreen(BattleHandler.instance.thisPlayerPlay, this));
            BattleHandler.instance.thisPlayerPlay.attackCursor.SetActive(false);
            
        }
        transform.localScale += Vector3.one * 0.1f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale -= Vector3.one * 0.1f;
    }
}
