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
                break;
        // 行動する
            case Turn.ActionTurn:
                // Player.Action();
                if (playerFast)
                {
                    turnType = Turn.EnemyActionTurn;
                }
                break;
            case Turn.EnemyActionTurn:
                // Enemy.Action();
                if (!playerFast)
                {
                    turnType = Turn.ActionTurn;
                }
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
}
