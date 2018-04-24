using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerWeaponInterface : MonoBehaviour {
    Shooting shooting;
    public Transform Gun_Pos;

	Ray Action_Ray;
	RaycastHit Action_Hit;
    LayerMask Item_Mask;
    bool isPickUp;

    float Range = 2.0f;

    public Text PickUp_Text;
    public GameObject PlayerCamera;
    public Transform ShootPos;

    [Header("PickUp Check")]
    public Color GizmosColor;
    public LayerMask PickUp_Mask;
    public float Offset = 2f;
    public float Size = 1f;

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
            shooting.StatusChange(gunStatus);
        }
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

        Collider[] collider = Physics.OverlapSphere(ShootPos.transform.position, Size, PickUp_Mask);
        isPickUp = On_PickUpCheck(collider);

        PickUp_Text.enabled = isPickUp;
        
        if(isPickUp && collider.Length > 0)
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

        ShootPos = GS.ShootPos;
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
        Gizmos.color = GizmosColor;
        
        Gizmos.DrawSphere(ShootPos.transform.position, Size);
    }

    bool On_PickUpCheck(Collider[] collider)
    {
        return collider.Length > 0 ? true : false;
    }

    public void Gun_Change(GameObject gun)
    {
        EquipGun = gun;
        shooting.StatusChange(gun.GetComponent<Gun_Status>());
    }
}
