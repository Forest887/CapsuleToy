using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleBall : MonoBehaviour
{
    [SerializeField] Vector3 localGravity = Vector3.zero;
    Rigidbody rBody;

    float timer = 0f;

    // Use this for initialization
    private void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        SetLocalGravity(); //重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。

        if (timer >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 重力を変更する
    /// </summary>
    private void SetLocalGravity()
    {
        rBody.AddForce(localGravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            rBody.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
        }

    }
}
