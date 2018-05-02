using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerWeaponInterface : PickupItem {
    Shooting shooting;
    public Transform Gun_Pos;
    
    public GameObject PlayerCamera;

    [SerializeField]
    GameObject EquipGun;

    GameObject[] Gun_List;

    void Awake()
    {
        shooting = GetComponent<Shooting>();
        PickUp_Text.enabled = false;

        if (EquipGun)
        {
            Gun_Status gunStatus = EquipGun.GetComponent<Gun_Status>();
            gunStatus.ZoomCamera.targetDisplay = 0;
            shooting.StatusChange(gunStatus);
            Target = gunStatus.ShootPos;
            EquipGun.GetComponent<BoxCollider>().enabled = false;
            EquipGun.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    // Use this for initialization
    void Start () {
        PickUp_Mask = LayerMask.GetMask("Gun");
	}
	
	// Update is called once per frame
	void Update () {

        On_PickUp();

        if (EquipGun) EquipGun.transform.localPosition = Vector3.zero;


        if (isPickUp && collider.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Get_Gun(collider[0].gameObject);
            }
        }
    }

	void Get_Gun(GameObject New_Gun)
	{
        Gun_Status GS = New_Gun.GetComponent<Gun_Status>();
        Destroy(EquipGun.gameObject);

        GS.ZoomCamera.enabled = false;
        GS.ZoomCamera.targetDisplay = 0;

        Target = GS.ShootPos;
        New_Gun.GetComponent<BoxCollider>().enabled = false;
        New_Gun.GetComponent<Rigidbody>().useGravity = false;

        Gun_Ready(New_Gun);
        Gun_Change(New_Gun);
	}

	void Gun_Ready(GameObject New_Gun)
	{
        Gun_Status GS = New_Gun.GetComponent<Gun_Status>();

        New_Gun.transform.parent = Gun_Pos;
        New_Gun.transform.position = Gun_Pos.position;
        New_Gun.transform.localEulerAngles = GS.StandardRotation;
    }

    private void OnDrawGizmos()
    {
        if (!Target) return;

        Gizmos.color = GizmosColor;
        
        Gizmos.DrawSphere(Target.transform.position, Size);
    }

    

    public void Gun_Change(GameObject gun)
    {
        EquipGun = gun;
        shooting.StatusChange(gun.GetComponent<Gun_Status>());
    }
}
