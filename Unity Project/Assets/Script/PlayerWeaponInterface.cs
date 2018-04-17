﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerWeaponInterface : MonoBehaviour {
	public Transform Gun_Pos;

	Ray Action_Ray;
	RaycastHit Action_Hit;
    LayerMask Item_Mask;
    bool isPickUp;

    float Range = 2.0f;

    public Text PickUp_Text;

    [Header("PickUp Check")]
    public Color GizmosColor;
    public LayerMask PickUp_Mask;
    public float Offset = 2f;
    public float Size = 1f;

    void Awake()
    {
        PickUp_Text.enabled = false;
    }

    // Use this for initialization
    void Start () {
        PickUp_Mask = LayerMask.GetMask("Gun");
	}
	
	// Update is called once per frame
	void Update () {
        Action_Ray.origin = Gun_Pos.transform.position;
        Action_Ray.direction = Gun_Pos.transform.forward;
        Debug.DrawRay(Action_Ray.origin, Action_Ray.direction, Color.yellow);

        isPickUp = On_PickUpCheck();
        PickUp_Text.enabled = isPickUp;

		if(Input.GetKeyDown(KeyCode.F))
		{
			
		}
	}

	void Get_Gun(GameObject Gun)
	{
		Gun_Status status = Gun.GetComponent<Gun_Status>();

		GetComponent<Shooting>().StatusChange(status);
		Gun_Ready(Gun);
	}

	void Gun_Ready(GameObject Gun)
	{
		Gun.transform.parent = Gun_Pos;
		Gun.transform.position = new Vector3(0, 0, 0);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;

        float PosX = transform.position.x * (Mathf.Cos(6 * transform.rotation.y * Mathf.PI / 180) * Offset);
        float PosY = (transform.position.y);
        float PosZ = transform.position.z * (Mathf.Sin(6 * transform.rotation.y * Mathf.PI / 180) * Offset);

        Vector3 CubePos = new Vector3(PosX, PosY, PosZ);

        Gizmos.DrawCube(CubePos, Vector3.one * Size);

        Debug.Log(CubePos);
    }

    bool On_PickUpCheck()
    {
        Collider[] collider = Physics.OverlapBox(transform.position, (Vector3.one * Size) / 2, transform.rotation, PickUp_Mask);

        return collider.Length > 0 ? true : false;
    }
}
