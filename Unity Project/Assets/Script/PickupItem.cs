using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour {
    [Header("PickUp Check")]
    public Color GizmosColor;
    public LayerMask PickUp_Mask;
    public float Offset = 2f;
    public float Size = 1f;

    protected bool isPickUp
    {
        get;
        private set;
    }
    public Text PickUp_Text;
    
    protected virtual Transform Target
    {
        get;
        set;
    }

    protected Collider[] collider
    {
        get;
        private set;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    protected bool On_PickUpCheck(Collider[] collider)
    {
        return collider.Length > 0 ? true : false;
    }

    protected void On_PickUp()
    {
        collider = Physics.OverlapSphere(Target.transform.position, Size, PickUp_Mask);

        isPickUp = On_PickUpCheck(collider);

        PickUp_Text.enabled = isPickUp;
    }

}
