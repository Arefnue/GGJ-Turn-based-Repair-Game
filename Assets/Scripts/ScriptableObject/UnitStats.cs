using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    public int speed;
    public int damage;
    public int health;


    public Sprite attackSprite;
    public Sprite blockSprite;
    public Sprite normalSprite;

    public GameObject hitFX;
    public GameObject defFX;
}
