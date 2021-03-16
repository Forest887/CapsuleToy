using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] GameObject player = null;

    Rigidbody rb;

    bool hide = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player && !hide)
        {
            Vector3 heading = player.transform.position - this.transform.position;

            float dist = heading.magnitude;

            Vector3 direction = heading / dist;
            transform.forward = direction;

            rb.velocity = direction * speed;
        }
    }
}
