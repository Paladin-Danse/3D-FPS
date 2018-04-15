using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	[SerializeField]
	float Speed;
	[SerializeField]
	float MouseSensitivity;
	[SerializeField]
	float JumpPower;

	[SerializeField]
	Camera PlayerCamera;


	/*
	public float Speed
	{
		get
		{
			
			return speed*10;
		}
		set
		{
			if(speed != value)
			{
				
			}

			speed = value;

		}
	}
*/
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed);
		}

		if(Input.GetAxis("Mouse X") != 0)
		{
			transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, transform.rotation.z);
		}

		if(Input.GetAxis("Mouse Y") != 0)
		{
			PlayerCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * MouseSensitivity, transform.rotation.x, transform.rotation.z);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower);
		}
	}
}
