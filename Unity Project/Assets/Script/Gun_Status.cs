using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Status : MonoBehaviour {
    public Vector3 StandardRotation;
	public Transform ShootPos;
	public Camera ZoomCamera;

	public float Accuracy;
	public float Rate_Of_Fire;
	public float Recoil_value;
	public float ReloadTime;
	public int Magazine;
    public float Gun_Power;

    public enum AutoType
    {
        Auto,
        SemiAuto
    }
    public AutoType Mode_Type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
