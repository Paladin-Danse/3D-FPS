using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Shooting : MonoBehaviour {
	[SerializeField]
	Camera PlayerCamera;
	[SerializeField]
	Camera ZoomCamera;
    
	[SerializeField]
	Transform ShootPos;
	[SerializeField]
	float Rate_Of_Fire;
	[SerializeField]
	float Recoil_value;
	[SerializeField]
	float Accuracy;
	float Aim_Acc = 100.0f;
	float None_Aim_Acc;
	[SerializeField]
	float ReloadTime;

	[SerializeField]
	int Ammo;
	[SerializeField]
    int Magazine;

	[SerializeField]
	Vector3 Recoil_Angles;
	[SerializeField]
	float Correction_Time;

	bool On_Delay = false;
    bool On_Reload_Delay = false;
    bool On_Correction = false;
	bool Zoom;

    Gun_Status.AutoType _autoType;

    [SerializeField]
    GameObject Bullet;

    //UI
    public Text AmmoText;
	public Slider Reload_Gauge;

	Vector3 v;

    Ray shootRay;
    RaycastHit shootHit;

    // Use this for initialization
    void Awake () {
		PlayerCamera.enabled = true;
		ZoomCamera.enabled = false;
	}

	void Start()
	{
		Ammo = Magazine;
		None_Aim_Acc = Accuracy;
		AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
		v = PlayerCamera.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Mouse1();
		Mouse2();
        On_Reload();

		if(On_Correction)
		{
			correction();
		}
	}

	IEnumerator On_Fire()
	{
		AmmoCount();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        Instantiate(Bullet, ShootPos.position, Gun_Accuracy());

		Shoot_Recoil();
		On_Delay = true;
		On_Correction = true;

		yield return new WaitForSeconds(Rate_Of_Fire);

		On_Delay = false;
		On_Correction = false;
	}
	
	void Mouse1()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(On_Delay || Ammo <= 0 || On_Reload_Delay || !ShootPos) return;

			StartCoroutine("On_Fire");
		}
		else if (Input.GetMouseButton(0))
		{
			Mouse2();
			if(On_Delay || Ammo <= 0 || On_Reload_Delay || !ShootPos || _autoType == Gun_Status.AutoType.SemiAuto) return;

			StartCoroutine("On_Fire");
		}
		if(Input.GetMouseButtonUp(0))
		{

		}
	}
	void Mouse2()
	{
		if(Input.GetMouseButtonDown(1))
		{
			if(!ZoomCamera) return;

			Zoom_In();
		}
		else if(Input.GetMouseButton(1))
		{

		}
		if(Input.GetMouseButtonUp(1))
		{
			if(!ZoomCamera) return;

			Zoom_Out();
		}
	}

	void Shoot_Recoil()
	{
		v = PlayerCamera.transform.eulerAngles;

		PlayerCamera.transform.eulerAngles += Vector3.left * Recoil_value;
	}

	void AmmoCount()
	{
		if(Ammo <= 0) return;
		Ammo--;
		AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
	}

	void Zoom_In()
	{
		PlayerCamera.enabled = false;
		ZoomCamera.enabled = true;
		Accuracy = Aim_Acc;
	}

	void Zoom_Out()
	{
		ZoomCamera.enabled = false;
		PlayerCamera.enabled = true;
		Accuracy = None_Aim_Acc;
	}
	
	void correction()
	{
		PlayerCamera.transform.eulerAngles = Vector3.MoveTowards(PlayerCamera.transform.eulerAngles, v, Correction_Time * Time.deltaTime);
	}

	public void StatusChange(Gun_Status status)
	{
        _autoType = status.Mode_Type;
		ShootPos = status.ShootPos;
		ZoomCamera = status.ZoomCamera;

		Accuracy = status.Accuracy;
        None_Aim_Acc = Accuracy;
        Rate_Of_Fire = status.Rate_Of_Fire;
        Recoil_value = status.Recoil_value;
        ReloadTime = status.ReloadTime;
        if (On_Reload_Delay)
        {
            StopCoroutine("Weapon_Reload");
            On_Reload_Delay = false;
            Reload_Gauge.value = 1;
        }
        Magazine = status.Magazine;
		Ammo = Magazine;

        AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
    }
}