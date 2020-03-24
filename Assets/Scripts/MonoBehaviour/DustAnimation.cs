using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DustAnimation : MonoBehaviour
{
    public GameObject dustprefab;
    private GameObject dust;
    private Animation dustanim;

    public void PlayAnimation()
    {
        dust = Instantiate(dustprefab, transform.position, Quaternion.identity);
    }
}
