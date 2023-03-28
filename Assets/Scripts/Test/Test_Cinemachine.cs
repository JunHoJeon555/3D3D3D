using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Test_Cinemachine : Test_Base
{
    public CinemachineVirtualCamera[] vcams;


    private void Start()
    {
        if (vcams == null)
        {
            vcams = FindObjectsOfType<CinemachineVirtualCamera>();
        }
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        ResetPriority();
        vcams[0].Priority = 100;
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        ResetPriority();
        vcams[1].Priority = 100;
    }

    private void ResetPriority()
    {
        foreach (var vcam in vcams)
        {
            vcam.Priority = 10;
        }
    }

}
