using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoad : MonoBehaviour
{
    [SerializeField] GameObject enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.sceneLoaded -= BattleSceneLoaded;
            SceneManager.LoadScene("Battle");
        }
    }
    //private void BattleSceneLoaded(Scene next, LoadSceneMode mode)
    //{
    //    GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //    //gameManager.enemyNum = 1;

    //    SceneManager.sceneLoaded -= BattleSceneLoaded;
    //}
}
