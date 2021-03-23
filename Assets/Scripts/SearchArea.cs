using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    GameObject player = null;
    [SerializeField] float searchAngle = 110f;
    [SerializeField] GameObject enemyObj = null;
    Enemy enemy;
    void Start()
    {
        enemy = enemyObj.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //https://gametukurikata.com/program/onlyforwardsearch
        // 視野
        if (other.tag == "Player")
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>(); 
            if (controller.veloM >= 1.9f)
            {
                player = other.gameObject;
                enemy.Search(player);
            }
            else if (!controller.hide)
            {
                //　主人公の方向
                var playerDireciton = other.transform.position - this.transform.position;
                //　敵の前方からの主人公の方向
                var angle = Vector3.Angle(transform.forward, playerDireciton);
                //　サーチする角度内だったら発見
                if (angle <= searchAngle)
                {
                    player = other.gameObject;
                    enemy.Search(player);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
            enemy.Search(player);
        }
    }
}
