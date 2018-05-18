using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Target {
    HPManager HPM;

    // Use this for initialization
    void Start () {
        HPM = transform.parent.GetComponent<HPManager>();
	}

    public override void FixedUpdate()
    {

    }

    public override void HP_Lost(float Damage)
    {
        HPM.HP_Lost(Damage * 100);
    }

    protected override void Die()
    {

    }
}
