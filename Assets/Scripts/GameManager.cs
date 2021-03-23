using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemys = null;
    [SerializeField] GameObject enemyZone = null;
    [SerializeField] GameObject playerZone = null;

    GameObject enemy = null;
    int num = 10;
    void Start()
    {
        Debug.Log(num);
        if (num <= enemys.Length)
        {
            enemy = Instantiate(enemys[num - 1], enemyZone.transform);

        }
    }

}
