using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MobileInputManager : MonoSingleton<MobileInputManager>
{
    public Joystick joystick;
    private void Awake()
    {
        joystick.gameObject.SetActive(false);
    }
    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            joystick.gameObject.SetActive(true);
        }
     
    }
}
