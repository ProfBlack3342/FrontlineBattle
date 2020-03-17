using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAnimation : MonoBehaviour
{
    public GameObject dustprefab;

    public void PlayAnimation()
    {
        Instantiate(dustprefab, transform.position, Quaternion.identity);
    }
}
