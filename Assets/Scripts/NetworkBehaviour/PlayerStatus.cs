﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    public int ammo;
    public int HP;
    public bool isalive;

    private Vector2 speed;

    public GameObject explosion;
    public Animator hpstate;

    public Camera cam;
    public PlayerMovement movement;
    private Rigidbody2D rb;
    

    private void Awake()
    {
        HP = 100;
        ammo = 5;
        speed = new Vector2(0, 3);
        isalive = true;

        movement = GetComponent<PlayerMovement>();
        movement.enabled = true;

        rb = GetComponent<Rigidbody2D>();
        GameManager.singleton.player = this;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            Instantiate(cam, gameObject.transform);
            cam.tag = "MainCamera";
            movement.cam = cam;
            GameManager.singleton.gamestart = true;
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (HP <= 0)
            {
                HP = 0;
                if(isalive)
                    PlayerDead();
            }
            hpstate.SetFloat("HP", HP);
            Debug.Log("HP = " + HP);
            Debug.Log("Ammo = " + ammo);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLocalPlayer)
        {
            Debug.Log("Colisão");
            switch (collision.tag)
            {
                case ("Bullet"):
                    {
                        Debug.Log("Colidiu com tiro");
                        HP -= 25;
                        break;
                    }
                case ("Trap"):
                    {
                        Debug.Log("Colidiu com armadilha");
                        HP -= 50;
                        break;
                    }
                case ("Obstacle"):
                    {
                        Debug.Log("Colidiu com obstaculo");
                        if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        {
                            HP -= 10;
                        }
                        break;
                    }
                case ("Player"):
                    {
                        Debug.Log("Colidiu com outro jogador");
                        if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        {
                            HP -= 4;
                        }
                        break;
                    }
                case ("Ammo"):
                    {
                        Debug.Log("Pegou municão");
                        ammo += 4;
                        Destroy(collision.gameObject);
                        break;
                    }
                default:
                    {
                        Debug.Log("Erro em definir tipo de colisão");
                        break;
                    }
            }
        }
    }

    private void PlayerDead()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        isalive = false;
        CmdEndGame();
    }

    [Command]
    private void CmdEndGame()
    {
        RpcEndGame();
    }

    [ClientRpc]
    private void RpcEndGame()
    {
        GameManager.singleton.endgameflag = true;
    }
}
