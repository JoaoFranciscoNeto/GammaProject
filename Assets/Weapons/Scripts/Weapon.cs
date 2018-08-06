using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float RateOfFire = 1;
    float CooldownTime;
    float RemainingCooldownTime;

    bool CanFire = true;

    protected Transform Muzzle;

	// Use this for initialization
	void Start () {
        CooldownTime = 1.0f / RateOfFire;
        RemainingCooldownTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!CanFire)
        {
            RemainingCooldownTime -= Time.deltaTime;
            if (RemainingCooldownTime <= 0)
            {
                CanFire = true;
            }
        }
	}

    public virtual bool Fire()
    {
        if (CanFire)
        {
            RemainingCooldownTime = CooldownTime;
            CanFire = false;
            Debug.Log("Cooldown Set");
            return true;
        }
        else
            return false;
    }
}
