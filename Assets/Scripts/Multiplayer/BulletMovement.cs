using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletMovement : NetworkBehaviour
{
    private float speed;
    Transform self;
    float timer;

    private void Awake()
    {
        speed = 0.5f;
        self =  GetComponent<Transform>();
        timer = 2;

        Destroy(gameObject, timer);
    }

    private void FixedUpdate()
    {
        self.Translate(speed, 0, 0, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Trap" && other.tag != "Map")
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
