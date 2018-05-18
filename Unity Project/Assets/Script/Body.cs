using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Target {
    HPManager HPM;

    void Start()
    {
        HPM = transform.parent.GetComponent<HPManager>();
    }

    public override void FixedUpdate()
    {

    }

    public override void HP_Lost(float Damage)
    {
        HPM.HP_Lost(Damage);
    }

    protected override void Die()
    {

    }
}
