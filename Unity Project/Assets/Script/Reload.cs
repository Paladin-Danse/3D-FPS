using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Shooting {
    float NowTime;

    private void On_Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (On_Reload_Delay || Ammo == Magazine)
            {
                Debug.Log("On_Reload_Delay is True!");
                return;
            }
            StartCoroutine("Weapon_Reload");
        }

        if (On_Reload_Delay)
        {
            NowTime += Time.deltaTime;
            Reload_Gauge.value = NowTime / ReloadTime;
        }
        else
        {
            Reload_Gauge.value = 1;
            NowTime = 0;
        }
    }

    IEnumerator Weapon_Reload()
    {
        On_Reload_Delay = true;
        NowTime = 0;

        Debug.Log(On_Reload_Delay);

        yield return new WaitForSeconds(ReloadTime);

        Ammo = Magazine;
        AmmoText.text = Ammo.ToString() + " / " + Magazine.ToString();
        On_Reload_Delay = false;
        Debug.Log(On_Reload_Delay);
    }
}
