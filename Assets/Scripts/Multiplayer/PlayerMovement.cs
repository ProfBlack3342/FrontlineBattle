using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public Rigidbody2D rb;
    private float Fspeed, Rspeed;
    public GameObject bullet;
    public GameObject bulletspawn;
    public GameObject cannon;

    private Vector2 mousepos;
    public Camera cam;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        Fspeed = 1000f;
        Rspeed = 5f;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer)
        {
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                    Movement();

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                    Rotation();


                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Instantiate(bullet, bulletspawn.transform.position, bulletspawn.transform.rotation);
            }

        }
    }

    private void Movement()
    {
        Debug.Log("Movement");
        Vector2 input = new Vector2((Input.GetAxisRaw("Vertical") * Fspeed * Time.deltaTime), 0);
        rb.AddRelativeForce(input);
    }


    private void Rotation()
    {
        Debug.Log("Rotation");
        float input = Input.GetAxisRaw("Horizontal");

        rb.AddTorque(input);
    }

}
