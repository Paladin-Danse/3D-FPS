using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField]
	float Speed;
	[SerializeField]
	GameObject Damaged_Wall;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Target>())
		{
			collider.gameObject.GetComponent<Target>().HP_Lost();
			GameObject Damaged_Object = Instantiate(Damaged_Wall, gameObject.transform);
			Damaged_Object.transform.parent = collider.transform;
		}
		Destroy(gameObject);
	}
}
