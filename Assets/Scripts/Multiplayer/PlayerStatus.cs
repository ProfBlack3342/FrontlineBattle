using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    [SerializeField][SyncVar]public int HP;
    [SerializeField][SyncVar]public int ammo;
    [SyncVar]public uint id;
    private Vector2 speed;
    [SyncVar]public bool isalive;

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
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (HP <= 0)
            {
                PlayerDead();
            }
            Debug.Log("HP = " + HP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLocalPlayer)
        {
            Debug.Log("Colisão");
            switch (collision.tag)
            {
                case ("Obstacle"):
                    {
                        Debug.Log("Colidiu com obstaculo");
                        if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        {
                            SetHP(GetHP() - 5);
                        }
                        break;
                    }
                case ("Player"):
                    {
                        Debug.Log("Colidiu com outro jogador");
                        if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        {
                            SetHP(GetHP() - 1);
                        }
                        break;
                    }
                case ("Ammo"):
                    {
                        Debug.Log("Pegou municão");
                        SetAmmo(GetAmmo() + 4);
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
        isalive = false;
    }

    public void SetHP(int HP) { if (!isServer) return;  this.HP = HP; }
    public int GetHP() { return HP; }

    public void SetAmmo(int ammo) { if (!isServer) return; this.ammo = ammo; }
    public int GetAmmo() { return ammo; }
}
