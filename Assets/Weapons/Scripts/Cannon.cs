using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon {


    public override bool Fire()
    {
        if (!base.Fire())
            return false;

        Debug.Log("Cannon Fired");

        return true;
    }
}
