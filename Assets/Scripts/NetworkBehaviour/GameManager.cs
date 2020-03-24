using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;

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

                PlayerStatus winnerinfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

                if (isServer)
                {
                    if (winnerinfo.id == 1)
                    {
                        Debug.Log("Sou o server e ganhei o jogo");
                    }
                    else
                    {
                        Debug.Log("Sou o server e perdi o jogo");
                    }
                }
                else if (isClient)
                {
                    if (winnerinfo.id == 2)
                    {
                        Debug.Log("Sou o cliente e ganhei o jogo");
                    }
                    else
                    {
                        Debug.Log("Sou o cliente e perdi o jogo");
                    }
                }
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
                    endgameflag = true;
                else if (GameObject.FindGameObjectsWithTag("Player") == null)
                    Debug.Log("N achou Players");
            }
        }
    }
}
