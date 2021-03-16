using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    GameObject player = null;
    PlayerController controller;
    Rigidbody rb;
    float speed = 1.5f;

    void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 heading = player.transform.position - this.transform.position;

        float dist = heading.magnitude;

        Vector3 direction = heading / dist;
        transform.forward = direction;

        if (dist > 6)
        {

        }
        else if (dist > 2)
        {
            rb.velocity = direction * speed;
        }


        if (dist > 3 && speed < 3)
        {
            speed += 0.1f;
        }
        else if(speed > 1.5f)
        {
            speed -= 0.05f;
        }
    }
}
