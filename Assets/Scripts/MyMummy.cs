using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMummy : MonoBehaviour
{
    //string nAME = "Mummy";
    int hP = 50;
    int sTR = 10;
    //int aGI = 15;

    //public MyMummy(int hP,int sTR, int aGI)
    //{
    //    this.hP = hP;
    //    this.sTR = sTR;
    //    this.aGI = aGI;
    //}

    [SerializeField] GameObject battleSystemObj = null;
    BattleSystem battleSystem;

    void Start()
    {
        battleSystem = battleSystemObj.GetComponent<BattleSystem>();
    }


    void Update()
    {
        if (hP <= 0)
        {
            Down();
        }
        
    }

    public int Attack()
    {
        return sTR;
    }

    public void DamegeCheck(int damege)
    {
        Debug.Log(damege);
        hP -= damege;
        battleSystem.TurnCheck(1);

    }

    void Down()
    {
        Destroy(this.gameObject);
    }
}
