using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletMovement : NetworkBehaviour
{
    private float speed;
    private Transform self;
    private float timer;

    private Rigidbody2D rb;
    public GameObject explosion;
    public Transform parent;

    private void Awake()
    {
        speed = 10f;
        self = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        timer = 2;
    }

    private void Start()
    {
        Vector2 force = new Vector2(speed, 0);

        rb.AddRelativeForce(force);
        Destroy(gameObject, timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }
}
