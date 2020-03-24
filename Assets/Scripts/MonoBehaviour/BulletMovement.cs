using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private Transform self;
    private float timer;

    private Rigidbody2D rb;
    public GameObject explosion;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
