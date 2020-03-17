using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    private int HP;
    private int ammo;
    public uint id;
    private Vector2 speed;
    private bool endstateflag;

    public StateMachine machine;
    private PlayerMovement movement;

    private Rigidbody2D rb;
    public Camera cam;

    private void Awake()
    {
        if (isClient)
        {
            if (isServer)
                id = 1;
            else
                id = 2;
        }
        HP = 100;
        ammo = 5;
        speed = new Vector2(0, 3);
        endstateflag = false;
        rb = GetComponent<Rigidbody2D>();
        machine = new StateMachine();
        movement = GetComponent<PlayerMovement>();
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
        if (!endstateflag)
        {
            Debug.Log("Player Update() calling for ExecuteState()");
            machine.ExecuteState();
        }
        else
        {
            Debug.Log("Player Update() calling for EndState()");
            machine.EndState();
        }
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
                    HP -= 1;
                    break;
                }
            case ("Trap"):
                {

                    break;
                }
            case ("Player"):
                {

                    break;
                }
        }
    }

    public void SetHP(int HP) { this.HP = HP; }
    public int GetHP() { return HP; }

    public void SetAmmo(int ammo) { this.ammo = ammo; }
    public int GetAmmo() { return ammo; }
}
