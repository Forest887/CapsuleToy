using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 setPosition;

    float timer = 0f;
    int moveTimer = 6;
    bool move = false;
    bool gohome = false;

    enum State
    {
        normal, tracking, goHome
    }
    State state = State.normal;

    //キャラごとに変えたい
    int moveSpeed = 2;
    int turnSpeed = 6;

    float searchAngle = 110f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
    }

    void Update()
    {
        switch (state)
        {
            case State.normal:
                NomalAction();
                break;
            case State.tracking:
                Move(setPosition);
                break;
            case State.goHome:
                GoHome();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// setPositionに向かって進む
    /// </summary>
    private void Move(Vector3 setPosition)
    {
        Vector3 heading = setPosition - this.transform.position;
        float dist = heading.magnitude;
        Vector3 direction = heading / dist;
        // 向きを変える
        Quaternion rotation = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * turnSpeed);

        rb.velocity = direction * moveSpeed;

        if (0.2 > dist && dist > -0.2)
        {
            moveTimer = Random.Range(5, 10);
            move = false;
        }
    }

    /// <summary>
    /// 通常時の行動
    /// </summary>
    public void NomalAction()
    {
        if (!move)
        {
            timer += Time.deltaTime;
            if (timer > moveTimer)
            {
                setPosition = SetPosition(startPosition);
                move = true;
                timer = 0;
               
            }
        }
        else
        {
            Move(setPosition);
        }
    }

    void GoHome()
    {
        if (!gohome)
        {
            timer = 0;
            move = true;
            setPosition = SetPosition(startPosition);
            gohome = true;
        }
        else
        {
            if (timer < 3)
            { timer += Time.deltaTime; }
            else
            { Move(setPosition); }
        }
        if (!move)
        {
            gohome = false;
            timer = 0;
            state = State.normal;
        }
    }

    /// <summary>
    /// 移動先を決める
    /// </summary>
    /// <param name="startPosition">初期位置</param>
    /// <returns>移動先</returns>
    public Vector3 SetPosition(Vector3 startPosition)
    {
        float x = startPosition.x + Random.Range(-3f, 3f);
        float z = startPosition.z + Random.Range(-3f, 3f);
        return new Vector3(x, 0, z);
    }
    void OnTriggerStay(Collider other)
    {
        //https://gametukurikata.com/program/onlyforwardsearch
        // 視野
        if (other.tag == "Player")
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (controller.veloM >= 1.9f)
            {
                setPosition = other.gameObject.transform.position;
                state = State.tracking;
            }
            else
            {
                //　主人公の方向
                var playerDireciton = other.transform.position - this.transform.position;
                //　敵の前方からの主人公の方向
                var angle = Vector3.Angle(transform.forward, playerDireciton);
                //　サーチする角度内だったら発見
                if (angle <= searchAngle)
                {
                    setPosition = other.gameObject.transform.position;
                    state = State.tracking;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            state = State.goHome;
        }
    }
}
