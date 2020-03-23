using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;
    public HUDRef HUD;

    public bool endgameflag;


    private void Awake()
    {
        if (singleton != this && singleton != null)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        endgameflag = false;
    }

    private void Update()
    {
        if(endgameflag)
        {

        }
    }
}
