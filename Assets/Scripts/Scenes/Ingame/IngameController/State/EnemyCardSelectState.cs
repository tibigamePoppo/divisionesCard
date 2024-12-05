using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardSelectState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("BattleState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        EnemySelectArg selectArg = arg1 as EnemySelectArg;
        view.EnemyCardHide();
        base.StateAction(arg1);
    }
}
