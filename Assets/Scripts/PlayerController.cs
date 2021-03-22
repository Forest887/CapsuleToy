using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    [SerializeField] GameObject player = null;
    [SerializeField] Material[] defaultMaterial = null;
    [SerializeField] Material[] hideMaterial = null;

    [SerializeField] float speed = 1.5f;
    float defaultSpeed;
    [SerializeField] int turnSpeed = 9;
    [SerializeField] int throwPower = 6;

    [SerializeField] GameObject capsule = null;
    [SerializeField] GameObject throwPoint = null;

    public bool hide = false;

    bool isThrow = false;
    DateTime reloadTime;
    TimeSpan allowTime = new TimeSpan(0, 0, 1);
    // 前回ボタンが押された時点と現在時間との差分を格納
    TimeSpan pastTime;

    public Vector3 velo = Vector3.zero;
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

        velo = rb.velocity;
        anim.SetFloat("Run", velo.magnitude);

        if (Input.GetButtonDown("Run"))
        {
            speed *= 2;
            if (hide)
            {
                Hide();
            }
        }
        if (Input.GetButtonUp("Run"))
        {
            speed = defaultSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            ThrowACapsule();
        }

        //if (Input.GetButtonDown("Hide"))
        //{
        //    Hide();
        //}

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
        ballRb.AddForce((transform.forward + new Vector3(0, 0.4f, 0)) * throwPower, ForceMode.Impulse);
    }

    void Hide()
    {
        // 走っているときは隠れることができない
        if (velo.magnitude <= 1.6f)
        {
            // Materialを変えるときは一度レンダラーだけを取り出してから変える
            Renderer renderer = player.GetComponent<Renderer>();
            // Materialが複数の時は一度配列で取得して、配列としてセットし直す
            Material[] materials = renderer.materials;
            if (hide)
            {
                Debug.Log("default");
                hide = false;
                materials[0] = defaultMaterial[0];
                materials[1] = defaultMaterial[1];
            }
            else
            {
                Debug.Log("hide");
                hide = true;
                materials[0] = hideMaterial[0];
                materials[1] = hideMaterial[1];
            }
            renderer.materials = materials;
        }
    }
}
