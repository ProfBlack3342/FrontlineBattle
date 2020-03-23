using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Animations;

public class PlayerMovement : NetworkBehaviour
{
    private float Fspeed, Rspeed;
    private Vector2 mousepos;

    public Rigidbody2D rb;
    public GameObject bullet;
    public GameObject bulletspawn;
    public GameObject cannon;
    public Camera cam;
    public Animator anim;

    private PlayerStatus status;

    private void Awake()
    {
        Fspeed = 1000f;
        Rspeed = 5f;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        anim = GetComponent<Animator>();

        status = GetComponent<PlayerStatus>();
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                    Movement();

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                    Rotation();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (status.GetAmmo() > 0)
                    {
                        CmdSpawnBullet();
                        status.SetAmmo(status.GetAmmo() - 1);
                    }
                    else
                    {
                        Debug.Log("Out of Bullets");
                    }
                }
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            Vector3 mousepos = Input.mousePosition;
            mousepos = cam.ScreenToWorldPoint(mousepos);

            Vector2 direction = new Vector2(mousepos.x, mousepos.y);
            cannon.transform.right = direction;
        }
    }

    [Command]
    public void CmdSpawnBullet()
    {
        Instantiate(bullet, bulletspawn.transform.position, bulletspawn.transform.rotation);
    }

    private void Movement()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Movement");
            anim.SetBool("Walking", true);
            Vector2 input = new Vector2((Input.GetAxisRaw("Vertical") * Fspeed * Time.deltaTime), 0);
            rb.AddRelativeForce(input);
        }
    }

    private void Rotation()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Rotation");
            float input = -Input.GetAxisRaw("Horizontal");

            rb.AddTorque(input);
        }
    }


}
