﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HUDRef : MonoBehaviour
{
    public Button HostButton, JoinButton, BackButton, ConnectButton;
    public InputField input;
    public string IP;

    public bool Host, Join, Back, Connect;

    public void HostGame()
    {
        Host = true;
    }

    public void JoinGame()
    {
        Join = true;
    }

    public void BackMenu()
    {
        Back = true;
    }

    public void ConnectGame()
    {
        Connect = true;
    }

    public void SetIP()
    {
        IP = input.text;
    }
}
