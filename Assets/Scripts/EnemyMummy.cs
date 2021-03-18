using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMummy : MonoBehaviour
{
    //string nAME = "Mummy";
    int hP = 30;
    int sTR = 1;
    //int aGI = 15;

    //public EnemyMummy(int hP, int sTR, int aGI)
    //{
    //    this.hP = hP;
    //    this.sTR = sTR;
    //    this.aGI = aGI;
    //}

    Animator anim;
    [SerializeField] GameObject battleSystemObj = null;
    BattleSystem battleSystem;

    void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.Play("MummyAttack");
        return sTR;
    }

    public void DamegeCheck(int damege)
    {
        Debug.Log(damege);
        hP -= damege;
        battleSystem.TurnCheck(0);
    }

    void Down()
    {
        Destroy(this.gameObject);
    }
}
