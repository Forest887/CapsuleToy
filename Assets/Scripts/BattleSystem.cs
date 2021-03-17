using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    enum Turn
    {
        PlanTurn, EnemyPlanTurn, Ordering, ActionTurn, EnemyActionTurn, Other
    }
    Turn turnType = Turn.Other;

    [SerializeField] GameObject attackButton = null;

    int attackNum = 0;
    bool playerFast = true;

    void Start()
    {
        
    }


    void Update()
    {
        switch (turnType)
        {
        // 行動を決める
            case Turn.PlanTurn:
                attackButton.SetActive(true);
                break;
            case Turn.EnemyPlanTurn:
                // Enemy.Plan();
                break;
        // 順番決め
            case Turn.Ordering:
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
                // 動かすAnimationでChack()を呼び出す
                // Player.Action();
                break;
            case Turn.EnemyActionTurn:
                // 動かすAnimationでChack()を呼び出す
                // Enemy.Action();
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

    public void Chack(int actionChara)
    {
        // 呼び出し元が自キャラの時
        if (actionChara == 0)
        {
            if (playerFast)
                turnType = Turn.EnemyActionTurn;
            else
                turnType = Turn.PlanTurn;
        }
        else
        {
            if (!playerFast)
                turnType = Turn.ActionTurn;
            else
                turnType = Turn.PlanTurn;
        }
    }
}
