using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMummy : MonoBehaviour
{
    //string nAME = "Mummy";
    [SerializeField] int hP = 30;
    [SerializeField] int sTR = 1;
    //int aGI = 15;

    //public EnemyMummy(int hP, int sTR, int aGI)
    //{
    //    this.hP = hP;
    //    this.sTR = sTR;
    //    this.aGI = aGI;
    //}

    Rigidbody rb;
    int speed = 2;
    Animator anim;
    GameObject battleSystemObj = null;
    BattleSystem battleSystem;
    bool attackAct = false;

    Vector3 defaultPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        battleSystemObj = GameObject.Find("BattleSystem");
        battleSystem = battleSystemObj.GetComponent<BattleSystem>();
        defaultPosition = this.transform.position;
    }


    void Update()
    {
        if (hP <= 0)
        {
            Down();
        }

        if (attackAct)
        {
            AttackAction();
        }
    }

    public int Attack()
    {
        attackAct = true;
        return sTR;
    }

    public void DamegeCheck(int damege)
    {
        Debug.Log(damege);
        hP -= damege;
        anim.Play("Damege");
        rb.AddForce(transform.forward * -3, ForceMode.Impulse);
    }

    void Down()
    {
        Destroy(this.gameObject);
    }
    void AttackAction()
    {
        Vector3 heading = new Vector3(0, 0, -1.5f) - this.transform.position;

        float dist = heading.magnitude;

        Vector3 direction = heading / dist;
        transform.forward = direction;

        if (dist > 0.1f)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            anim.Play("MummyAttack 1");
            attackAct = false;
        }
    }

    public void AttackTrigger()
    {
        battleSystem.AttackCheck((int)Group.rival, sTR);
        //battleSystem.TurnCheck((int)Group.rival);
    }

    public void AttackEnd()
    {
        this.transform.position = defaultPosition;
    }

    public void PositionReset()
    {
        this.transform.position = defaultPosition;
        battleSystem.TurnCheck((int)Group.partner);
    }

    //public void CameraChange()
    //{
    //    battleSystem.CameraChange(2);
    //}
}
