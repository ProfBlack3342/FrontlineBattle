using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [Range(0.01f, 1.0f)]
    public float smooth;

    private Vector3 cameraoffset;
    private Transform player;


    private void Awake()
    {
        player = transform.parent.transform;
        transform.parent = null;
        smooth = 0.5f;
    }

    private void Start()
    {
        cameraoffset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newpos = player.position + cameraoffset;
            transform.position = Vector3.Slerp(transform.position, newpos, smooth);
        }
    }
}
