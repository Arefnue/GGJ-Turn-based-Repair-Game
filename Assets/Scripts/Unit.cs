using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 initalPos;
    public Vector3 targetPos;
    [SerializeField]private int moveTimeSpeed = 5;
    public bool isEnemy;

    public UnitStats profile;
    

    #region Stats
    [HideInInspector] public int speed;

    #endregion

    protected virtual void Start()
    {
        initalPos = transform.position;
    }

    protected virtual void SetStats()
    {
        DetermineInitiative();
    }

    public void DetermineInitiative()
    {
        int d6 = Random.Range(1, 6);

        speed = d6 + profile.speed;
       
    }

    protected virtual void EndTurn()
    {
        if (BattleHandler.instance.combatCharacters.Count > 0)
        {
            BattleHandler.instance.PlayFastThenNext();
        }
        else
        {
            NextTurn();
        }
    }

    public void NextTurn() 
    {
        
    }

    public void MoveTo(Vector3 target) 
    {
        StartCoroutine(SmoothMovement(target));
    }

    IEnumerator SmoothMovement(Vector3 end)
    {
        //While moving dont move again
        BattleHandler.instance.unitIsMoving = true;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, moveTimeSpeed * Time.deltaTime);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        //While moving dont move again
        BattleHandler.instance.unitIsMoving = false;

    }

}
