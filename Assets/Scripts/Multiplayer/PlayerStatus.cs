using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private int ammo;
    public uint id;
    private Vector2 speed;
    public bool isalive;

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
                CmdPlayerDead();
            }
            Debug.Log("HP = " + HP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisão");
        switch (collision.tag)
        {
            case ("Bullet"):
                {
                    Debug.Log("Colidiu com tiro");
                    if (collision.GetComponent<BulletMovement>().parent != gameObject.transform)
                    {
                        CmdSetHP(CmdGetHP() - 25);
                    }
                    break;
                }
            case ("Obstacle"):
                {
                    Debug.Log("Colidiu com obstaculo");
                    if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        CmdSetHP(CmdGetHP() - 5);
                    break;
                }
            case ("Trap"):
                {
                    Debug.Log("Colidiu com armadilha");
                    CmdSetHP(CmdGetHP() - 25);
                    break;
                }
            case ("Player"):
                {
                    Debug.Log("Colidiu com outro jogador");
                    if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        CmdSetHP(CmdGetHP() - 1);
                    break;
                }
            case ("Ammo"):
                {
                    Debug.Log("Pegou municão");
                    CmdSetAmmo(CmdGetAmmo() + 4);
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

    [Command]
    void CmdPlayerDead()
    {
        movement.enabled = false;
        isalive = false;
    }

    [Command] public void CmdSetHP(int HP) { this.HP = HP; }
    public int CmdGetHP() { return HP; }

    [Command] public void CmdSetAmmo(int ammo) { this.ammo = ammo; }
    public int CmdGetAmmo() { return ammo; }
}
