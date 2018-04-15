using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	[SerializeField]
	int HP;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(HP <= 0)
		{
			Die();
		}
	}

	public void HP_Lost()
	{
		HP -= 50;
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
