using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField] float speed = 3;
    float defaultSpeed;
    [SerializeField] int turnSpeed = 3;
    [SerializeField] int throwPower = 8;

    [SerializeField] GameObject capsule = null;
    GameObject throwPoint = null;


    bool isThrow = false;
    DateTime reloadTime;
    TimeSpan allowTime = new TimeSpan(0, 0, 1);
    // 前回ボタンが押された時点と現在時間との差分を格納
    TimeSpan pastTime;

    float x = 0;
    float z = 0;
    void Start()
    {
        throwPoint = GameObject.Find("ThrowPoint");
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
            speed *= 2;
        }
        if (Input.GetButtonUp("Run"))
        {
            speed = defaultSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            ThrowACapsule();
        }
    }

    private void FixedUpdate()
    {
        // 移動方向のベクトル
        Vector3 dir = Vector3.forward * z + Vector3.right * x;
        // カメラを基準に入力方向のベクトルを変換する
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir == Vector3.zero)// 移動していないときは落下や上昇のベクトルを保つ
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        else
        {
            // 入力方向に回転させる
            Quaternion rotation = Quaternion.LookRotation(dir);
            // Slerpで滑らかに回転させる
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * turnSpeed);
            // 入力方向に移動する
            Vector3 velo = dir.normalized * speed;
            velo.y = rb.velocity.y;
            rb.velocity = velo;
        }

        // 連打防止
        if (isThrow)
        {
            pastTime = DateTime.Now - reloadTime;
            if (pastTime > allowTime)
            {
                isThrow = false;
            }
        }
    }

    void ThrowACapsule()
    {
        // 連打防止
        if (isThrow) return;
        isThrow = true;
        // 現在の時間をセット
        reloadTime = DateTime.Now;

        // 生成
        GameObject ball = Instantiate(capsule, throwPoint.transform.position, Quaternion.identity);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.AddForce(this.transform.forward * throwPower, ForceMode.Impulse);
    }
}
