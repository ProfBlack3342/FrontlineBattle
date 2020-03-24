using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    public int HP;
    public int ammo;
    public uint id = 0;
    private Vector2 speed;
    public bool isalive;

    public GameObject explosion;
    public Animator hpstate;

    private PlayerMovement movement;
    private Rigidbody2D rb;
    public Camera cam;

    private void Awake()
    {
        if (isClient)
        {
            id = 2;
        }
        if (isServer)
        {
            id = 1;
        }

        HP = 100;
        ammo = 5;
        speed = new Vector2(0, 3);
        isalive = true;

        movement = GetComponent<PlayerMovement>();
        movement.enabled = true;

        rb = GetComponent<Rigidbody2D>();

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

    void PlayerDead()
    {
        movement.enabled = false;
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
