using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Unit,IPointerUpHandler,IPointerDownHandler
{
    public enum State 
    {
        Idle,
    }
    private State state;

    protected override void Start()
    {

        BattleHandler.instance.AddPlayerToList(this);

        base.Start();
        SetStats();
        
    }

    private void Update()
    {
        if (BattleHandler.instance.enemyTurn == true || BattleHandler.instance.unitIsMoving == true) return;

        switch (state)
        {
            case State.Idle:
                Debug.Log("BeklemedePlayer");
                if (Input.GetKeyDown(KeyCode.P)) 
                {
                    EndTurn();

                }
                
                break;
           

        }
    }

    public void MoveHandler()
    {
        
        //EndTurn();
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
