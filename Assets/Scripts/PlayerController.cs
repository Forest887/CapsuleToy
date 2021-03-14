using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField] int speed = 3;
    int defaultSpeed;
    int runSpeed = 1;
    [SerializeField] int turnSpeed = 3;


    bool run = false;

    float x = 0;
    float z = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        defaultSpeed = speed;
    }


    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Vector3 velo = rb.velocity;
        anim.SetFloat("Run", velo.magnitude);

        if (Input.GetButtonDown("Run"))
        {
            speed *= 3;
        }
        if (Input.GetButtonUp("Run"))
        {
            speed = defaultSpeed;
        }
        //if (Input.GetButton("Run"))
        //{
        //    runSpeed = 3;
        //}
        //else
        //{
        //    runSpeed = 1;
        //}
    }

    private void FixedUpdate()
    {
        //移動方向のベクトル
        Vector3 dir = Vector3.forward * z + Vector3.right * x;
        //カメラを基準に入力方向のベクトルを変換する
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir == Vector3.zero)//移動していないときは落下や上昇のベクトルを保つ
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        else
        {
            //入力方向に回転させる
            Quaternion rotation = Quaternion.LookRotation(dir);
            //Slerpで滑らかに回転させる
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * turnSpeed);
            //入力方向に移動する
            Vector3 velo = dir.normalized * speed * runSpeed;
            velo.y = rb.velocity.y;
            rb.velocity = velo;
        }

    }
}
