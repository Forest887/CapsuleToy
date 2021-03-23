using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;

    GameObject player = null;

    [SerializeField] GameObject searchArea = null;
    [SerializeField] float searchAngle = 130f;

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
    public void Search(GameObject player)
    {
        this.player = player;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    //https://gametukurikata.com/program/onlyforwardsearch
    //    // 視野
    //    if (other.tag =="Player")
    //    {
    //        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
    //        if (!controller.hide)
    //        {
    //            //　主人公の方向
    //            var playerDireciton = other.transform.position - this.transform.position;
    //            //　敵の前方からの主人公の方向
    //            var angle = Vector3.Angle(transform.forward, playerDireciton);
    //            //　サーチする角度内だったら発見
    //            if (angle <= searchAngle)
    //            {
    //                player = other.gameObject;
    //            }
    //        }
    //        else if(controller.veloM >= 2)
    //        {
    //            player = other.gameObject;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        player = null;
    //    }
    //}
}
