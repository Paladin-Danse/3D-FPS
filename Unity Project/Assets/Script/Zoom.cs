﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Shooting {
    bool Zoom;
    bool On_Zoom_Button;

    void Mouse2()
    {
        if (Input.GetMouseButtonDown(1) || On_Zoom_Button)
        {
            if (!ZoomCamera) return;

            Zoom_In();
        }
        else if (Input.GetMouseButton(1))
        {

        }
        if (Input.GetMouseButtonUp(1) || !On_Zoom_Button)
        {
            if (!ZoomCamera) return;

            Zoom_Out();
        }
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

    void Zoom_Key()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            On_Zoom_Button = !On_Zoom_Button;
        }
    }

}