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
	
	float Rate_Of_Fire;
	float Recoil_value;
	float Accuracy;
	float Aim_Acc = 100.0f;
	float None_Aim_Acc;
	float ReloadTime;
    float Range = 600.0f;

	int Ammo;
	int Magazine;
    
    public GameObject Damaged_Wall;
	[SerializeField]
	Vector3 Recoil_Angles;
	[SerializeField]
	float Correction_Time;

	bool On_Delay = false;
    bool On_Reload_Delay = false;
    bool On_Correction = false;
	
    
    Gun_Status.AutoType _autoType;

    [SerializeField]
    GameObject Bullet;

    //UI
    public Text AmmoText;
	public Slider Reload_Gauge;

    [SerializeField]
	Vector3 shootPrevCamVec;

    Ray shootRay;
    RaycastHit shootHit;
    public LayerMask Target;
    LineRenderer gunLine;

    public float timeBetweenBullets = 0.15f;
    float timer;
    float effectsDisplayTime = 0.2f;
    // Use this for initialization
    void Awake () {
		PlayerCamera.enabled = true;
		ZoomCamera.enabled = false;
        gunLine = GetComponent<LineRenderer>();
    }

	void Start()
	{
		Ammo = Magazine;
		None_Aim_Acc = Accuracy;
		AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
		shootPrevCamVec = PlayerCamera.transform.eulerAngles;
	}

    void Update()
    {
        Zoom_Key();
        timer += Time.deltaTime;

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        Mouse1();
		Mouse2();
        On_Reload();

        correction();
	}

	IEnumerator On_Fire()
	{
		AmmoCount();
        timer = 0;
        
        shootRay.origin = ShootPos.position;
        shootRay.direction = Accuracy_Vec3();

        gunLine.enabled = true;
        gunLine.SetPosition(0, ShootPos.position);

        

        if(Physics.Raycast(shootRay, out shootHit, Target))
        {
            gunLine.SetPosition(1, shootHit.point);

            if (shootHit.collider.GetComponent<Target>())
            {
                shootHit.collider.GetComponent<Target>().HP_Lost();
                GameObject Damaged_Object = Instantiate(Damaged_Wall, shootHit.point, shootHit.transform.rotation);
                Damaged_Object.transform.parent = shootHit.collider.transform;
            }
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * Range);
        }
        
        //Instantiate(Bullet, ShootPos.position, Gun_Accuracy());
        
		Shoot_Recoil();
		On_Delay = true;

		yield return new WaitForSeconds(Rate_Of_Fire);

		On_Delay = false;
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
	

	void Shoot_Recoil()
	{
		shootPrevCamVec = PlayerCamera.transform.eulerAngles;

		PlayerCamera.transform.eulerAngles += Vector3.left * Recoil_value;
	}

	void AmmoCount()
	{
		if(Ammo <= 0) return;
		Ammo--;
		AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
	}

	void correction()
	{
		PlayerCamera.transform.eulerAngles = Vector3.MoveTowards(PlayerCamera.transform.eulerAngles, shootPrevCamVec, Correction_Time * Time.deltaTime);
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
    

    void DisableEffects()
    {
        gunLine.enabled = false;
    }

    public void SetCamVec(Vector3 angle)
    {
        shootPrevCamVec = angle;
    }
}