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
            if (collision.tag == "Player")
            {
                CmdDamage(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    [Command]
    public void CmdDamage(GameObject enemy)
    {
        enemy.GetComponent<PlayerStatus>().HP -= 25;

        NetworkIdentity enemyIdentity = enemy.GetComponent<NetworkIdentity>();
        TargetDamage(enemyIdentity.connectionToClient, 25);
    }

    [TargetRpc]
    public void TargetDamage(NetworkConnection connection, float damage)
    {
        Debug.Log("Recebeu" + damage + " de dano");
    }
}
