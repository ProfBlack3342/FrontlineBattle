using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;
    public GameObject manager;

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

                //Determinar quem ganhou e encerrar o jogo
            }
        }
    }
}
