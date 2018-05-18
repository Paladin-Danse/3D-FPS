using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	[SerializeField]
	private float HP;

    public virtual void FixedUpdate()
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void HP_Lost(float Damage)
	{
        HP -= Damage;
	}

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
