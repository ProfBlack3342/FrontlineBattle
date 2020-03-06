using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletMovement : NetworkBehaviour
{
    private float speed;
    Transform self;

    private void Awake()
    {
        speed = 4.5f;
        self =  GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        self.Translate(speed, 0, 0);

        if(Time.deltaTime > 5)
        {
            Destroy(this);
        }
    }
}
