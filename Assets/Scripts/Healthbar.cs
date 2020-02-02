using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
	public Transform bar;
	public Unit unit;

	public int maxHealth;

	private void Start()
	{
		maxHealth = unit.profile.health;
	}

	public void HpConfig(float damage) 
	{
		float current = (unit.health / maxHealth);
		SetSize(current);
	}



	public void SetSize(float sizeNormalized) 
	{
		sizeNormalized = sizeNormalized * 0.6f;
		
		bar.localScale = new Vector3(sizeNormalized, 1f);
	}

}
