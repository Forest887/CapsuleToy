using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Group
{
    partner, rival
}
public class BattleSystem : MonoBehaviour
{
    enum Turn
    {
        PlanTurn, EnemyPlanTurn, Ordering, ActionTurn, EnemyActionTurn, Other
    }
    Turn turnType = Turn.PlanTurn;

    [SerializeField] GameObject attackButton = null;
    [SerializeField] GameObject player = null;
    MyMummy my;
    [SerializeField] GameObject enemyMummy = null;
    EnemyMummy enemy;

    int attackNum = 0;
    bool playerFast = true;

    void Start()
    {
        my = player.GetComponent<MyMummy>();
        enemy = enemyMummy.GetComponent<EnemyMummy>();
    }
    /// <summary>
    /// 後で消します
    /// </summary>
    public void Tatakau()
    {
        turnType = Turn.EnemyPlanTurn;
    }

    void Update()
    {
        if (player && enemyMummy)
        {

        }
        else
        {
            Debug.Log("終了");
            return;
        }
        switch (turnType)
        {
        // 行動を決める
            case Turn.PlanTurn:
                //Debug.Log("PlanTurn");
                attackButton.SetActive(true);
                break;
            case Turn.EnemyPlanTurn:
                Debug.Log("EnemyPlanTurn");
                // Enemy.Plan();

                turnType = Turn.Ordering;//仮　消す予定
                break;
        // 順番決め
            case Turn.Ordering:
                Debug.Log("Ordering");
                // 速さで順番を決める
                // if (player.speed > enemy.speed)
                // { playerFast = true; }
                // else
                // { playerFast = fale; }

                if (playerFast)
                { turnType = Turn.ActionTurn; }
                else
                { turnType = Turn.EnemyActionTurn; }
                break;
        // 行動する
            case Turn.ActionTurn:
                Debug.Log("ActionTurn");
                // 動かすAnimationでTurnCheck()を呼び出す
                // Player.Action();

                //enemy.DamegeCheck(my.Attack());//仮　消す予定
                my.Attack();

                turnType = Turn.Other;
                break;
            case Turn.EnemyActionTurn:
                Debug.Log("EnemyActionTurn");
                // 動かすAnimationでTurnCheck()を呼び出す
                // Enemy.Action();

                //my.DamegeCheck(enemy.Attack());//仮　消す予定
                enemy.Attack();

                turnType = Turn.Other;
                break;
        // その他
            case Turn.Other:
                break;
            default:
                break;
        }

    }

    public void AttackPlan(int num)
    {
        attackNum = num;
    }

    /// <summary>
    /// 先に誰が行動しているかによって次の行動を決める
    /// </summary>
    /// <param name="actionChara"></param>
    public void TurnCheck(int actionChara)
    {
        if (actionChara == (int)Group.partner)
        {
            if (playerFast)
            { turnType = Turn.EnemyActionTurn; }
            else
            { turnType = Turn.PlanTurn; }
        }
        else
        {
            if (!playerFast)
            { turnType = Turn.ActionTurn; }
            else
            { turnType = Turn.PlanTurn; }
        }
    }

    public void AttackCheck(int actionChara, int sTR)
    {
        if (actionChara == (int)Group.partner)
        {
            enemy.DamegeCheck(sTR);
        }
        else
        {
            my.DamegeCheck(sTR);
        }

    }
}
