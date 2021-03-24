using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoad : MonoBehaviour
{
    GameObject manager;
    GameManager gameManager;

    private void Start()
    {
        manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            gameManager.enemy = other.gameObject;
            SceneManager.LoadScene("Battle");
        }
    }
}
