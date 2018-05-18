using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour {
    [SerializeField]
    private float HP;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public void HP_Lost(float Damage)
    {
        HP -= Damage;
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
