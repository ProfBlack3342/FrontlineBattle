using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    [SerializeField]private int HP;
    [SerializeField]private int ammo;
    public uint id;
    private Vector2 speed;
    public bool isalive;

    private PlayerMovement movement;

    private Rigidbody2D rb;
    public Camera cam;

    private void Awake()
    {
        if(isClient)
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
        if(isLocalPlayer)
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

    [Command] void CmdPlayerDead()
    {
        movement.enabled = false;
        isalive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case("Bullet"):
                {
                    HP -= 25;
                    break;
                }
            case ("Obstacle"):
                {
                    if(rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        HP -= 5;
                    break;
                }
            case ("Trap"):
                {
                    HP -= 25;
                    break;
                }
            case ("Player"):
                {
                    if (rb.velocity.y > speed.y || rb.velocity.y < -speed.y)
                        HP -= 1;
                    break;
                }
            case ("Ammo"):
                {
                    ammo += 4;
                    break;
                }
        }
    }

    [Command]public void CmdSetHP(int HP) { this.HP = HP; }
    public int GetHP() { return HP; }

    [Command]public void CmdSetAmmo(int ammo) { this.ammo = ammo; }
    public int GetAmmo() { return ammo; }
}
