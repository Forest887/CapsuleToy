using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static bool m_isExists = false;

    //[SerializeField] GameObject[] enemys = null;

    public GameObject enemy = null;

    bool battleScene = false;

    void Awake()
    {
        if (m_isExists)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_isExists = true;
            DontDestroyOnLoad(this.gameObject);
        }

        SceneManager.activeSceneChanged += ActiveSceneChanged;

        if (SceneManager.GetActiveScene().name == "Battle")
        { battleScene = true; }
        else
        { battleScene = false; }
        Debug.Log(battleScene);
    }
    void Start()
    {
    }

    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        if (SceneManager.GetActiveScene().name == "Battle")
        { battleScene = true; }
        else
        { battleScene = false; }
        Debug.Log(battleScene);
    }
}
