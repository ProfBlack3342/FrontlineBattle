using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    private float Fspeed, Rspeed;
    public GameObject bullet;
    public GameObject bulletspawn;
    public GameObject cannon;
    private Vector2 mousepos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Fspeed = 2.5f;
        Rspeed = 1;
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Movement(v);
            Rotation(h);

            mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            cannon.transform.LookAt(mousepos);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(bullet, bulletspawn.transform.position, Quaternion.identity);
            }
        }
    }

    private void Movement(float input)
    {
        float movement = input * Fspeed * Time.deltaTime;

        rb.velocity = transform.forward * movement;
    }

    private void Rotation(float input)
    {
        float rotation = input * Rspeed * Time.deltaTime;

        rb.MoveRotation(rb.rotation * rotation);
    }
}
