using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 setPosition;

    int moveSpeed = 2;
    int turnSpeed = 6;
    float timer = 0f;
    int moveTimer = 6;
    bool move = false;

    GameObject player = null;
    float searchAngle = 110f;
    enum State
    {
        normal, tracking
    }
    State state = State.normal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
    }

    public void UPDATE()
    {
        switch (state)
        {
            case State.normal:
                DefaultAction();
                break;
            case State.tracking:
                break;
            default:
                break;
        }
    }

    private void Move(Vector3 vector3)
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
    public void DefaultAction()
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
    public void OnTriggerStay(Collider other)
    {
        //https://gametukurikata.com/program/onlyforwardsearch
        // 視野
        if (other.tag == "Player")
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (controller.veloM >= 1.9f)
            {
                player = other.gameObject;
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
                    player = other.gameObject;
                }
            }
        }
    }
}
