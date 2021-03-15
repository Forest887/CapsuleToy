using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Vector3 velo = rb.velocity;
        anim.SetFloat("Run", velo.magnitude);
    }
}
