using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Shooting {

    Quaternion Gun_Accuracy()
    {
        Quaternion Acc_Angle = ShootPos.rotation;

        Acc_Angle.eulerAngles += new Vector3((Random.Range(-10.0f + (Accuracy / 10.0f), 10.0f - (Accuracy / 10.0f))) - 1f, Random.Range(-10.0f + (Accuracy / 10.0f), 10.0f - (Accuracy / 10.0f)), ShootPos.rotation.z);
        Debug.Log(Random.Range(-10.0f + (Accuracy / 10.0f), 10.0f - (Accuracy / 10.0f)));
        return Acc_Angle;
    }

    Vector3 Accuracy_Vec3()
    {
        Vector3 Acc_Angle = ShootPos.forward;

        Acc_Angle += new Vector3((Random.Range(-10.0f + (Accuracy / 10.0f), 10.0f - (Accuracy / 10.0f))), Random.Range(-10.0f + (Accuracy / 10.0f), 10.0f - (Accuracy / 10.0f)), ShootPos.rotation.z);
        return Acc_Angle;
    }
}
