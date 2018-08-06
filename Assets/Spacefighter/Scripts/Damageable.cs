using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Damageable : MonoBehaviour, IEventSystemHandler {

    public int hitPoints;
    int currentHitPoints;


	// Use this for initialization
	void Start () {
        currentHitPoints = hitPoints;	
	}
	
    public void ApplyDamage(int damage)
    {
        currentHitPoints -= damage;
        if (currentHitPoints <= 0)
        {
            Debug.Log(gameObject.name + " was destroyed");
        }
    }
}
