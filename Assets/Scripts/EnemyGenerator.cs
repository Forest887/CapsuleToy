using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject[] enemys = null;

    bool generator = true;

    void Start()
    {
        Vector3 heading = this.transform.position - player.transform.position;
        float dist = heading.magnitude;

        if (dist <= 5)
        {
            generator = false;
        }

        if (generator)
        {
            int random = Random.Range(0, enemys.Length);
            int x = Random.Range(0, 3);
            int z = Random.Range(0, 3);
            int yQ = Random.Range(0, 360);
            Vector3 instantiateP = this.transform.position + new Vector3(x, 0, z);
            Instantiate(enemys[random], instantiateP, Quaternion.Euler(0, yQ, 0));
        }
    }

}
