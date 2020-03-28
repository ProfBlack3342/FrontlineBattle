using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;

    public PlayerStatus player;

    public bool endgameflag, gamestart;

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
        gamestart = false;
    }

    private void Update()
    {
        Debug.Log("GameManager Update");
        if (gamestart)
        {
            Debug.Log("Gamemanager Game loop");
            if (endgameflag)
            {
                Debug.Log("GameOver");

                if(player.HP <= 0)
                {
                    Debug.Log("Perdeu o jogo");
                }
                else
                {
                    Debug.Log("Ganhou o jogo");
                }

                player.movement.enabled = false;
                player.enabled = false;
            }
        }
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("StartLocalPlayer");
        player.movement.enabled = true;
        player.enabled = true;
    }

    public override void OnNetworkDestroy()
    {
        Debug.Log("OnNetworkDestroy");
        endgameflag = false;
        gamestart = false;
        player.movement.enabled = false;
        player.enabled = false;
    }
}
